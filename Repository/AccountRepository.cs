using Wales_Online_Bank.Models.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Wales_Online_Bank.Data;
using Wales_Online_Bank.Models;

namespace Wales_Online_Bank.Repository
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private AppDbContext _dbContext;
        public AccountRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Account GetAccountByAccountNumber(Account obj)
        {
            var account = _dbContext.Accounts.FirstOrDefault(u => u.Number == obj.Number);
            
            if (account != null)
            {
                return account;
            }

            return obj;
        }

        public Account GetAccountByAccountNumber(string accountNumber)
        {
            return _dbContext.Accounts.FirstOrDefault(t => t.Number.Equals(accountNumber));
        }


    }
}