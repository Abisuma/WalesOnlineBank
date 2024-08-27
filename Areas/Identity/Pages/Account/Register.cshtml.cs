// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Wales_Online_Bank.Models;
//using Wales_Online_Bank.Models;

namespace Wales_Online_Bank.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Wales_Online_Bank.Models.CustomerUser> _signInManager;
        private readonly UserManager<Wales_Online_Bank.Models.CustomerUser> _userManager;
        private readonly IUserStore<Wales_Online_Bank.Models.CustomerUser> _userStore;
        private readonly IUserEmailStore<Wales_Online_Bank.Models.CustomerUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RegisterModel(
            UserManager<Wales_Online_Bank.Models.CustomerUser> userManager,
            IUserStore<Wales_Online_Bank.Models.CustomerUser> userStore,
            SignInManager<Wales_Online_Bank.Models.CustomerUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
             _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            //public string CustomerUserId { get; set; }
            [Display(Name = "First Name")]
            [Required]
            public string FirstName { get; set; }
            [Display(Name = "Last Name")]
            [Required]
            public string LastName { get; set; }
            [Required]
            public string Address { get; set; }
            [Display(Name = "ID Number")]
            [Required]
            public string IdCardNumber { get; set; }
            [Display(Name = "ID Type")]
            [Required]
            public IdCardType IdType { get; set; }
            [Display(Name = "Marital Status")]
            [Required]
            public MaritalStatus MaritalStatusOfCustomerUser { get; set; }
            
            [EnumDataType(typeof(Gender), ErrorMessage ="Invalid Gender")]
            [Display(Name = "Gender")]
            [Required]
            public Gender GenderType { get; set; }
            public StatusType StatusOfAccount { get; set; }
            [Required]

            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public DateTime DateOfBirth { get; set; }  
            [Display(Name = "Phone Number")]
            [Required]
            public string PhoneNum { get; set; }
            //[FileExtensions(Extensions ="jpg, png,JPG, PNG", ErrorMessage="Invalid Image format, kindly upload an image file")]
            [Display(Name = "Upload Image")]
            [Required]
            public IFormFile ProfileImage { get; set; }
            public IEnumerable<SelectListItem> IdTypeList { get; set; }
            public IEnumerable<SelectListItem> GenderList { get; set; }
            public IEnumerable<SelectListItem> MaritalStatusList { get; set; }
            public Wales_Online_Bank.Models.Account Account { get; set; }    
        }

        


        public async Task OnGetAsync(string returnUrl = null)
        {
            Input = new InputModel
            {
                 IdTypeList = Enum.GetValues(typeof(IdCardType))
                        .Cast<IdCardType>()
                        .Select(e => new SelectListItem
                        {
                        Value = e.ToString(),
                        Text = e.ToString()
                        }),

                MaritalStatusList = Enum.GetValues(typeof(MaritalStatus))
                    .Cast<MaritalStatus>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    }),

                GenderList = Enum.GetValues(typeof(Gender))
                    .Cast<Gender>()
                    .Select(e => new SelectListItem
                    {
                        Value = e.ToString(),
                        Text = e.ToString()
                    }),
            };
                 ReturnUrl = returnUrl;
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

            public async Task<IActionResult> OnPostAsync(string returnUrl = null)
            {
                returnUrl ??= Url.Content("~/");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                if (ModelState.IsValid) 
                {
                    var user = CreateUser();
                    //user.CustomerUserId = Input.CustomerUserId;
                    user.FirstName = Input.FirstName;
                    user.LastName = Input.LastName;
                    user.Address = Input.Address; 
                    user.IdCardNumber = Input.IdCardNumber;
                    user.IdType = Input.IdType;
                    user.MaritalStatusOfCustomerUser = Input.MaritalStatusOfCustomerUser; 
                    user.GenderType = Input.GenderType;
                    //user.StatusOfAccount = Input.StatusOfAccount;
                    user.DateOfBirth = Input.DateOfBirth;
                    user.PhoneNum = Input.PhoneNum;

                if (user.Account == null)
                {
                    user.Account = new Wales_Online_Bank.Models.Account();
                }
                user.Account.Number = Input.Account.Number;
                //user.Account.CustomerUserId = user.Id;
                user.Account.AccountName = user.FirstName + " " + user.LastName;    

                if (Input.ProfileImage != null)
                  {
                    // Save the image to a specific directory
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Images\ProfileImages");
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(Input.ProfileImage.FileName);
                    var imagePathWithFileName = Path.Combine(imagePath, imageName);

                    using (var stream = new FileStream(imagePathWithFileName, FileMode.Create))
                    {
                        await Input.ProfileImage.CopyToAsync(stream);
                    }

                    user.ImageUrl = @"\Images\ProfileImages" + imageName; // Save the URL to the image
                  }

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                    var result = await _userManager.CreateAsync(user, Input.Password);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                          

                    var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false) ;
                            return LocalRedirect(returnUrl);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                // If we got this far, something failed, redisplay form
                return Page();
            }

            private Wales_Online_Bank.Models.CustomerUser CreateUser()
            {
                try
                {
                    return Activator.CreateInstance<Wales_Online_Bank.Models.CustomerUser>();
                }
                catch
                {
                    throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                        $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                        $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
                }
            }

            private IUserEmailStore<Wales_Online_Bank.Models.CustomerUser> GetEmailStore()
            {
                if (!_userManager.SupportsUserEmail)
                {
                    throw new NotSupportedException("The default UI requires a user store with email support.");
                }
                return (IUserEmailStore<Wales_Online_Bank.Models.CustomerUser>)_userStore;
            }       
    }

}


