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
        [JSImport("getMessage", "SignifyTsInterop")]
        internal static partial string GetMessageFromJs();

        [JSImport("BlazorInteropSignify.getMessage", "SignifyTsInterop")]
        internal static partial string GetMessageFromJs2();

        [JSImport("ExampleClass.setStaticValue", "SignifyTsInterop")]
        public static partial void SetStaticValue(int value);

        [JSImport("ExampleClass.getStaticValue", "SignifyTsInterop")]
        public static partial int GetStaticValue();

        //        [JSExport]
        //        internal static string Task<void> connect(agentUrl: string, passcode: string)
        //        {

        //        }

        //            [JSExport]
        //isConnected() : Task<bool>

        //            [JSExport]
        //validateClient() : void

        //            [JSExport]
        //        getState() : Task<JSONDocument> // State

        //            [JSExport]
        //listIdentifiers() : Task<object> // IList<Identifier>

        //            [JSExport]
        //listCredentials() : Task<object> // IList<Credentials>

        //            [JSExport]
        //disconnect(): Task<void>

        //            [JSExport]
        //signHeaders(aidName: string, origin: string) : Task<object> // Task<Headers>

        //            [JSExport]
        //createAID(name: string) : Task<any>



    }
}