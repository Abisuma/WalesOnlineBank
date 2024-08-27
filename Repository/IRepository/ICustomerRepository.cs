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
    public interface ICustomerUserRepository: IRepository<CustomerUser>
    {
        public void UpdateCustomerUser(CustomerUser obj);
        //public decimal Transfer(decimal amount); 
        public CustomerUser GetCustomerandallProperties(CustomerUser user);
        public CustomerUser GetCustomerBasedOnNames(string firstName, string LastName);


    }
}
