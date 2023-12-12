
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wales_Online_Bank.Data;
using Wales_Online_Bank.Models;
using Wales_Online_Bank.Models.Repository.IRepository;

namespace Wales_Online_Bank.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private AppDbContext _dbContext;
        public TransactionRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Transaction> GetDepositList()
        {
            return _dbContext.Transactions.Where(u=> u.TransactionDescription == "credit").Include(t => t.Account).ToList(); 
        }

        public List<Transaction> GetWithdrawalList()
        {
            return _dbContext.Transactions.Where(u => u.TransactionDescription == "debit").Include(t => t.Account).ToList();
        }

        public List<Transaction> GetTransactionsForUser(int accountid)
        {
            return _dbContext.Transactions.Where(t => t.AccountId == accountid).ToList();
        }
    }
}
