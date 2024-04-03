
namespace KeriAuth.SignifyExtension.Services;
using KeriAuth.SignifyExtension.Models;
using Stateless;
using static KeriAuth.SignifyExtension.Services.IStateService;


public class PreferencesService : IPreferencesService, IObservable<Preferences>
{
    public PreferencesService(IStorageService storageService)
    {
        this.storageService = storageService;
    }

    private readonly List<IObserver<Preferences>> preferencesObservers = [];
    private IStorageService storageService;

    public async Task<Preferences> GetPreferences()
    {
        try
        {
            var preferencesResult = await storageService.GetItem<Preferences>();
            if (preferencesResult is null || preferencesResult.IsFailed || preferencesResult.Value is null)
            {
                // If preferences don't yet exist in storage, return the defaults
                return new Preferences();
            }
            else // IsSuccess
            {
                return preferencesResult.Value;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new Preferences();
        }
    }

    public async Task SetPreferences(Preferences preferences)
    {
        await storageService.SetItem<Preferences>(preferences);
        foreach (var observer in preferencesObservers)
            observer.OnNext(preferences);
        return;
    }

    IDisposable IObservable<Preferences>.Subscribe(IObserver<Preferences> preferencesObserver)
    {
        if (!preferencesObservers.Contains(preferencesObserver))
        {
            preferencesObservers.Add(preferencesObserver);
        }
        return new Unsubscriber(preferencesObservers, preferencesObserver);
    }

    private class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<Preferences>> _preferencesObservers;
        private readonly IObserver<Preferences> _preferencesObserver;

        public Unsubscriber(List<IObserver<Preferences>> observers, IObserver<Preferences> observer)
        {
            this._preferencesObservers = observers;
            this._preferencesObserver = observer;
        }

        public void Dispose()
        {
            if (!(_preferencesObserver == null)) _preferencesObservers.Remove(_preferencesObserver);
        }
    }
}