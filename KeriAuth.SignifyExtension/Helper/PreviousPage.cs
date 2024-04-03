using Microsoft.JSInterop;

namespace KeriAuth.SignifyExtension.Helper
{
    public class PreviousPage
    {
        public static async Task GoBack(IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("history.back");
        }
    }
}
