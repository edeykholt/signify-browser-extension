using BlazorDB;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using KeriAuth.SignifyExtension;
using KeriAuth.SignifyExtension.Services;
using KeriAuth.SignifyExtension.Services.SignifyClientService;
using KeriAuth.SignifyExtension.Services.SignifyService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using MudBlazor.Services;
using System.Runtime.InteropServices.JavaScript;

namespace KeriAuth.SignifyExtension
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine($"{nameof(Program)}: Building WASM Host...");

            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.UseBrowserExtension(browserExtension =>
            {
                builder.RootComponents.Add<KeriAuth.SignifyExtension.App>("#app");
                builder.RootComponents.Add<HeadOutlet>("head::after");
            });

            builder.Services.AddBrowserExtensionServices();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();

            // construct storage service dependent on whether we are running in a browser extension or not
            // TODO.  Also consider creating helpers:
            // isBrowserWASM = System.OperatingSystem.IsBrowser()
            // approx...  isExtension = JSInterop.RunAsync<Bool>("if (chrome.extension)")
            if (builder.HostEnvironment.BaseAddress.Contains("chrome-extension"))
            {
                builder.Services.AddSingleton<IStorageService>(serviceProvider =>
                {
                    var jsRuntime = serviceProvider.GetRequiredService<IJSRuntime>();
                    var hostEnvironment = serviceProvider.GetRequiredService<IWebAssemblyHostEnvironment>();
                    return new StorageService(jsRuntime, hostEnvironment, null, null);
                });
            }
            else // WASM hosted, e.g. in developer's Kestrel ASPNetCore or IISExpress
            {
                builder.Services.AddBlazoredLocalStorageAsSingleton();
                builder.Services.AddBlazoredSessionStorageAsSingleton();
                builder.Services.AddSingleton<IStorageService>(serviceProvider =>
                {
                    var jsRuntime = serviceProvider.GetRequiredService<IJSRuntime>();
                    var hostEnvironment = serviceProvider.GetRequiredService<IWebAssemblyHostEnvironment>();
                    var localStorage = serviceProvider.GetRequiredService<ILocalStorageService>();
                    var sessionStorage = serviceProvider.GetRequiredService<ISessionStorageService>();
                    return new StorageService(jsRuntime, hostEnvironment, localStorage, sessionStorage);
                });
            }
            builder.Services.AddSingleton<IExtensionEnvironmentService, ExtensionEnvironmentService>();
            builder.Services.AddSingleton<IWalletService, WalletService>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddSingleton<IStateService, StateService>();
            builder.Services.AddSingleton<IAlarmService, AlarmService>();
            builder.Services.AddSingleton<IPreferencesService, PreferencesService>();
            builder.Services.AddSingleton<ISignifyClientService, SignifyClientService>(); // TODO really need?
            builder.Services.AddSingleton<ISignifyService, SignifyService>();

            // Add JSInterop
            if (OperatingSystem.IsBrowser())
            {
                try
                {
                    Console.WriteLine("Program: Importing JS modules...");

                    // Adding imports of modules here for use via [JSImport] attributes in C# classes
                    await JSHost.ImportAsync("signifyTsInterop", "/scripts/signifyTsInterop.js");
                    Console.WriteLine("Program: signifyTsInterop");
                    // test via C# Interop class
                    // Console.WriteLine("Program: testing GetMessageFromJs... ");
                    // string message = KeriAuth.SignifyExtension.Services.SignifyService.SignifyTsInterop.GetMessageFromJs();
                    // Console.WriteLine("Program: " + message);

                    await JSHost.ImportAsync("uiHelper", "/scripts/uiHelper.js");
                    Console.WriteLine("Program: uiHelper");
                    // test via C# Interop class
                    // Console.WriteLine("Program: testing Copy2Clipboard ");
                    // await KeriAuth.SignifyExtension.Helper.UIHelper.Copy2Clipboard("Yo Momma");
                    // Console.WriteLine("Program: copied to clipboard");

                    await JSHost.ImportAsync("registerInactivityEvents", "/scripts/registerInactivityEvents.js");
                    Console.WriteLine("Program: registerInactivityEvents");
                }
                catch (Microsoft.JSInterop.JSException e)
                {
                    Console.WriteLine("Program: Initialize: JSInterop.JSException: " + e.StackTrace);
                }
                catch (System.Runtime.InteropServices.JavaScript.JSException e)
                {
                    Console.WriteLine("Program: Initialize: JSException: " + e.StackTrace);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Program: Initialize: Exception: " + e);
                }
            }

            Console.WriteLine($"{nameof(Program)}: Running WASM Host...");

            await builder.Build().RunAsync();
        }
    }
}