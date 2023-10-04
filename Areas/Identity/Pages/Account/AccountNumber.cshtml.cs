using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Wales_Online_Bank.Areas.Identity.Pages.Account
{
    public class AccountNumberModel : PageModel
    {
        public long AccountNumber { get; set; }

        public void OnGet(int accountNumber)
        {
            AccountNumber = accountNumber;
        }
    }
}
