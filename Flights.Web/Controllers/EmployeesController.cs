using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightTicketsSystem.Web.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin, Employee")]
    public class EmployeesController : Controller
    {
        public IActionResult ListClients()
        {
            return View();
        }
    }
}
