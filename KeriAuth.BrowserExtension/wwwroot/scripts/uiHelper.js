/// <reference types="chrome" />
// uiUtnils.ts
const UIHelper = () => {
    // Scroll to an element on the page
    const bt_scrollToItem = (elementId) => {
        var elem = document.getElementById(elementId);
        if (elem) {
            elem.scrollIntoView();
            window.location.hash = elementId;
        }
    };
    const closeCurrentTab = () => {
        window.close();
    };
    const newTabAndClosePopup = () => {
        const currentTab = window;
        // const location: Location = currentTab.location;
        createTab("/index.html");
        currentTab.close(); // TODO: assure this is called only for a popup window
    };
    const createTab = (urlString) => {
        console.log("UIHelper: creating tab: " + urlString);
        var createProperties = { url: urlString };
        if (typeof (chrome.tabs) == 'undefined') {
            console.error('UIHelper: chrome.tabs is not available');
        }
        else {
            chrome.tabs.create(createProperties);
        }
    };
    const copy2Clipboard = async (text) => {
        // permissions are only relevant when running in the context of an extension (versus development mode when testing in Kestral ASP.NET Core, for example)
        if (chrome.permissions && typeof chrome.permissions.contains === 'function') {
            // Various browsers support different defaults for permissions. Some implicitly grant permission, or was previously granted
            chrome.permissions.contains({ permissions: ["clipboardWrite"] }, (isClipboardPermitted) => {
                console.log('UIHelper: copy2Clipboard: isClipboardPermitted: ', isClipboardPermitted);
                if (!isClipboardPermitted) {
                    // Request permission from the user
                    chrome.permissions.request({
                        permissions: ["clipboardWrite"]
                    }, (isGranted) => {
                        if (isGranted) {
                            console.log('UI-UTILITIES: Clipboard permission granted');
                        }
                        else {
                            console.log('UI-UTILITIES: Clipboard permission denied');
                            return;
                        }
                    });
                }
                ;
            });
        }
        navigator.clipboard.writeText(text).then(function () {
            console.log('UIHelper: Copied to clipboard');
        }, function (err) {
            console.error('UIHelper: Could not copy to clipboard: ', err);
        });
    };
    return {
        bt_scrollToItem,
        closeCurrentTab,
        newTabAndClosePopup,
        createTab,
        copy2Clipboard,
    };
};
export const Utils = UIHelper();
//# sourceMappingURL=uiHelper.js.map