using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using PasswordManager.Entities.Shared;
using Solvintech.Entities;

namespace Solvintech.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task RegisterUser(UserForRegistrationDto userDto);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> GetToken();
    }
}