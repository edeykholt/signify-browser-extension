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

        public Task<Result<bool>> Connect(string url, string passcode, string? boot_url = null, bool isBootForced = false);

        public Result<bool> IsConnected();
    
        public Result Disconnect();

        public Result<object> GetState(); // TODO add return type

        public Result<object> ListIdentifiers(); // TODO add return type
    
        public Result<object> SignHeaders(String aidName, String method, String path, String origin);  // TODO add return type
    }
}
