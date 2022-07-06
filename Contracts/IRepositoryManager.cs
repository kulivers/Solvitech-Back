namespace Solvintech.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository Users { get; }
        void Save();
    }
}