using Wales_Online_Bank.Data;
using Wales_Online_Bank.Models;

namespace Wales_Online_Bank
{
    
        public class UserService
        {
            private readonly AppDbContext _dbContext;

            public UserService(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            
        }

    
}
