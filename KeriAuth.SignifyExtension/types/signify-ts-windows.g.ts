declare global {
    interface Window {
        myCustomObject: {
            myProperty: string;
            myMethod: () => void;
        };
    }
}

// Ensure this file is a module by exporting an empty object if necessary.
export { };