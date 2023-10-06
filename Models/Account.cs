using System.ComponentModel;

namespace Wales_Online_Bank.Models
{
    public enum AccountType { Savings, Current}
    public class Account
    {
        public int AccountId { get; set; }
        [DisplayName("Account Number")]
        public string? Number { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

        public string? CustomerUserId { get; set; }
        public virtual CustomerUser CustomerUser { get; set; }

        public string? TransactionDescription {  get; set; } 

        public string AccountName => CustomerUser.FirstName + " " + CustomerUser.LastName;

    //public AccountType TypeofAccount { get; set; }   
    }
}
