using FluentResults;
using KeriAuth.SignifyExtension.Services.SignifyClientService;
using static KeriAuth.SignifyExtension.Services.SignifyService.SignifyServiceConfig;

using KeriAuth.SignifyExtension.Services;

// using KeriAuth.SignifyExtension.Models;
using KeriAuth.SignifyExtension.Services.SignifyClientService.Models;
using State = KeriAuth.SignifyExtension.Services.SignifyClientService.Models.State;
using System.Text.RegularExpressions;
using Group = KeriAuth.SignifyExtension.Services.SignifyClientService.Models.Group;
using WebExtensions.Net.Tabs;

namespace KeriAuth.SignifyExtension.Services.SignifyClientService
{
    // TODO EE!  This is a placeholder example
    public class MyRequestType { /* Define properties here */ }
    public class MyResponseType { /* Define properties here */ }

    public class SignifyClientService : ISignifyClientService
    {
        public readonly ILogger<SignifyClientService> logger;
        public SignifyClientService()
        {
            logger = new Logger<SignifyClientService>(new LoggerFactory()); // TODO: insert via DI
        }

        public static async Task GetPostExample()
        {
            // TODO EE!  This is a placeholder example
            // Example GET request and POST request

            var httpClientService = new HttpClientService(new HttpClient());
            // GET request
            var getResult = await httpClientService.SendAsync<object, MyResponseType>(HttpMethod.Get, "https://example.com/get");
            if (getResult.IsSuccess)
            {
                // Handle successful response
            }
            else
            {
                // Handle failure
                // xxConsole.WriteLine(getResult.Reasons.First().Message);
            }

            // POST request
            var postResult = await httpClientService.SendAsync<MyRequestType, MyResponseType>(HttpMethod.Post, "https://example.com/post", new MyRequestType());
            if (postResult.IsSuccess)
            {
                // Handle successful response
            }
            else
            {
                // Handle failure
                // xxConsole.WriteLine(postResult.Reasons.First().Message);
            }
        }

        public Task<Result<HttpResponseMessage>> ApproveDelegation()
        {
            return Task.FromResult(Result.Fail<HttpResponseMessage>("Not implemented"));
        }

        public async Task<Result<State>> Boot(Url url)
        {
            // See https://github.com/WebOfTrust/signify-integration/blob/main/scripts/create_agent.py
            var httpClientService = new HttpClientService(new HttpClient());
            var postResult = await httpClientService.SendAsync<MyRequestType, MyResponseType>(HttpMethod.Post, $"{url}:{BootPort}/boot", new MyRequestType());
            if (postResult.IsSuccess)
            {
                return await Task.FromResult(Result.Ok<State>(new State()));
            }
            else
            {
                logger.LogInformation($"KERI Agent response for Post boot: {postResult.Reasons.First().Message}");
                return await Task.FromResult(Result.Fail($"KERI Agent response for Post boot: {postResult.Reasons.First().Message}"));
            }
        }

        public Task<Result<bool>> Connect()
        {
            return Task.FromResult(Result.Fail<bool>("Not implemented"));
        }

        public Task<Result<Models.Identifier>> CreatePersonAid()
        {
            return Task.FromResult(Result.Fail<Models.Identifier>("Not implemented"));
        }

        public Task<Result<HttpResponseMessage>> DeletePasscode()
        {
            return Task.FromResult(Result.Fail<HttpResponseMessage>("Not implemented"));
        }

        public Task<Result<HttpResponseMessage>> Fetch(string path, string method, object data, Dictionary<string, string>? extraHeaders)
        {
            return Task.FromResult(Result.Fail<HttpResponseMessage>("Not implemented"));
        }

        public Task<Result<IList<Challenge>>> GetChallenges()
        {
            return Task.FromResult(Result.Fail<IList<Challenge>>("Not implemented"));
        }

        public Task<Result<IList<Contact>>> GetContacts()
        {
            return Task.FromResult(Result.Fail<IList<Contact>>("Not implemented"));
        }

        public Task<Result<IList<Credential>>> GetCredentials()
        {
            return Task.FromResult(Result.Fail<IList<Credential>>("Not implemented"));
        }

        public Task<Result<IList<Escrow>>> GetEscrows()
        {
            return Task.FromResult(Result.Fail<IList<Escrow>>("Not implemented"));
        }

        public Task<Result<IList<Exchange>>> GetExchanges()
        {
            return Task.FromResult(Result.Fail<IList<Exchange>>("Not implemented"));
        }

        public Task<Result<IList<Group>>> GetGroups()
        {
            return Task.FromResult(Result.Fail<IList<Group>>("Not implemented"));
        }

        public Task<Result<IList<Models.Identifier>>> GetIdentifiers()
        {
            return Task.FromResult(Result.Fail<IList<Models.Identifier>>("Not implemented"));
        }

        public Task<Result<IList<Ipex>>> GetIpex()
        {
            return Task.FromResult(Result.Fail<IList<Ipex>>("Not implemented"));
        }

        public Task<Result<IList<KeyEvent>>> GetKeyEvents()
        {
            return Task.FromResult(Result.Fail<IList<KeyEvent>>("Not implemented"));
        }

        public Task<Result<IList<KeyState>>> GetKeyStates()
        {
            return Task.FromResult(Result.Fail<IList<KeyState>>("Not implemented"));
        }

        public Task<Result<IList<Notification>>> GetNotifications()
        {
            return Task.FromResult(Result.Fail<IList<Notification>>("Not implemented"));
        }

        public Task<Result<IList<Oobi>>> GetOobis()
        {
            return Task.FromResult(Result.Fail<IList<Oobi>>("Not implemented"));
        }

        public Task<Result<IList<Operation>>> GetOperations()
        {
            return Task.FromResult(Result.Fail<IList<Operation>>("Not implemented"));
        }

        public Task<Result<IList<Registry>>> GetRegistries()
        {
            return Task.FromResult(Result.Fail<IList<Registry>>("Not implemented"));
        }

        public Task<Result<IList<Schema>>> GetSchemas()
        {
            return Task.FromResult(Result.Fail<IList<Schema>>("Not implemented"));
        }

        public Task<Result<State>> GetState()
        {
            return Task.FromResult(Result.Fail<State>("Not implemented"));
        }

        public Task<Result<HttpResponseMessage>> Rotate(string nbran, string[] aids)
        {
            return Task.FromResult(Result.Fail<HttpResponseMessage>("Not implemented"));
        }

        public Task<Result<HttpResponseMessage>> SaveOldPasscode(string passcode)
        {
            return Task.FromResult(Result.Fail<HttpResponseMessage>("Not implemented"));
        }

        public Task<Result<HttpResponseMessage>> SignedFetch(string url, string path, string method, object data, string aidName)
        {
            return Task.FromResult(Result.Fail<HttpResponseMessage>("Not implemented"));
        }

    }
}
