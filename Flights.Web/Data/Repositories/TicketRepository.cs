using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Flights.Web.Data.Repositories;
using Flights.Web.Helpers;
using FlightTicketsSystem.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private readonly DataContext _context;
        private readonly IFlightRepository _flightRepository;
        private readonly IUserHelper _userHelper;

        public TicketRepository(
            DataContext context,
            IFlightRepository flightRepository,
            IUserHelper userHelper) : base(context)
        {
            _context = context;
            _flightRepository = flightRepository;
            _userHelper = userHelper;
        }

        //public async Task<Flight> CheckAvailability(int flightId)
        //{

        //}



    }
}
