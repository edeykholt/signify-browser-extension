using FluentResults;
using KeriAuth.SignifyExtension.Services.SignifyClientService.Models;
using MudBlazor;
using System;

namespace KeriAuth.SignifyExtension.Services.SignifyService
{
    interface ISignifyService
    {
        public SignifyClient SignifyClient(byte[] bran); // TODO add security param

        public Task<Result> Initialize();

        public Task<Result<bool>> connect(string url, string passcode, string? boot_url = null, bool isBootForced = false);

        public Result<bool> isConnected();
    
        public Result disconnect();

        public Result<object> getState(); // TODO add return type

        public Result<object> listIdentifiers(); // TODO add return type
    
        public Result<object> signHeaders(String aidName, String method, String path, String origin);  // TODO add return type
    }
}
