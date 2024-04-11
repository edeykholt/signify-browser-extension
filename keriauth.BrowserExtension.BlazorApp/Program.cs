using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace keriauth.BrowserExtension.BlazorApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.UseBrowserExtension(browserExtension =>
            {
                builder.RootComponents.Add<App>("#app");
                builder.RootComponents.Add<HeadOutlet>("head::after");
            });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            if (OperatingSystem.IsBrowser())
            {
                try
                {
                    await JSHost.ImportAsync("signifyTsInterop", "/scripts/signifyTsInterop.js");
                    Console.WriteLine("Program: imported signifyTsInterop");

                    //// test via C# Interop class
                    //Console.WriteLine("Program: testing GetMessageFromJs... ");
                    //string message = keriauth.BrowserExtension.BlazorApp.SignifyTsInterop.GetMessage();
                }
                catch (Microsoft.JSInterop.JSException e)
                {
                    Console.WriteLine("App: Initialize: JSInterop.JSException: " + e.StackTrace);
                }
                catch (System.Runtime.InteropServices.JavaScript.JSException e)
                {
                    Console.WriteLine("App: Initialize: JSException: " + e.StackTrace);
                }
                catch (Exception e)
                {
                    Console.WriteLine("App: Initialize: Exception: " + e);
                }
            }

            await builder.Build().RunAsync();
        }
    }
}
