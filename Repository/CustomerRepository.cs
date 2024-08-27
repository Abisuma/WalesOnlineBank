
using Microsoft.EntityFrameworkCore;
using Wales_Online_Bank.Data;
using Wales_Online_Bank.Models;
using Wales_Online_Bank.Models.Repository.IRepository;

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

        public CustomerUser GetCustomerandallProperties(CustomerUser user)
        {
            return _dbContext.CustomerUsers.Include(cu => cu.Account)
            .FirstOrDefault(g => g.Id == user.Id);
        }

        public CustomerUser GetCustomerBasedOnNames(string firstName, string LastName)
        {
            return _dbContext.CustomerUsers.Include(cu => cu.Account)
            .FirstOrDefault(x => x.FirstName == firstName && x.LastName == LastName);
        }
    }
}