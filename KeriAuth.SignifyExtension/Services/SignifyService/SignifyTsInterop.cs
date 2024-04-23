using MudBlazor;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Xml.Linq;

namespace KeriAuth.SignifyExtension.Services.SignifyService
{
    // keep the imported method and property names aligned with SignifyTsInterop.ts
    [SupportedOSPlatform("browser")]
    public partial class SignifyTsInterop
    {
        [JSImport("GetMessage2", "signifyTsInterop")]
        internal static partial Task<string> GetMessageFromJs();

        // don't forget about promises for other methods
    }
}