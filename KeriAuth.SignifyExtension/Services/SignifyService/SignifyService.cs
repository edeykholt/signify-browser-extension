using FluentResults;
using JsBind.Net;

// using KeriAuth.SignifyExtension.Services.KeriaProxyService;
using KeriAuth.SignifyExtension.Services.SignifyClientService.Models;
using Microsoft.JSInterop;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using KeriAuth.SignifyExtension.Helper;
using System.Text.Json;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace KeriAuth.SignifyExtension.Services.SignifyService
{
    public class SignifyService(IJSRuntime js) : ISignifyService
    {
        private readonly IJSRuntime js = js;
        private readonly Logger<SignifyService> logger = new(new LoggerFactory()); // TODO: insert via DI
        // static public IJSObjectReference? utilModule;

        public async Task<Result> Initialize()
        {
            Console.WriteLine("SignifyService Initialize...");
            logger.LogInformation("Initialize");

            // TODO EE! any of this needed?
            // consider the first argument as nameof(KeriAuth.SignifyExtension.Services.SignifyService.SignifyTsInterop),
            try
            {
                Debug.Assert(js is not null);

                var moduleLoader = await js.InvokeAsync<IJSObjectReference>("import", "./scripts/moduleLoader.js");
                var interopHelpers = await js.InvokeAsync<IJSObjectReference>("import", "/scripts/interopHelper.js");
                var yy = await JSHost.ImportAsync("SignifyTsInterop", "/scripts/SignifyTsInterop.js");
                logger.LogInformation("SignifyTsInterop imported");

                return Result.Ok();
            }
            catch (System.Runtime.InteropServices.JavaScript.JSException ex)
            {
                // Handle JavaScript exceptions specifically (e.g., module not found, loading errors)
                logger.LogCritical("SignifyTsInterop JSException failed to import");
                logger.LogCritical("{ex}", ex.ToString());
                return Result.Fail("SignifyService: SignifyTsInterop JSException failed to import");
            }
            catch (Exception ex)
            {
                logger.LogCritical("SignifyTsInterop failed to import");
                logger.LogCritical("{ex}", ex.ToString());
                return Result.Fail("SignifyService: SignifyTsInterop failed to import");
            }
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

        public Result Disconnect()
        {
            throw new NotImplementedException();
        }

        public Result<object> GetState()
        {
            throw new NotImplementedException();
        }

        public Result<bool> IsConnected()
        {
            throw new NotImplementedException();
        }

        public Result<object> ListIdentifiers()
        {
            throw new NotImplementedException();
        }

        public Result<object> SignHeaders(string aidName, string method, string path, string origin)
        {
            throw new NotImplementedException();
        }

        public SignifyClient SignifyClient(byte[] bran)
        {
            throw new NotImplementedException();
        }
    }
}
