using System;
using Solvintech.Contracts;

namespace Solvintech.Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IUserRepository> _userRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
        }
        public IUserRepository Users => _userRepository.Value;
        public void Save() => _repositoryContext.SaveChanges();
    }
}