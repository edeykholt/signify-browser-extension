/// <reference types="chrome" />

const Utils = () => {

    // Scroll to an element on the page
    const bt_scrollToItem = (elementId: string): void => {
        var elem = document.getElementById(elementId);
        if (elem) {
            elem.scrollIntoView();
            window.location.hash = elementId;
        }
    }

    const closeCurrentTab = (): void => {
        window.close();
    }

    const newTabAndClosePopup = (): void => {
        const currentTab: Window = window;
        // const location: Location = currentTab.location;
        createTab("/index.html");
        currentTab.close();
    }

    // TODO DRY with createTab in service-worker.ts
    const createTab = (urlString: string): void => {
        console.log("WORKER: ui-utilities creating tab: " + urlString);
        var createProperties = { url: urlString } as chrome.tabs.CreateProperties;
        chrome.tabs.create(createProperties);
    }

    const copy2Clipboard = async (text: string): Promise<void> => {
        // permissions are only relevant when running in the context of an extension (versus development mode when testing in Kestral ASP.NET Core, for example)
        if (chrome.permissions && typeof chrome.permissions.contains === 'function') {
            // Various browsers support different defaults for permissions. Some implicitly granted permission.
            chrome.permissions.contains({ permissions: ["clipboardWrite"] }, (isClipboardPermitted: boolean) => {
                console.log('WORKER: copy2Clipboard: isClipboardPermitted: ', isClipboardPermitted);
                if (!isClipboardPermitted) {
                    // Request permission from the user
                    chrome.permissions.request(
                        {
                            permissions: ["clipboard-write"]
                        }, (isGranted: boolean) => {
                            if (isGranted) {
                                console.log('WORKER: Clipboard permission granted');
                            } else {
                                console.log('WORKER: Clipboard permission denied');
                                return;
                            }
                        }
                    );
                };
            });
        }
        navigator.clipboard.writeText(text).then(
            function () {
                console.log('WORKER: Async: Copying to clipboard was successful!');
            }, function (err) {
                console.error('WORKER: Async: Could not copy text: ', err);
            }
        );
    }

    // return all functions
    return {
        bt_scrollToItem,
        closeCurrentTab,
        newTabAndClosePopup,
        createTab,
        copy2Clipboard,
    };
}
export const utils = Utils();