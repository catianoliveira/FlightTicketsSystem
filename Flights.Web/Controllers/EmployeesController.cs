using Flights.Web.Data.Entities;
using Flights.Web.Helpers;
using FlightTicketsSystem.Web.Data.Entities;
using FlightTicketsSystem.Web.Data.Repositories;
using FlightTicketsSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin, Employee")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUserHelper _userHelper;

        public EmployeesController(
            IEmployeeRepository employeeRepository,
            UserManager<User> userManager,
            IUserHelper userHelper)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _userHelper = userHelper;
        }

        // GET: Employees
        public IActionResult Index()
        {
            var employee = _employeeRepository.GetAll().ToList();

            return View(employee);
        }
    }
}
