using FluentResults;
using KeriAuth.SignifyExtension.Services.SignifyClientService.Models;
using MudBlazor;
using System;

namespace KeriAuth.SignifyExtension.Services.SignifyService
{
    interface ISignifyService
    {

        public Task<Result<bool>> Connect(string url, string passcode, string? boot_url = null, bool isBootForced = false);

    }
}
