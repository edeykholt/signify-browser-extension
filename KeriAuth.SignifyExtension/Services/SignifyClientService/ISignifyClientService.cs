using KeriAuth.SignifyExtension.Services.SignifyClientService;
using KeriAuth.SignifyExtension.Services.SignifyClientService.Models;
using Notification = KeriAuth.SignifyExtension.Services.SignifyClientService.Models.Notification;
using FluentResults;
// using KeriAuth.SignifyExtension.Models;
using System.Reactive;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using WebExtensions.Net.Tabs;

namespace KeriAuth.SignifyExtension.Services.SignifyClientService
{
    public interface ISignifyClientService
    {
        Task<Result<Models.Identifier>> CreatePersonAid();
        Task<Result<State>> Boot(Url url);
        Task<Result<State>> GetState();
        Task<Result<bool>> Connect();
        Task<Result<HttpResponseMessage>> Fetch(string path, string method, object data, Dictionary<string, string>? extraHeaders = null);
        Task<Result<HttpResponseMessage>> SignedFetch(string url, string path, string method, object data, string aidName);
        Task<Result<HttpResponseMessage>> ApproveDelegation();
        Task<Result<HttpResponseMessage>> SaveOldPasscode(string passcode);
        Task<Result<HttpResponseMessage>> DeletePasscode();
        Task<Result<HttpResponseMessage>> Rotate(string nbran, string[] aids);
        Task<Result<IList<Models.Identifier>>> GetIdentifiers();
        Task<Result<IList<Oobi>>> GetOobis();
        Task<Result<IList<Operation>>> GetOperations();
        Task<Result<IList<KeyEvent>>> GetKeyEvents();
        Task<Result<IList<KeyState>>> GetKeyStates();
        Task<Result<IList<KeriAuth.SignifyExtension.Services.SignifyClientService.Models.Credential>>> GetCredentials();
        Task<Result<IList<Ipex>>> GetIpex();
        Task<Result<IList<Registry>>> GetRegistries();
        Task<Result<IList<Schema>>> GetSchemas();
        Task<Result<IList<Challenge>>> GetChallenges();
        Task<Result<IList<Contact>>> GetContacts();
        Task<Result<IList<Notification>>> GetNotifications();
        Task<Result<IList<Escrow>>> GetEscrows();
        Task<Result<IList<Models.Group>>> GetGroups();
        Task<Result<IList<Exchange>>> GetExchanges();
    }
}
