namespace Solvintech.Services.Contracts
{
    public interface IServiceManager
    {
        IQuotationProxyService QuotationService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}