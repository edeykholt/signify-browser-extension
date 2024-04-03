export function registerInactivityTimerResetEvents(dotNetReference) {
    document.onmousemove = () => {
        dotNetReference.invokeMethodAsync("ResetInactivityTimer")
            .then((result) => {
            // console.log(result);
        })
            .catch((error) => {
            console.error(error);
        });
        ;
    };
    document.onkeydown = () => {
        dotNetReference.invokeMethodAsync("ResetInactivityTimer")
            .then((result) => {
            // console.log(result);
        })
            .catch((error) => {
            console.error(error);
        });
        ;
    };
}
window.registerInactivityTimerResetEvents = registerInactivityTimerResetEvents;
//# sourceMappingURL=registerInactivityEvents.js.map