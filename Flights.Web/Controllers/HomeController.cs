using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Flights.Web.Models;
using Flights.Web.Data.Entities;
using Flights.Web.Helpers;
using Flights.Web.Data.Repositories;
using FlightTicketsSystem.Web.Models;

namespace Flights.Web.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IFlightRepository _flightRepository;

        public HomeController(
            IUserHelper userHelper,
            IMailHelper mailHelper,
            IFlightRepository flightRepository)
        {
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _flightRepository = flightRepository;
        }


        public IActionResult Index()
        {
            var flight = _flightRepository.GetTodaysFlights();
            return View(flight);
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
        public IActionResult Contact(ContactViewModel model)
        {
            _mailHelper.SendMail("highflyairline@gmail.com", model.Subject, $"Mail from: {model.Name}, {model.Email}</br>" +
                $"</br></br>Message: {model.Message}");


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
