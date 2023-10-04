using System.ComponentModel;

namespace Wales_Online_Bank.Models
{
    public enum AccountType { Savings, Current}
    public class Account
    {
        public int AccountId { get; set; }
        [DisplayName("Account Number")]
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }    
    
        public AccountType TypeofAccount { get; set; }   
    }
}
