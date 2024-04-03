export declare class ExampleClass {
    static staticValue: number;
    static setStaticValue(value: number): void;
    static getStaticValue(): number;
}
export declare function getMessage(): string;
export declare const BlazorInteropSignify: {
    getMessage: () => string;
};
declare const Signify: () => {
    init: () => void;
    test: string;
};
export { Signify };
