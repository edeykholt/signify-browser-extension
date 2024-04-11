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

Console.WriteLine($"{nameof(Program)}: WASM Host starting");

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
} else // WASM hosted, e.g. in developer's Kestrel ASPNetCore or IISExpress
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

Console.WriteLine($"{nameof(Program)}: Running...");

await builder.Build().RunAsync();