// keep the exported functions and namespaces aligned with SignifyTsInterop.cs
console.log("BlazorInteropSignify...");
//import { signifyService } from "./vite_node_module_imports.js";
// assure the module is included in the bundle
// console.log("signifyService: " + signifyService.test);
// signifyService.init();
// import { signifyService  } from "@bundles/bundle.js";
export class ExampleClass {
    static staticValue = 0;
    static setStaticValue(value) {
        ExampleClass.staticValue = value;
        console.log(`Static Value set to: ${ExampleClass.staticValue}`);
    }
    static getStaticValue() {
        console.log(`Static Value got as: ${ExampleClass.staticValue}`);
        return ExampleClass.staticValue;
    }
}
export function getMessage() {
    try {
        return 'Ol� do Blazor! 1';
    }
    catch (error) {
        console.error(`SignifyTsInterop: error caught: ${error}`);
        throw error;
    }
}
export const BlazorInteropSignify = {
    // let _client: SignifyClient | null;
    // let _passcode: string;
    // let _agentUrl: string;
    getMessage: () => {
        //_client = new SignifyClient("http://localhost:5620", "passcode");
        //return _client
        return 'Ol� do Blazor! 2';
    }
};
// export { SignifyClient, Tier, ready, Authenticater } from "signify-ts";
//export async function setMessage() {
//    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
//    var exports = await getAssemblyExports("BlazorSample.dll");
//    document.getElementById("result").innerText =
//        exports.BlazorSample.JavaScriptInterop.Interop.GetMessageFromDotnet();
//}
// inspired by https://github.com/WebOfTrust/signify-browser-extension/blob/44b570a8728d62640240cf30f2f59eb31e06af70/src/pages/background/services/signify.ts
const Signify = () => {
    const test = "foo";
    const init = () => {
        console.log("SignifyTsInterop Signify signifyClient init.");
        return;
    };
    /*
        let _client: SignifyClient | null;
        let _passcode: string;
        let _agentUrl: string;
    
        const connect = async (agentUrl: string, passcode: string) => {
            _agentUrl = agentUrl;
            _passcode = passcode;
            try {
                await ready();
                _client = new SignifyClient(agentUrl, passcode, Tier.low);
                // todo Boot if needed
                await _client.connect();
    
            } catch (error) {
                console.error(error);
                _client = null;
                return { error };
            }
        };
    
        const isConnected = async () => {
            if (_agentUrl && _passcode && !_client) {
                await connect(_agentUrl, _passcode);
            }
    
            console.log(
                _client
                    ? "Signify client is connected"
                    : "Signify client is not connected", _client
            );
            return _client ? true : false;
        };
    
        const validateClient = () => {
            if (!_client) {
                throw new Error("Client not connected");
            }
        };
        const getState = async () => {
            validateClient();
            return await _client?.state();
        };
    
        const listIdentifiers = async () => {
            validateClient();
            return await _client?.identifiers().list();
        };
    
        const listCredentials = async () => {
            validateClient();
            return await _client?.credentials().list();
        };
    
        const disconnect = async () => {
            _client = null;
        };
    
        const signHeaders = async (aidName = "", origin: string) => {
            const hab = await _client?.identifiers().get(aidName);
            const keeper = _client?.manager!.get(hab);
    
            const authenticator = new Authenticater(
                keeper.signers[0],
                keeper.signers[0].verfer
            );
    
            const headers = new Headers();
            headers.set("Signify-Resource", hab.prefix);
            headers.set(
                "Signify-Timestamp",
                new Date().toISOString().replace("Z", "000+00:00")
            );
            headers.set("Origin", origin);
    
            const fields = [
                // '@method',
                // '@path',
                "signify-resource",
                "signify-timestamp",
                "origin",
            ];
    
            const signed_headers = authenticator.sign(headers, "", "", fields);
    
            return signed_headers;
        };
    
        const createAID = async (name: string) => {
            validateClient();
            let res = await _client?.identifiers().create(name);
            return await res?.op();
        };
    
                */
    return {
        init,
        test,
        // connect,
        // isConnected,
        // disconnect,
        //listIdentifiers,
        //listCredentials,
        //signHeaders,
        //createAID,
    };
};
// const signifyService = Signify();
export { Signify };
//# sourceMappingURL=SignifyTsInterop.js.map