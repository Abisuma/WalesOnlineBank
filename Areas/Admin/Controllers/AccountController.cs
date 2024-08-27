using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Wales_Online_Bank.Models;
using Wales_Online_Bank.Models.ViewModels;
using Wales_Online_Bank.Repository.IRepository;

namespace Wales_Online_Bank.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly UserManager<Wales_Online_Bank.Models.CustomerUser> _userManager;

        public AccountController(IUnitOfWork unitOfWork/*, UserManager<Wales_Online_Bank.Models.CustomerUser> userManager*/)
        {
            _unitOfWork = unitOfWork;
           // _userManager = userManager;
        }


        public ViewResult AdminDashBoard()
        {
            return View();
        }

        public ViewResult Deposit()
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


        public ViewResult Withdraw()
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

            // Perform the withdrawal.
            account.Amount = amount;
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
        public ViewResult CustomerList() 
        {
            List<Account> customerList = _unitOfWork.Account.GetAllAccOrCustomerUser().ToList();
            return View(customerList);
        
        }

        [HttpGet]
        public ViewResult DepositList()
        {
            List<Transaction> depositList = _unitOfWork.Transaction.GetDepositList().ToList();   
            return View(depositList);

        }

        [HttpGet]
        public ViewResult WithdrawList()
        {
            List<Transaction> withdrawalList = _unitOfWork.Transaction.GetWithdrawalList().ToList();
            return View(withdrawalList);

        }

        [HttpGet]
        public ViewResult FullCustomerProfile()
        {
            ViewBag.IdCardTypes = Enum.GetValues(typeof(IdCardType)).Cast<IdCardType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            ViewBag.MaritalStatuses = Enum.GetValues(typeof(MaritalStatus)).Cast<MaritalStatus>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            ViewBag.GenderType = Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            UpdateCustomerView customer = new()
            {
                CustomerUser = new Wales_Online_Bank.Models.CustomerUser()
            };

            return View(customer);

        }
       
        [HttpPost]
        
        public IActionResult FullCustomerProfile(UpdateCustomerView customer)
        {
            if (ModelState.IsValid)
            {

                _unitOfWork.CustomerUser.UpdateAccOrCustomerUser(customer.CustomerUser);
                return RedirectToAction("AdminDashBoard", "Account");
            }

            return View(customer);

        }

        //[HttpPost]
        //public IActionResult FullCustomer(UpdateCustomerView customer)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            // Attempt to update the entity
        //            _unitOfWork.CustomerUser.UpdateAccOrCustomerUser(customer.CustomerUser);
        //            return RedirectToAction("AdminDashBoard", "Account");
        //        }
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        // Log the exception or handle it as needed
        //        // Reload the entity from the database
        //        ex.Entries.Single().Reload();
        //        var entry = ex.Entries.Single();
        //        var databaseValues = entry.GetDatabaseValues();
        //        var clientValues = entry.CurrentValues;

        //        // Update the original values with database values
        //        foreach (var property in entry.OriginalValues.Properties)
        //        {
        //            var databaseValue = databaseValues[property];
        //            entry.OriginalValues[property] = databaseValue;
        //        }

        //        // You may want to display a message to the user indicating a concurrency conflict
        //        ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user after you got the original value. The edit operation was canceled.");

        //        // You can then return the view with the updated entity for the user to resolve the conflict
        //        return View("FullCustomerProfile", customer);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle other exceptions
        //        ModelState.AddModelError(string.Empty, "An error occurred while updating the record.");
        //        // Log the exception
        //    }

        //    // If there was an error, return the view with the original data
        //    return View("FullCustomerProfile", customer);
        //}


        [HttpGet]  
        public JsonResult GetUserDetails (string name, string CustomerUser_LastName)
        
        {
            try
            {
                // Implement the logic to fetch the account name based on the account number.
                var userDetailsFetched = _unitOfWork.CustomerUser.GetCustomerBasedOnNames(name, CustomerUser_LastName);

                if (userDetailsFetched != null)
                {
                    var userfulldetails = new
                    {     Id = userDetailsFetched.Id, 
                        GenderType = userDetailsFetched.GenderType,
                        PhoneNum = userDetailsFetched.PhoneNum,
                        Email = userDetailsFetched.Email,
                        FirstName = userDetailsFetched.FirstName, 
                         LastName = userDetailsFetched.LastName,    
                        Address = userDetailsFetched.Address,   
                         IdCardNumber = userDetailsFetched.IdCardNumber,    
                         IdType = userDetailsFetched.IdType,    
        
                        MaritalStatusOfCustomerUser = userDetailsFetched.MaritalStatusOfCustomerUser,


                        //StatusOfAccount = userDetailsFetched.StatusOfAccount,
                        DateOfBirth = userDetailsFetched.DateOfBirth, 
        
                         ImageUrl = userDetailsFetched.ImageUrl,    
       
                       Number = userDetailsFetched.Account.Number,
                    };

                    return new JsonResult(new { userfulldetails });
                }
                else
                {
                    return new JsonResult(new { error = "User details not found" });
                }
            }

            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message });
            }

        }

    }
}
