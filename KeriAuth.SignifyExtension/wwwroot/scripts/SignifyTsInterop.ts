// Note the compilation of this file is bundled with its dependencies

// See the following for more inspiration:
// https://github.com/WebOfTrust/signify-browser-extension/blob/main/src/pages/background/services/signify.ts

import {
    SignifyClient,
    Tier,
    ready,
    Authenticater,
    randomPasscode,
} from "signify-ts";

export const PASSCODE_TIMEOUT = 5;

let _client: SignifyClient | null;


export const bootAndConnect = async (
    agentUrl: string,
    bootUrl: string,
    passcode: string
) => {
    try {
        await ready();
        _client = new SignifyClient(agentUrl, passcode, Tier.low, bootUrl);
        await _client.boot();
        await _client.connect();
        //const state = await getState();
        //await userService.setControllerId(state?.controller?.state?.i);
        //setTimeoutAlarm();
    } catch (error) {
        console.error(error);
        _client = null;
        return { error };
    }
};

export const connect = async (agentUrl: string, passcode: string) => {
    try {
        await ready();
        _client = new SignifyClient(agentUrl, passcode, Tier.low);
        await _client.connect();
        //const state = await getState();
        // await userService.setControllerId(state?.controller?.state?.i);
        //setTimeoutAlarm();
    } catch (error) {
        console.error(error);
        _client = null;
        return { error };
    }
};

export const GetMessage2 = () => {
    return "Hello from SignifyService 2! " + Tier.high;
}
