namespace KeriAuth.SignifyExtension.Services;

using KeriAuth.SignifyExtension.Models;

public interface IPreferencesService : IObservable<Preferences>
{
    Task<Preferences> GetPreferences();

    Task SetPreferences(Preferences preferences);
   
}