using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartParcel.Data;
using SmartParcel.DataClasses;
using SmartParcel.Models;

namespace SmartParcel.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHostingEnvironment _iHostingEnvironment;

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string ImagePath { get; set; }

        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _iHostingEnvironment = hostingEnvironment;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return LocalRedirect("/Identity/Account/Register");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return LocalRedirect("/Identity/Account/Login");
        }

        //[AllowAnonymous]
        //[AcceptVerbs("Get","Post")]
        //public async Task<IActionResult> IsEmailInUse(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if(user == null)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json($"Email {email} is already taken.");
        //    }
        //}



        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ImageUpload()
        {
            return LocalRedirect("/Identity/Account/ImageUpload");
        }


        [HttpPost][AllowAnonymous]
        public async Task<IActionResult> ImageUpload(IFormCollection form)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            string image = Guid.NewGuid().ToString();
            var webRoot = _iHostingEnvironment.WebRootPath;
            string storePath = webRoot + "/images/";
            if (form.Files == null || form.Files[0].Length == 0)
                return RedirectToAction("Index");

            var filename = image + form.Files[0].FileName;
            var name = form.Files[0].Name;
            var path = Path.Combine(Directory.GetCurrentDirectory(), storePath,filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await form.Files[0].CopyToAsync(stream);
            }

            user.PhotoPath = "/Images/" + filename;

            await _userManager.UpdateAsync(user);

            ImagePath = user.PhotoPath;
            StatusMessage = "Image has been Uploaded.Confirmation Pending.";

            return RedirectToAction("Index", "Home");
        }
    }
}