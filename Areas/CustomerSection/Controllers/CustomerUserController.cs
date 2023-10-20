//using Microsoft.AspNetCore.Mvc;

using Azure;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Buffers.Text;
using System.Reflection.Metadata;
using Wales_Online_Bank.Models;
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
            var customerUser = _userManager.Users.Include(u => u.Account).FirstOrDefault(u => u.Id == u.Id);

            if (customerUser != null)
            {
                ///ViewBag.UserId = customerUser.Id;

                if (customerUser.Account != null)
                {

                    var account = GetAccountDetails(customerUser.Account.AccountName, customerUser.Account.Number); // Replace with your actual logic.
                    if (account != null)
                    {
                        customerUser.Account = account;

                    }

                }

            }

            return View(customerUser);
        }
        



        public Account GetAccountDetails(string accountname, string accountnumber)
        {
            var accountdetails = _unitOfWork.Account.GetAccOrCustomerUser(x=>x.AccountName == accountname && x.Number == accountnumber);
            return accountdetails;
            
        }


        #region

        
        
        
        #endregion

    }
}
