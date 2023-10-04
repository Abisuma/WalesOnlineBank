using ApptmentmentScheduler.DataAccessLayer.Repository.IRepository;

namespace Wales_Online_Bank.Repository.IRepository
{
    public interface IUnitOfWork
    {

        IAccountRepository Account { get; }
        ICustomerUserRepository CustomerUser { get; }
        void Save();
    }
}
