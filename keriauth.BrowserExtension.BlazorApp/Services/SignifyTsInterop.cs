// using Microsoft.JSInterop;
// using MudBlazor;
using System;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices.JavaScript;

namespace keriauth.BrowserExtension.BlazorApp;

[SupportedOSPlatform("browser")]
public partial class SignifyTsInterop
{
    //private readonly IJSRuntime jSRuntime;
    //public SignifyTsInterop(IJSRuntime jSRuntime)
    //{
    //    this.jSRuntime = jSRuntime;
    //}

    // keep the imported method and property names aligned with SignifyTsInterop.ts
    
    [JSImport("getMessage", "signifyTsInterop")] // "/scripts/SignifyTsInterop.js")]
    internal static partial string GetMessage();
}
