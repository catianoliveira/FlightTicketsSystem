using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Flights.Web.Helpers;
using FlightTicketsSystem.Web.Data.Repositories;
using FlightTicketsSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;

        public ClientsController(
            ITicketRepository ticketRepository,
            IUserHelper userHelper,
            DataContext context)
        {
            _ticketRepository = ticketRepository;
            _userHelper = userHelper;
            _context = context;
        }
        

    }
}
