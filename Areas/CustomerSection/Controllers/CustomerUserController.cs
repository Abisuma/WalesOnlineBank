//using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using Wales_Online_Bank.Repository.IRepository;

namespace Wales_Online_Bank.Areas.CustomerUser.Controllers
{
    [Area("CustomerSection")]
    
    public class CustomerUserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public CustomerUserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult CustomerUserList()
        {
            var listOfCustomerUser = _unitOfWork.CustomerUser.GetAllAccOrCustomerUser().ToList();

            return View(listOfCustomerUser);
        }

        public IActionResult DashBoard()
        {
            return View();  
        }
    }
}
