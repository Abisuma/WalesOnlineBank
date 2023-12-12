namespace Wales_Online_Bank.Models.ViewModels
{
    public class StatementViewModel
    {
        public CustomerUser User { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
     
}
