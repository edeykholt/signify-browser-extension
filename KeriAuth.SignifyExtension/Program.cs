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

// note Program and Main are implicit and static

// Intentionally using Console.WriteLine herein since ILogger isn't yet easy to inject
Console.WriteLine("Program: started");

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

builder.Services.AddSingleton<IExtensionEnvironmentService, ExtensionEnvironmentService>();
builder.Services.AddSingleton<IWalletService, WalletService>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddSingleton<IStateService, StateService>();
builder.Services.AddSingleton<IAlarmService, AlarmService>();
builder.Services.AddSingleton<IPreferencesService, PreferencesService>();
builder.Services.AddSingleton<ISignifyClientService, SignifyClientService>(); // TODO really need?
builder.Services.AddSingleton<ISignifyService, SignifyService>();

var host = builder.Build();

// Import JS modules for use in C# classes
Debug.Assert(OperatingSystem.IsBrowser());
try
{
    Console.WriteLine("Program: Importing JS modules...");
    // Adding imports of modules here for use via [JSImport] attributes in C# classes
    List<(string, string)> imports = [
        // ("signify-ts", "/node_modules/signify-ts"),
        ("signifyTsInterop", "/scripts/signifyTsInterop.js"),
        ("registerInactivityEvents", "/scripts/registerInactivityEvents.js"),
        ("uiHelper", "/scripts/uiHelper.js")
    ];
    foreach (var (moduleName, modulePath) in imports)
    {
        Console.WriteLine("Program: importing " + moduleName);
        await JSHost.ImportAsync(moduleName, modulePath);
    }
    Console.WriteLine("Program: Imported.");
}
catch (Microsoft.JSInterop.JSException e)
{
    Console.WriteLine("Program: Initialize: JSInterop.JSException: " + e.StackTrace);
    return;
}
catch (System.Runtime.InteropServices.JavaScript.JSException e)
{
    Console.WriteLine("Program: Initialize: JSException: " + e.StackTrace);
    return;
}
catch (Exception e)
{
    Console.WriteLine("Program: Initialize: Exception: " + e);
    return;
}


// Run
Console.WriteLine("Program: Running WASM Host...");
await host.RunAsync();
