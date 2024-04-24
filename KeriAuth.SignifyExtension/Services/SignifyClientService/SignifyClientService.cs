using FluentResults;

using KeriAuth.SignifyExtension.Services.SignifyClientService;
using static KeriAuth.SignifyExtension.Services.SignifyService.SignifyServiceConfig;
using KeriAuth.SignifyExtension.Services;
using KeriAuth.SignifyExtension.Services.SignifyClientService.Models;
using static KeriAuth.SignifyExtension.Services.SignifyService.SignifyTsInterop;
using KeriAuth.SignifyExtension.Models;
using State = KeriAuth.SignifyExtension.Services.SignifyClientService.Models.State;
using Group = KeriAuth.SignifyExtension.Services.SignifyClientService.Models.Group;



using System.Text.RegularExpressions;
using WebExtensions.Net.Tabs;
using Microsoft.Extensions.Logging;

using Microsoft.Win32;
using System.Reactive;
using static KeriAuth.SignifyExtension.UI.Views.Create;
using KeriAuth.SignifyExtension.Services.SignifyService;
using System.Linq.Expressions;
using System.Runtime.InteropServices.JavaScript;
using System.Diagnostics;
using System.Runtime.Versioning;


namespace KeriAuth.SignifyExtension.Services.SignifyClientService
{
    public class SignifyClientService() : ISignifyClientService
    {
        private readonly ILogger<SignifyClientService> logger = new Logger<SignifyClientService>(new LoggerFactory());

        public Task<Result<HttpResponseMessage>> ApproveDelegation()
        {
            return Task.FromResult(Result.Fail<HttpResponseMessage>("Not implemented"));
        }

        public async Task<Result> HealthCheck(Url fullUrl)
        {
            var httpClientService = new HttpClientService(new HttpClient());
            var postResult = await httpClientService.GetJsonAsync<String>(fullUrl);
            return postResult.IsSuccess ? Result.Ok() : Result.Fail(postResult.Reasons.First().Message);
        }

        public async Task<Result<bool>> Connect(string agentUrl, string passcode, string? bootUrl, bool isBootForced = false)
        {
            Debug.Assert(bootUrl is not null);
            await Task.Delay(0);
            Console.WriteLine("SignifyService: connect()...");
            // Console.WriteLine($"SignifyService: passcode: {passcode}");
            logger.LogInformation("connect()...");

            // simple example of using https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-javascript-from-dotnet?view=aspnetcore-8.0
            if (OperatingSystem.IsBrowser())
            {
                var res = await SignifyTsInterop.BootAndConnect(agentUrl, bootUrl, passcode);
                Debug.Assert(res is not null);
                // TODO EE! this exposes the passcode bran in the console
                Console.WriteLine("SignifyService: connect: res: " + res);
            }

            // TODO fix
            return true.ToResult<bool>();
        }

        public async Task<Result<ClientState>> BootAndConnect(Url url, String BootPort, string passcode)
        {
            Debug.Assert(url is not null);
            try
            {
                string agentUrl = url.ToString()!;
                var ClientRes = await SignifyTsInterop.BootAndConnect(agentUrl, $"{url}:{BootPort}/boot", "passcode");
                if (ClientRes is not null)
                {
                    // TODO EE!: parse what we need from ClientRes json
                    return Result.Ok<ClientState>(new ClientState());
                }
                else
                {
                    return Result.Fail<ClientState>("SignifyClientService: BootAndConnect: ClientRes is null");
                }
            }
            catch (JSException e)
            {
                logger.LogInformation("SignifyClientService: BootAndConnect: JSException: {e}", e);
                return Result.Fail<ClientState>("SignifyClientService: BootAndConnect: Exception: " + e);
            }
            catch (Exception e)
            {
                logger.LogInformation("SignifyClientService: BootAndConnect: Exception: {e}", e);
                return Result.Fail<ClientState>("SignifyClientService: BootAndConnect: Exception: " + e);
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

        public static Task<Result<IList<Credential>>> GetCredentials()
        {
            return Task.FromResult(Result.Fail<IList<Credential>>("Not implemented"));
        }

        public Task<Result<IList<Services.SignifyClientService.Models.Escrow>>> GetEscrows()
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

        public static Task<Result<IList<Models.Notification>>> GetNotifications()
        {
            return Task.FromResult(Result.Fail<IList<Models.Notification>>("Not implemented"));
        }

        public Task<Result<IList<Oobi>>> GetOobis()
        {
            return Task.FromResult(Result.Fail<IList<Services.SignifyClientService.Models.Oobi>>("Not implemented"));
        }

        public Task<Result<IList<Operation>>> GetOperations()
        {
            return Task.FromResult(Result.Fail<IList<Operation>>("Not implemented"));
        }

        public static Task<Result<IList<Models.Registry>>> GetRegistries()
        {
            return Task.FromResult(Result.Fail<IList<Models.Registry>>("Not implemented"));
        }

        public Task<Result<IList<Services.SignifyClientService.Models.Schema>>> GetSchemas()
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

        Task<Result> ISignifyClientService.BootPort(Url url)
        {
            throw new NotImplementedException();
        }

        Task<Result<State>> ISignifyClientService.Boot(Url url)
        {
            throw new NotImplementedException();
        }

        Task<Result<IList<Models.Credential>>> ISignifyClientService.GetCredentials()
        {
            throw new NotImplementedException();
        }

        Task<Result<IList<Models.Registry>>> ISignifyClientService.GetRegistries()
        {
            throw new NotImplementedException();
        }

        Task<Result<IList<Models.Notification>>> ISignifyClientService.GetNotifications()
        {
            throw new NotImplementedException();
        }
    }
}
