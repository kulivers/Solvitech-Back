using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Solvintech.Contracts;
using Solvintech.Entities;

namespace Solvintech.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task CreateUser(User user)
        {
            await CreateAsync(user);
            await _repositoryContext.SaveChangesAsync();
        }
    }
}