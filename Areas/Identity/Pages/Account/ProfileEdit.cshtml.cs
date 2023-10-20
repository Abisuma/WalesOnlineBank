using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Wales_Online_Bank.Data;
using Wales_Online_Bank.Models;
using Wales_Online_Bank.Repository.IRepository;

namespace Wales_Online_Bank.Areas.Identity.Pages.Account
{
    
    public class ProfileEditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfileEditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        [BindProperty]
        public Wales_Online_Bank.Models.CustomerUser? CustomerUser { get; set; } 
        //public RegisterModel RegisterModel { get; set; }
        public void OnGet(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                CustomerUser = _unitOfWork.CustomerUser.GetAccOrCustomerUser(x=>x.Id == id); 
            }
        }

        public IActionResult OnPost() 
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CustomerUser.UpdateCustomerUser(CustomerUser);
                _unitOfWork.Save();
                return RedirectToAction("DashBoard", "CustomerUser");
            }

            return Page();

        }    
    }
}
