using System.Threading.Tasks;
using Solvintech.Entities;

namespace Solvintech.Contracts
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
    }
}