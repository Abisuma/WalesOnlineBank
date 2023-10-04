using ApptmentmentScheduler.DataAccessLayer.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
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


        
            
        public decimal MakeDeposit(decimal amount)
        {
            var account = new Account();
            if (amount > 0)
            {
                account.Amount = amount;    
                account.Balance = account.Balance + account.Amount;
            }
                return account.Balance;
             
        }

        public decimal MakeWithdrawal(decimal amount)
        {
            var account = new Account();
            if (amount > 0 && account.Balance > amount)
            {
                account.Amount = amount;
                account.Balance = account.Balance - account.Amount;
            }
            return account.Balance;
        }
    }
}