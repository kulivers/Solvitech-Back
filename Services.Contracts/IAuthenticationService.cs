using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using PasswordManager.Entities.Shared;
using Solvintech.Entities;

namespace Solvintech.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<string> GenerateNewUserToken(UserForAuthenticationDto userDto);
        Task<string> GetUserTokenAsync(UserForAuthenticationDto userDto);
        Task<string> GetUserTokenAsync(string email, string password);
        Task RegisterUser(UserForRegistrationDto userDto);
    }
}