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
        [JSImport("bootAndConnect", "SignifyTsInterop")]
        internal static partial Task<string> BootAndConnect(string agentUrl, string bootUrl, string passcode);

        [JSImport("connect", "SignifyTsInterop")]
        internal static partial Task<string> Connect(string agentUrl, string passcode);

        [JSImport("createAID", "SignifyTsInterop")]
        internal static partial Task<string> CreateAID(string name);

        [JSImport("getAIDs", "SignifyTsInterop")]
        internal static partial Task<string> GetAIDs();
    }
}