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
    public static async Task Copy(IJSObjectReference utilsModule, string str)
    {
        Debug.Assert(utilsModule is not null);
        Debug.Assert(str is not null);
        // Note copy2Clipboard performs a permission check
        await utilsModule.InvokeVoidAsync("utils.copy2Clipboard", str);
    }
}