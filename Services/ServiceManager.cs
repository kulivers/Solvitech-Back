using System;
using System.Reflection.Emit;
using AutoMapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Solvintech.Contracts;
using Solvintech.Services.Contracts;

namespace Solvintech.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<AuthenticationService> _authenticationService;
        private readonly Lazy<QuotationProxyService> _quotationProxyService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IConfiguration configuration,
            IOptions<JwtConfiguration> jwtConfiguration)
        {
            _quotationProxyService = new Lazy<QuotationProxyService>(()=>new QuotationProxyService());

            _authenticationService = new Lazy<AuthenticationService>(() =>
                new AuthenticationService(repositoryManager, mapper, configuration, jwtConfiguration));
        }


        // public IUserService UserService => _userService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IQuotationProxyService QuotationService => _quotationProxyService.Value;
    }
}