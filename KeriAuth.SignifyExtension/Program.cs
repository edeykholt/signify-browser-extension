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
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using MudBlazor.Services;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;

namespace KeriAuth.SignifyExtension
{
    public class Program
    {
        public static readonly ILogger<Program> logger = new Logger<Program>(new LoggerFactory()); // TODO: insert via DI
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Logging.AddConfiguration(
                builder.Configuration.GetSection("Logging")
            );
            // See appsettings.json for Logging settings
            // builder.Logging.SetMinimumLevel(LogLevel.Debug);

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
                    //var iLoggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                    //Debug.Assert(iLoggerFactory is not null);

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
            // builder.Services.AddScoped<IStorageService, StorageService>();
            builder.Services.AddSingleton<IExtensionEnvironmentService, ExtensionEnvironmentService>();
            builder.Services.AddSingleton<IWalletService, WalletService>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddSingleton<IStateService, StateService>();
            builder.Services.AddSingleton<IAlarmService, AlarmService>();
            builder.Services.AddSingleton<IPreferencesService, PreferencesService>();
            builder.Services.AddSingleton<ISignifyClientService, SignifyClientService>(); // TODO really need?
            builder.Services.AddSingleton<ISignifyService, SignifyService>();
            // builder.Services.AddSingleton<ILoggerFactory, SignifyService>();

            var host = builder.Build();

            //var logger = host.Services.GetRequiredService<ILoggerFactory>()
            //    .CreateLogger<Program>();

            //// Resolve the services depending on ILoggerFactory
            //using (var scope = host.Services.CreateScope())
            //{
            //    var storageService = scope.ServiceProvider.GetRequiredService<IStorageService>();
            //    // initialize if needed
            //    // storageService.Initialize(host.Services.GetRequiredService<IWebExtensionsApi>());
            //}

            Debug.Assert(OperatingSystem.IsBrowser());
            
                try
                {
                    logger.LogInformation("Importing JS modules...");

                    // Adding imports of modules here for use via [JSImport] attributes in C# classes
                    await JSHost.ImportAsync("signifyTsInterop", "/scripts/signifyTsInterop.js");
                    logger.LogInformation("signifyTsInterop");
                    // test via C# Interop class
                    // logger.LogInformation("Program: testing GetMessageFromJs... ");
                    // string message = KeriAuth.SignifyExtension.Services.SignifyService.SignifyTsInterop.GetMessageFromJs();
                    // logger.LogInformation("Program: " + message);

                    await JSHost.ImportAsync("uiHelper", "/scripts/uiHelper.js");
                    logger.LogInformation("uiHelper");
                    // test via C# Interop class
                    // logger.LogInformation("Program: testing Copy2Clipboard ");
                    // await KeriAuth.SignifyExtension.Helper.UIHelper.Copy2Clipboard("Yo Momma");
                    // logger.LogInformation("Program: copied to clipboard");

                    await JSHost.ImportAsync("registerInactivityEvents", "/scripts/registerInactivityEvents.js");
                    logger.LogInformation("registerInactivityEvents");
                }
                catch (Microsoft.JSInterop.JSException e)
                {
                    logger.LogCritical("Initialize: JSInterop.JSException: " + e.StackTrace);
                    return;
                }
                catch (System.Runtime.InteropServices.JavaScript.JSException e)
                {
                    logger.LogCritical("Initialize: JSException: " + e.StackTrace);
                    return;
                }
                catch (Exception e)
                {
                    logger.LogCritical("Initialize: Exception: " + e);
                return;
            }


            Console.WriteLine($"{nameof(Program)}: Running WASM Host...");
            // TODO why isn't logger.LogInformation working here?
            Program.logger.LogInformation("Running WASM Host...");

            await host.RunAsync();
        }
    }
}