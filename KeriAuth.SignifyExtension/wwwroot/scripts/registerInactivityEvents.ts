export function registerInactivityTimerResetEvents(dotNetReference: any) {
    document.onmousemove = () => {
        dotNetReference.invokeMethodAsync("ResetInactivityTimer")
            .then((result: any) => {
                // console.log(result);
            })
            .catch((error: any) => {
                console.error(error);
            });            ;
    };
    document.onkeydown = () => {
        dotNetReference.invokeMethodAsync("ResetInactivityTimer")
            .then((result: any) => {
                // console.log(result);
            })
            .catch((error: any) => {
                console.error(error);
            });            ;
    };
}
(window as any).registerInactivityTimerResetEvents = registerInactivityTimerResetEvents;