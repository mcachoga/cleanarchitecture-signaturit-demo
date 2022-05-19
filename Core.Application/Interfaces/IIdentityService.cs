﻿using Signaturit.Application.DTOs.Identity;
using Signaturit.Application.Results;
using System.Threading.Tasks;

namespace Signaturit.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<Result<string>> RegisterAsync(RegisterRequest request, string origin);

        Task<Result<string>> ConfirmEmailAsync(string userId, string code);

        Task ForgotPassword(ForgotPasswordRequest model, string origin);

        Task<Result<string>> ResetPassword(ResetPasswordRequest model);

        Task<string> GetUserNameAsync(string userId);

    }
}