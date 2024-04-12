namespace KeriAuth.SignifyExtension.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using KeriAuth.SignifyExtension.Services;
using Microsoft.Extensions.Logging;

public class AlarmService : IAlarmService
{
    public readonly ILogger<AlarmService> logger;
    public AlarmService()
    {
        logger = new Logger<AlarmService>(new LoggerFactory()); // TODO: insert via DI
    }

    private class Alarm
    {
        public Timer Timer { get; }
        public TimeSpan Duration { get; private set; }
        private Timer? _debounceTimer;
        private TimeSpan _debounceDuration;

        public Alarm(TimeSpan duration, Action callback)
        {
            Duration = duration;
            Timer = new Timer(state => callback.Invoke(), null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Start()
        {
            Timer.Change(Duration, Timeout.InfiniteTimeSpan);
        }

        public void Stop()
        {
            Timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        }

        public void Reset(TimeSpan newDuration, TimeSpan debounceDuration)
        {
            Duration = newDuration;
            _debounceDuration = debounceDuration;
            TimerCallback debounceTimerCallback = DebounceTimerMethod;

            if (_debounceTimer == null)
            {
                _debounceTimer = new Timer(debounceTimerCallback, null, _debounceDuration, Timeout.InfiniteTimeSpan);
            }
            else
            {
                _debounceTimer.Change(_debounceDuration, Timeout.InfiniteTimeSpan);
            }
        }

        void DebounceTimerMethod(object? state)
        {
            Start();
        }
    }

    private readonly Dictionary<string, Alarm> alarms = [];

    public void StartAlarm(string name)
    {
        if (alarms.TryGetValue(name, out var alarm))
        {
            alarm.Start();
            logger.LogInformation($"Alarm {name} started");
        }
        else
        {
            throw new ArgumentException($"Alarm with name {name} does not exist");
        }
    }

    public void StopAlarm(string name)
    {
        if (alarms.TryGetValue(name, out var alarm))
        {
            alarm.Stop();
        }
        else
        {
            throw new ArgumentException($"Alarm with name {name} does not exist");
        }
    }

    public void ResetAlarm(string name, TimeSpan newDuration, TimeSpan debounceInterval)
    {
        if (alarms.TryGetValue(name, out var alarm))
        {
            alarm.Reset(newDuration, debounceInterval);
        }
        else
        {
            throw new ArgumentException($"Alarm with name {name} does not exist");
        }
    }

    void IAlarmService.CreateAlarm(string name, Action callback, TimeSpan delay)
    {
        var alarm = new Alarm(delay, callback);
        alarms[name] = alarm;
    }
}