// Note the compilation of this file is bundled with its dependencies

// See the following for more inspiration:
// https://github.com/WebOfTrust/signify-browser-extension/blob/main/src/pages/background/services/signify.ts

//import {
//    SignifyClient,
//    Tier,
//    ready,
//    Authenticater,
//    randomPasscode,
//} from "signify-ts";

const PASSCODE_TIMEOUT = 5;

const Signify = () => {
    //let _client: SignifyClient | null;


    //const bootAndConnect = async (
    //    agentUrl: string,
    //    bootUrl: string,
    //    passcode: string
    //) => {
    //    try {
    //        await ready();
    //        _client = new SignifyClient(agentUrl, passcode, Tier.low, bootUrl);
    //        await _client.boot();
    //        await _client.connect();
    //        //const state = await getState();
    //        //await userService.setControllerId(state?.controller?.state?.i);
    //        //setTimeoutAlarm();
    //    } catch (error) {
    //        console.error(error);
    //        _client = null;
    //        return { error };
    //    }
    //};

    //const connect = async (agentUrl: string, passcode: string) => {
    //    try {
    //        await ready();
    //        _client = new SignifyClient(agentUrl, passcode, Tier.low);
    //        await _client.connect();
    //        //const state = await getState();
    //        // await userService.setControllerId(state?.controller?.state?.i);
    //        //setTimeoutAlarm();
    //    } catch (error) {
    //        console.error(error);
    //        _client = null;
    //        return { error };
    //    }
    //};

    const GetMessage = async () : Promise<string> => {
        return "Hello from SignifyService!";
    }

    return {
        // // connect,
        //isConnected,
        //disconnect,
        //listIdentifiers,
        //listCredentials,
        //signHeaders,
        //createAID,
        //generatePasscode,
        // // bootAndConnect,
        //getControllerID,
        //getSignedHeaders,
        GetMessage
    };
};

//export const getMessage2 = async (): Promise<string> => {
//    return "Hello from getMessage2 SignifyTsInterop.ts";
//}

export const signifyService = Signify();

export const GetMessage2 = async (): Promise<string> => {
    return "Hello from SignifyService 2!";
}
