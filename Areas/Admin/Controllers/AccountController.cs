using Microsoft.AspNetCore.Mvc;
using Wales_Online_Bank.Models;
using Wales_Online_Bank.Repository.IRepository;

namespace Wales_Online_Bank.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        [HttpPost]
        public IActionResult Deposit(decimal amount)
        {
            _unitOfWork.Account.MakeDeposit(amount);
            return View();
        }

        [HttpPost]
        public IActionResult Withdrawal(decimal amount)
        {
            _unitOfWork.Account.MakeWithdrawal(amount);
            return View();
        }
    }
}
