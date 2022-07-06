using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Entities.Exceptions;
using PasswordManager.Entities.Shared;
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

        public async Task CreateUserAsync(User user)
        {
            await CreateAsync(user);
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task<string> GetUserTokenAsync(UserForAuthenticationDto userDto) =>
            await GetUserTokenAsync(userDto.Email, userDto.Password);

        public async Task UpdateUserTokenAsync(string email, string newToken)
        {
            User result;
            try
            {
                result = await _repositoryContext.Users.SingleOrDefaultAsync(user =>
                    user.Email == email);
            }
            catch
            {
                throw new Exception("More than one user found");
            }

            if (result == null) throw new UserNotFoundException(email);

            result.Token = newToken;
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task UpdateUserTokenAsync(string email, string passwordHash, string newToken)
        {
            User result;
            try
            {
                result =
                    await _repositoryContext.Users.SingleOrDefaultAsync(user =>
                        user.Email == email && user.PasswordHash == passwordHash);
            }
            catch
            {
                throw new Exception("More than one user found");
            }

            if (result == null) throw new UserNotFoundException(email);

            result.Token = newToken;
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task<string> GetUserTokenAsync(string email, string passwordHash)
        {
            var users = await FindByCondition(u => u.Email == email && u.PasswordHash == passwordHash)
                .ToArrayAsync();
            if (users.Length > 1)
                throw new Exception("More than one user found");
            return users.FirstOrDefault()?.Token;
        }
    }
}