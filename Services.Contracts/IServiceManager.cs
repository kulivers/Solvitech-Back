namespace Solvintech.Services.Contracts
{
    public interface IServiceManager
    {
        // IUserService UserService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}