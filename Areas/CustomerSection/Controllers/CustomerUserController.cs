//using Microsoft.AspNetCore.Mvc;

using Azure;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Buffers.Text;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using Wales_Online_Bank.Models;
using Wales_Online_Bank.Models.ViewModels;
using Wales_Online_Bank.Repository.IRepository;

namespace Wales_Online_Bank.Areas.CustomerUser.Controllers
{
    [Area("CustomerSection")]
    
    public class CustomerUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Wales_Online_Bank.Models.CustomerUser> _userManager;


        public CustomerUserController(IUnitOfWork unitOfWork, UserManager<Wales_Online_Bank.Models.CustomerUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager; 
        }

        public IActionResult CustomerUserList()
        {
            var listOfCustomerUser = _unitOfWork.CustomerUser.GetAllAccOrCustomerUser().ToList();

            return View(listOfCustomerUser);
        }
        
       
        public  IActionResult DashBoard()
        {
            var customerUser = _userManager.Users.Include(u => u.Account).FirstOrDefault(u => u.Id == u.Id);
            //var customUser = new Wales_Online_Bank.Models.CustomerUser();

            if (customerUser != null)
            {
                ViewBag.UserId = customerUser.Id;                
            }
            else
            {
                return RedirectToAction("Login"); 
            }

            return View();
        }


        public IActionResult TransferMoney()
        {
            // Retrieve the currently logged-in user
            var user = _userManager.GetUserAsync(User).Result;

            if (user == null)
            {
                // Handle the case where the user is not found
                return View("Error");
            }

            // Retrieve user details and account
            var userdetails = _unitOfWork.CustomerUser.GetCustomerandallProperties(user);

            if (userdetails == null)
            {
                // Handle the case where user details are not found
                return View("Error");
            }

            var vm = new TransferViewModel
            {
                Account = userdetails.Account,
               
            };
            vm.Account.Amount = 0;
            return View(vm);
        }


        [HttpPost]
        public JsonResult TransferMoney([FromBody] TransferViewModel transfer)
        {
            // Retrieve source account
            var sourceAccount = _unitOfWork.Account.GetAccountByAccountNumber(transfer.Account.Number);

            if (sourceAccount == null)
            {
                return Json(new { success = false, message = "Source account not found." });
            }

            if (transfer.Account.Amount <= 0)
            {
                return Json(new { success = false, message = "Invalid transfer amount." });
            }

            if (sourceAccount.Balance < transfer.Account.Amount)
            {
                return Json(new { success = false, message = "Insufficient balance for the transfer." });
            }

            var destinationAccount = _unitOfWork.Account.GetAccountByAccountNumber(transfer.DestinationAccount);

            if (destinationAccount == null)
            {
                return Json(new { success = false, message = "Destination account not found." });
            }

            // Create a withdrawal debit transaction
            var creditedAmount = transfer.Account.Amount;
            var sourceAccountId = sourceAccount.AccountId;
            var destinationAccountId = destinationAccount.AccountId;    

            var transferCreditTransaction = new Transaction
            {
                TransactionDate = DateTime.Now,
                Amount = creditedAmount,
                Description = "Transfer credited",
                TransactionDescription = "credit", // Indicates a credit transaction
                AccountId = destinationAccountId, // Set the destination account ID
            };

            // Create a transfer debit transaction 
            var transferAmount = transfer.Account.Amount;

            var transferDebitTransaction = new Transaction
            {
                TransactionDate = DateTime.Now,
                Amount = transferAmount,
                Description = "Transfer to " + destinationAccount.Number,
                TransactionDescription = "debit", // Indicates a debit transaction
                AccountId = sourceAccountId, // Set the source account ID
            };

            // Save these transactions to your database
            _unitOfWork.Transaction.CreateAccOrCustomerUser(transferCreditTransaction);
            _unitOfWork.Transaction.CreateAccOrCustomerUser(transferDebitTransaction);


            // Perform the fund transfer
            sourceAccount.Balance -= transfer.Account.Amount;
            destinationAccount.Balance += transfer.Account.Amount;

            // Update both accounts in the database
            _unitOfWork.Account.UpdateAccOrCustomerUser(sourceAccount);
            _unitOfWork.Account.UpdateAccOrCustomerUser(destinationAccount);

            _unitOfWork.Save();

            return Json(new { success = true, message = "Transfer successful." });
        }





        public IActionResult Balance()
        {
            // Retrieve the logged-in user's ID
            var user = _userManager.GetUserAsync(User).Result;

            if (user != null)
            {
                // retrieve the user's  details and that of its' account
                var userdetails = _unitOfWork.CustomerUser.GetCustomerandallProperties(user);


                if (userdetails != null && userdetails.Account != null)
                {
                    return View(userdetails.Account);
                }
            }

            // Handle the case where the user is not found or the balance is not available
            return View("Error"); // You can create a custom error view.
        }

        public IActionResult Statement()
        {

            

            int accountId = GetCurrentUsersAccountId(); // Replace with your logic to get the user's account ID

            var user =  _userManager.GetUserAsync(User).Result;

            if (user != null)
            {
                List<Transaction> transactions = _unitOfWork.Transaction.GetTransactionsForUser(accountId);

                var viewModel = new StatementViewModel
                {
                    User = user,
                    Transactions = transactions
                };

                return View(viewModel);
            }

            // Handle the case where the user or their transactions are not found
            return View("Error");
        }

        public int GetCurrentUsersAccountId()
        {
            var user = _userManager.GetUserAsync(User).Result;
            if(user != null)
            { 
                var account = _unitOfWork.Account.GetAccOrCustomerUser(u=>u.AccountId == user.AccountId);

                if (account != null)
                {
                    return account.AccountId;
                }
            }
            return 0;
        }
    }


}
