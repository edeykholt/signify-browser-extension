using KeriAuth.SignifyExtension.Services;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Diagnostics;
using static MudBlazor.FilterOperator;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KeriAuth.SignifyExtension.Helper;

public class CopyToClipboard
{
    public static async Task Copy(IJSRuntime jsRuntime, string str)
    {
        Debug.Assert(jsRuntime is not null);
        Debug.Assert(str is not null);
        await jsRuntime.InvokeVoidAsync("copy2Clipboard", str);
    }
}