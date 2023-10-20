using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wales_Online_Bank.Models;
using Wales_Online_Bank.Repository.IRepository;

namespace ApptmentmentScheduler.DataAccessLayer.Repository.IRepository
{
    public interface IAccountRepository: IRepository<Account>
    {
        //public decimal MakeDeposit(decimal amount);
       //public decimal MakeWithdrawal(decimal amount);
        public Account GetAccountByAccountNumber(string accountNumber);
        


    }
}
