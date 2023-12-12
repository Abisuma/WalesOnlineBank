using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wales_Online_Bank.Models;
using Wales_Online_Bank.Repository.IRepository;

namespace Wales_Online_Bank.Models.Repository.IRepository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        public List<Transaction> GetTransactionsForUser(int accountid);
        public List<Transaction> GetDepositList();
        public List<Transaction> GetWithdrawalList();
    }
}
