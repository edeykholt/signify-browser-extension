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

/**
 * Connect or boot a SignifyClient instance
 * @param agentUrl
 * @param bootUrl
 * @param passcode
 * @returns
 */
// rename to getOrCreateClient?  See https://github.com/WebOfTrust/signify-ts/blob/fddaff20f808b9ccfed517b3a38bef3276f99261/examples/integration-scripts/utils/test-setup.ts#L13
export const bootAndConnect = async (
    agentUrl: string,
    bootUrl: string,
    passcode: string
) : Promise<SignifyClient> => {
    _client = null;
    await ready();
    console.log(`bootAndConnect: creating client...`);
    _client = new SignifyClient(agentUrl, passcode.padEnd(21, '_'), Tier.low, bootUrl);

    try {
        await _client.connect();
        console.info("client connected");
    } catch {
        const res = await _client.boot();
        if (!res.ok) throw new Error();
        await _client.connect();
        console.info("client booted and connected");
    }
    console.log('client', {
        agent: _client.agent?.pre,
        controller: _client.controller.pre,
    });
    const state = await getState();
    console.log(`bootAndConnect: connected`);
    console.assert(state?.controller?.state?.i != null, "controller id is null"); // TODO throw exception?

    return _client;
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
