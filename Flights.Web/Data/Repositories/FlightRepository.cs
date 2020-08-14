using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Repositories
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        private readonly DataContext _context;
        private readonly IAirportRepository _airportRepository;
        private readonly IAirplaneRepository _airplaneRepository;

        public FlightRepository(
            DataContext context,
            IAirportRepository airportRepository,
            IAirplaneRepository airplaneRepository) : base(context)
        {
            _context = context;
            _airportRepository = airportRepository;
            _airplaneRepository = airplaneRepository;
        }

        public IEnumerable<SelectListItem> GetComboFlights()
        {
            throw new NotImplementedException();
        }
    }
}

