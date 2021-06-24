using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartParcel.Models;

namespace SmartParcel.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, IHostingEnvironment ihostingEnvironment,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _hostingEnvironment = ihostingEnvironment;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Date Of Birth")]
            public DateTime DOB { get; set; }

            [Required]
            [EmailAddress]
          //  [Remote(action: "IsEmailInUse",controller:"Account")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "PostCode")]
            [Required(ErrorMessage = "Please Enter The PostCode")]
            public string PostCode { get; set; }

            [Display(Name = "House Number")]
            [Required(ErrorMessage = "Please Enter The House Number")]
            public string HouseNumber { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }


            [Display(Name = "Driver Licence")]
            public string DriverLicence { get; set; }

            [Required]
            public string MobileNumber { get; set; }

            [Display(Name = "Licence Expiry Date")]
            public DateTime ExpiryDate { get; set; }

            public bool IsDriverAlso { get; set; }
            public IFormFile Photo { get; set; }
            public DateTime CreateDate { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                     var user = new ApplicationUser
                     {
                        UserName = Input.Email,
                        Email = Input.Email,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        PostCode = Input.PostCode,
                        HouseNumber = Input.HouseNumber,
                        IsDriver=false,
                        PhoneNumber = Input.MobileNumber,
                        CreateDate = DateTime.Now
                     };


                if(Input.IsDriverAlso== true)
                {
                    user.ExpiryDate = Input.ExpiryDate;
                    user.DriverLicence = Input.DriverLicence;
                    user.IsDriver = true;
                    user.IsVerified = false;
                }

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if(Input.IsDriverAlso== true)
                    {
                        result = await _userManager.AddToRoleAsync(user, "DriverUser");
                    }
                    else
                    {
                        result = await _userManager.AddToRoleAsync(user, "Users");
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (Input.IsDriverAlso == true)
                    {
                        return LocalRedirect("/Identity/Account/ImageUpload");
                    }
                    else
                    {
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
    }
}
