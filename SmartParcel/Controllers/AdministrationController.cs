using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartParcel.Data;
using SmartParcel.DataClasses;
using SmartParcel.Models;
using Nexmo.Api;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace SmartParcel.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;

        public AdministrationController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context, UserManager<ApplicationUser> _UserManager, IHostingEnvironment env)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = _UserManager;
            _env = env;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRole model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name= model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }           
            return View(model);
        }

        [HttpGet]
        public IActionResult PendingRequest()
        {
            var model = _context.Users.Where(m => m.IsVerified == false && m.IsDriver == true);
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            string userid = id;

            if (userid == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var userDetails = _context.Users.Where(m => m.Id == userid);

            var model = userDetails.FirstOrDefault();

            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> ApproveDriver(string id)
        {
            if (ModelState.IsValid)
            {
                string userId = id;
                ApplicationUser applicationUser = await _context.Users.FindAsync(userId);
                applicationUser.IsVerified = true;
                var _User = _userManager.FindByIdAsync(applicationUser.Id);
                string User_Phone = _User.Result.PhoneNumber;
                string User_email = _User.Result.Email;
                _context.Users.Update(applicationUser);
                await _context.SaveChangesAsync();

                string textmsg = "Verification Complete. You can now Accept Offers";
                SendSMS(User_Phone, textmsg);

                string emailMsg = "Verification Complete. You can now Accept Offers";
                SendEmail(User_email, emailMsg);

                return RedirectToAction("PendingRequest", "Administration");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> RejectDriver(string id)
        {
            if (ModelState.IsValid)
            {
                string userId = id;

                ApplicationUser applicationUser = await _context.Users.FindAsync(userId);
                var _User = _userManager.FindByIdAsync(applicationUser.Id);
                string User_Phone = _User.Result.PhoneNumber;
                string User_email = _User.Result.Email;
                _context.Users.Remove(applicationUser);
                await _context.SaveChangesAsync();

                string textmsg = "Verification Failed. Please Register Again";
                SendSMS(User_Phone, textmsg);

                string emailMsg = "Verification Failed. Please Register Again";
                SendEmail(User_email, emailMsg);

                return RedirectToAction("PendingRequest", "Administration");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }


        //Send SMS
        public void SendSMS(string MobileNumber, string message)
        {
            string key = "48869aca";
            string secret = "m2EHQmGx5PRPyGil";
            string name = "SPD";

            var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = key,
                ApiSecret = secret
            });
            var results = client.SMS.Send(request: new SMS.SMSRequest
            {
                from = name,
                to = MobileNumber,
                text = message
            });
        }

        //Sending Email
        public void SendEmail(string Body, string email)
        {
            //MimeMessage message = new MimeMessage();

            //MailboxAddress from = new MailboxAddress("SPD","tousif123890@gmail.com");
            //message.From.Add(from);

            //MailboxAddress to = new MailboxAddress("User",email);
            //message.To.Add(to);

            //message.Subject = "Regarding Driver Verification";

            //BodyBuilder bodyBuilder = new BodyBuilder();
            //bodyBuilder.TextBody = Body;

            //message.Body = bodyBuilder.ToMessageBody();

            //SmtpClient client = new SmtpClient();
            //client.Connect("smtp_address_here", port_here, true);
            //client.Authenticate("user_name_here", "pwd_here");

            //client.Send(message);
            //client.Disconnect(true);
            //client.Dispose();
        }
    }
}
// GOFLID97691565455399