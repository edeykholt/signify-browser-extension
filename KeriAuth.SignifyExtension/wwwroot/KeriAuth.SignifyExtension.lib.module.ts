/**
* See https://mingyaulee.github.io/Blazor.BrowserExtension/app.js 
* Called before Blazor starts.
 * @param {object} options Blazor WebAssembly start options. Refer to https://github.com/dotnet/aspnetcore/blob/main/src/Components/Web.JS/src/Platform/WebAssemblyStartOptions.ts
 * @param {object} extensions Extensions added during publishing
 * @param {object} blazorBrowserExtension Blazor browser extension instance
 */

export const prefix = "KeriAuth.SignifyExtension.lib.module.js WASM: ";

export function onRuntimeConfigLoaded(config: any) {
    console.log(`${prefix} onRuntimeConfigLoaded`); // ` config: ${JSON.stringify(config, null, 2)}`);
}

export function beforeStart(options: any, extensions: any, blazorBrowserExtension: any) {
    console.log(`${prefix} beforeStart`); // ` options: ${JSON.stringify(options, null, 2)} extensions: ${JSON.stringify(extensions, null, 2)} blazorBrowserExtension: ${JSON.stringify(blazorBrowserExtension, null, 2)}`);
}

export async function onRuntimeReady({ }) // { getAssemblyExports, getConfig }
{
    console.log(`${prefix} onRuntimeReady`);
    console.log(`${prefix} onRuntimeReady`);
    console.log(`${prefix} onRuntimeReady: importing modules... `);
    try {
        await import("./scripts/registerInactivityEvents.js");
        await import("./scripts/ui-utilities.js");
    }
    catch (error) {
        console.log(`${prefix} onRuntimeReady: error importing modules: ${error}`);
    }
    console.log(`${prefix} onRuntimeReady: imported. `);
}

/**
 * Called after Blazor is ready to receive calls from JS.
 * @param {any} blazor The Blazor instance
 */
export async function afterStarted(blazor: any) {
    console.log(`${prefix} afterStarted`);
    console.log(`${prefix} afterStarted: importing modules... `);
    try {
        //TODO EE!
        //await import("./content/MudBlazor/MudBlazor.min.js");
        //await import("./content/BlazorIndexedDB/dexie.min.js");
        //await import("./content/BlazorIndexedDB/blazorDB.js");
        //await import("./content/JsBind.Net/JsBindNet.js");
        //await import("./dist/bundle.js");
    } catch (error) {
        console.log(`${prefix} afterStarted: error importing modules: ${error}`);
    }
    console.log(`${prefix} afterStarted: imported. `);
}