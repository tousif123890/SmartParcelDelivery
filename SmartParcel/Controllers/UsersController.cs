using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartParcel.Controllers
{
    public class UsersController : Controller
    {

        [HttpGet]
        public IActionResult PostOffer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostOffeer()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Update()
        {
            return LocalRedirect("/Identity/Account/Manage/Index");
        }
    }
}