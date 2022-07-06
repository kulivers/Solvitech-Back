using AutoMapper;
using Solvintech.Contracts;
using Solvintech.Services.Contracts;

namespace Solvintech.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UserService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



    }
}