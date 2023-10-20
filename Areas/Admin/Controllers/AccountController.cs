using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wales_Online_Bank.Models;
using Wales_Online_Bank.Models.ViewModels;
using Wales_Online_Bank.Repository.IRepository;

namespace Wales_Online_Bank.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Wales_Online_Bank.Models.CustomerUser> _userManager;
        private readonly UserService _userService;
        public AccountController(IUnitOfWork unitOfWork, UserManager<Wales_Online_Bank.Models.CustomerUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager; 
        }


        public IActionResult AdminDashBoard()
        {
            return View();
        }

        public IActionResult Deposit()
        {
            DepositViewModel vm = new ()
            {
                Account = new Account()
            };

            return View();
        }

        //[HttpPost, ActionName("Deposit")]
        //public IActionResult DepositPost() 
        //{ 
        //    return View();
        //}

       
        //[HttpPost]
        public JsonResult MakeDeposit([FromBody] DepositViewModel request)
        {
            // Perform validation and fetch the account details by account number.

            var accountNumber = request.Account.Number;
            var account = _unitOfWork.Account.GetAccountByAccountNumber(accountNumber);
            var amount = request.Account.Amount;


            if (string.IsNullOrWhiteSpace(accountNumber) || amount <= 0)
            {
                return Json(new { success = false, message = "Invalid account number or deposit amount." });
            }

            

            if (account == null)
            {
                return Json(new { success = false, message = "Account not found." });
            }

            // Perform the deposit.
            account.Amount += amount;
            account.Balance += amount;

            // Update the account in the database.
            _unitOfWork.Account.UpdateAccOrCustomerUser(account);

            return Json(new { success = true, message = "Deposit successful." });


            
        }


        [HttpGet]
        public JsonResult GetAccountName(string accountNumber)
        {

            try
            {
                // Implement the logic to fetch the account name based on the account number.
                var accountNumFetched = _unitOfWork.Account.GetAccOrCustomerUser(x => x.Number == accountNumber);

                if (accountNumFetched != null)
                {
                    var accountName = accountNumFetched.AccountName;

                    return new JsonResult(new { accountName });
                }
                else
                {
                    return new JsonResult(new { error = "Account not found" });
                }
            }

            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message });
            }

        }


    }
}
