﻿namespace KeriAuth.SignifyExtension.Services;

using KeriAuth.SignifyExtension.Models;
using static KeriAuth.SignifyExtension.Services.IStateService;

public interface IStateService : IObservable<States>
{
    public enum States
    {
        Unknown,
        Uninitialized,
        Initializing,
        Unconfigured,
        Unauthenticated,
        Authenticated
    }

    public States GetState();

    public Task Initialize();

    public Task Configure();

    public Task Authenticate();

    public Task Unauthenticate();

    public Task TimeOut();

    public Task<bool> IsAuthenticated();
}