using System.ComponentModel.DataAnnotations.Schema;

namespace Wales_Online_Bank.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string TransactionDescription { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
    }
}
