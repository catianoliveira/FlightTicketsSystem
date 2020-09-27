using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Flights.Web.Models;
using Flights.Web.Data.Entities;
using Flights.Web.Helpers;

namespace Flights.Web.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public HomeController(
            IUserHelper userHelper,
            IMailHelper mailHelper)
        {
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(User user)
        {
            user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
            {
                userid = user.Id,
                token = myToken
            }, protocol: HttpContext.Request.Scheme);

            _mailHelper.SendMail("highflyairline@gmail.com", "Email confirmation", $"<h1>Email Confirmation</h1>" +
                $"Complete your registration by " +
                $"clicking link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
