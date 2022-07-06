using System.Threading.Tasks;
using PasswordManager.Entities.Shared;
using Solvintech.Entities;

namespace Solvintech.Contracts
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);

        Task UpdateUserTokenAsync(string email, string passwordHash, string newToken);
        Task<string> GetUserTokenAsync(UserForAuthenticationDto userDto);
        Task<string> GetUserTokenAsync(string email, string passwordHash);
    }
}