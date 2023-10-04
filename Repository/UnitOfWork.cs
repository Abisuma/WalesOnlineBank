using ApptmentmentScheduler.DataAccessLayer.Repository.IRepository;
using Wales_Online_Bank.Data;
using Wales_Online_Bank.Repository.IRepository;

namespace Wales_Online_Bank.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private AppDbContext _appDbContext;
        public ICustomerUserRepository CustomerUser { get; private set; }
        public IAccountRepository Account { get; private set; }


        public UnitOfWork(AppDbContext db)
        {
            _appDbContext = db;
            CustomerUser = new CustomerUserRepository(_appDbContext);
            Account = new AccountRepository(_appDbContext);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}

