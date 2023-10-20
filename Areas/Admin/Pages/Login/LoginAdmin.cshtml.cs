using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Session;

namespace Wales_Online_Bank.Areas.Admin.Pages.Login
{
    [Area("Admin")]
    public class LoginAdminModel : PageModel
    {
        private readonly ILogger<LoginAdminModel> _logger;

        public LoginAdminModel(ILogger<LoginAdminModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                // Check if the provided email and password match your admin credentials.
                var dynamicPassword = "A@dmin" + DateTime.Now.ToString("MM") + (DateTime.Now.Day + 3).ToString();
                var dynamicEmail = "3B9A9174-D338-40D1-80CB-AB3B36A8CE26@adminOnlineBank.com";

                if (Input.Email == dynamicEmail && Input.Password == dynamicPassword)
                {
                    _logger.LogInformation("Admin logged in.");

                    // You can perform additional checks or authentication here.
                    // If successful, you can set a session variable or cookie to recognize the admin user.
                    HttpContext.Session.SetString("IsAdmin", "true");
                    // Redirect to the admin dashboard.
                    return Redirect("/Admin/Account/AdminDashBoard");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            // If we got this far, the login attempt failed, so redisplay the form.
            return Page();
        }
    }
}



