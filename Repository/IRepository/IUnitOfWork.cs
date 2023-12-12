using Wales_Online_Bank.Models.Repository.IRepository;

namespace Wales_Online_Bank.Repository.IRepository
{
    public interface IUnitOfWork
    {

        IAccountRepository Account { get; }
        ICustomerUserRepository CustomerUser { get; }
        ITransactionRepository Transaction { get; }
        void Save();
    }
}
