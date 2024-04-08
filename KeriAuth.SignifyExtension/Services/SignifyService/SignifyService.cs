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

namespace KeriAuth.SignifyExtension.Services.SignifyService
{
    public class SignifyService(IJSRuntime js) : ISignifyService
    {
        private readonly IJSRuntime js = js;

        private JSObject? _module8;
        // private JSObject? _module9;

        static public IJSObjectReference? utilModule;

        public async Task<Result> Initialize()
        {
            Console.WriteLine("SignifyService: Initialize");

            // consider the first argument as nameof(KeriAuth.SignifyExtension.Services.SignifyService.SignifyTsInterop),


            try
            {
                Debug.Assert(js is not null);

                var moduleLoader = await js.InvokeAsync<IJSObjectReference>("import", "./scripts/moduleLoader.js");
                var loadResult = await moduleLoader.InvokeAsync<object>("loadModule", "./ui-utilities.js");

                if (loadResult != null && ((JsonElement)loadResult).GetProperty("success").GetBoolean())
                {
                    Console.WriteLine("SignifyService: Module loaded successfully.");
                    // If you need to use the module further, you can do so here.
                    utilModule = await js.InvokeAsync<IJSObjectReference>("import", "./scripts/ui-utilities.js");
                    // await js.InvokeVoidAsync("utils.log", "Module loaded successfully.");
                }
                else
                {
                    Debug.Assert(loadResult is not null);
                    var msg = ((JsonElement)loadResult).GetProperty("error");
                    Console.WriteLine($"Failed to load module. {msg}");
                }

                var interopHelpers = await js.InvokeAsync<IJSObjectReference>("import", "/scripts/interopHelper.js");

                // Use the helper function to list the module's exports
                //var exports = await interopHelpers.InvokeAsync<string[]>("listModuleExports", module99);

                //foreach (var exportName in exports)
                //{
                //    Console.WriteLine($"Exported member: {exportName}");
                //}

                var yy = await JSHost.ImportAsync("SignifyTsInterop", "/scripts/SignifyTsInterop.js");

                _module8 = yy;
                Console.WriteLine("SignifyService: SignifyTsInterop imported");

                string message = SignifyTsInterop.GetMessageFromJs();
                Console.WriteLine("SignifyService: GetMessageFromJs: " + message);

                //string message2 = SignifyTsInterop.GetMessageFromJs2();
                //Console.WriteLine("GetMessageFromJs 2: " + message);

                //var qwer = SignifyTsInterop.GetStaticValue();
                //SignifyTsInterop.SetStaticValue(qwer + 1);

                return Result.Ok();
            }
            catch (System.Runtime.InteropServices.JavaScript.JSException ex)
            {
                // Handle JavaScript exceptions specifically (e.g., module not found, loading errors)
                Console.WriteLine("SignifyService: SignifyTsInterop JSException failed to import");
                Console.WriteLine(ex);
                return Result.Fail("SignifyService: SignifyTsInterop JSException failed to import");
            }
            catch (Exception e)
            {
                Console.WriteLine("SignifyService: SignifyTsInterop failed to import");
                Console.WriteLine(e);
                return Result.Fail("SignifyService: SignifyTsInterop failed to import");
            }
        }


        public async Task<Result<bool>> connect(string url, string passcode, string? boot_url = null, bool isBootForced = false)
        {
            await Task.Delay(0);
            Console.WriteLine($"{nameof(SignifyService)}: connect()...");

            // Use the module to create an instance of SignifyClient
            if (_module8 is null)
            {
                Console.WriteLine("SignifyService: connect: _module8 is null");
                return false.ToResult<bool>();
            }
            /*
            var signifyClientInstance = await _module8.InvokeAsync<JsonElement>("newSignifyClient", "http://localhost", "123456789012345678901");
            Console.WriteLine("signifyClientInstance: ");
            Console.WriteLine(signifyClientInstance);
            */

            // simple example of using https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-javascript-from-dotnet?view=aspnetcore-8.0
            if (OperatingSystem.IsBrowser())
            {
                string message = SignifyTsInterop.GetMessageFromJs();
                Console.WriteLine("GetMessageFromJs: " + message);
            }

            // TODO fix
            return true.ToResult<bool>();
        }

        public Result disconnect()
        {
            throw new NotImplementedException();
        }

        public Result<object> getState()
        {
            throw new NotImplementedException();
        }

        public Result<bool> isConnected()
        {
            throw new NotImplementedException();
        }

        public Result<object> listIdentifiers()
        {
            throw new NotImplementedException();
        }

        public Result<object> signHeaders(string aidName, string method, string path, string origin)
        {
            throw new NotImplementedException();
        }

        public SignifyClient SignifyClient(byte[] bran)
        {
            throw new NotImplementedException();
        }
    }

    //[SupportedOSPlatform("browser")]
    //public partial class SignifyTsInterop
    //{
    //    [JSImport("getMessage", "CallJavaScript1")]
    //    internal static partial string GetWelcomeMessage();
    //}
}
