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
            DepositViewModel vm = new()
            {
                Account = new Account()
            };

            return View();
        }


        [HttpPost]
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

            var depositTransaction = new Transaction
            {
                TransactionDate = DateTime.Now,
                Amount = amount,
                Description = "Deposit",
                TransactionDescription = "credit", // Indicates a credit transaction
                AccountId = account.AccountId,
            };

            // Update the account in the database.
            _unitOfWork.Account.UpdateAccOrCustomerUser(account);

            _unitOfWork.Transaction.CreateAccOrCustomerUser(depositTransaction);//adding transactions to database apologies for the naming
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deposit successful." });



        }


        public IActionResult Withdraw()
        {
            DepositViewModel vm = new()
            {
                Account = new Account()
            };

            return View(vm);
        }

        [HttpPost]
        public JsonResult MakeWithdrawal([FromBody] DepositViewModel request)
        {
            // Assign and fetch the account details by account number.

            var accountNumber = request.Account.Number;
            var account = _unitOfWork.Account.GetAccountByAccountNumber(accountNumber);
            var amount = request.Account.Amount;

            //Validate account number and amount
            if (string.IsNullOrWhiteSpace(accountNumber) || amount <= 0)
            {
                return Json(new { success = false, message = "Invalid account number or deposit amount." });
            }



            if (account == null)
            {
                return Json(new { success = false, message = "Account not found." });
            }

            // Perform the deposit.
            account.Amount -= amount;
            account.Balance -= amount;

            var withdrawTransaction = new Transaction
            {
                TransactionDate = DateTime.Now,
                Amount = amount,
                Description = "Withdraw",
                TransactionDescription = "debit", // Indicates a debit transaction
                AccountId = account.AccountId,
            };

            // Update the account in the database.
            _unitOfWork.Account.UpdateAccOrCustomerUser(account);
            _unitOfWork.Transaction.CreateAccOrCustomerUser(withdrawTransaction);//addingtodatabase apologies for the naming
            _unitOfWork.Save();
            return Json(new { success = true, message = "Withdrawal successful." });

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

        [HttpGet]
        public IActionResult CustomerList() 
        {
            List<Account> customerList = _unitOfWork.Account.GetAllAccOrCustomerUser().ToList();
            return View(customerList);
        
        }

        [HttpGet]
        public IActionResult DepositList()
        {
            List<Transaction> depositList = _unitOfWork.Transaction.GetDepositList().ToList();   
            return View(depositList);

        }

        [HttpGet]
        public IActionResult WithdrawList()
        {
            List<Transaction> withdrawalList = _unitOfWork.Transaction.GetWithdrawalList().ToList();
            return View(withdrawalList);

        }
    }
}
