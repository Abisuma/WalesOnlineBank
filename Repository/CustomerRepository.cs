using ApptmentmentScheduler.DataAccessLayer.Repository.IRepository;
using Wales_Online_Bank.Data;
using Wales_Online_Bank.Models;

namespace Wales_Online_Bank.Repository
{
    public class CustomerUserRepository : Repository<CustomerUser>, ICustomerUserRepository
    {
        private AppDbContext _dbContext;
        public CustomerUserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void UpdateCustomerUser(CustomerUser obj)
        {
            _dbContext.CustomerUsers.Update(obj);
        }

        //public decimal Transfer (decimal amount)
        //{
        //    return amount;
        //}
    }
}