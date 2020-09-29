using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Repositories
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        private readonly DataContext _context;
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IAirportRepository _airportRepository;

        public FlightRepository(
            DataContext context,
            IAirplaneRepository airplaneRepository,
            IAirportRepository airportRepository) : base(context)
        {
            _context = context;
            _airplaneRepository = airplaneRepository;
            _airportRepository = airportRepository;
        }

        public IQueryable GetAllFlights()
        {
            return _context.Flights
                .AsNoTracking()
                .Include(a => a.Airplane)
                .Include(a => a.DepartureAirport)
                .Include(a => a.ArrivalAirport)
                .OrderBy(p => p.DateTime)
                .Where(a => a.DateTime >= DateTime.Today.ToUniversalTime());
        }


        public IQueryable GetNextFlights()
        {
            return _context.Flights
                .Include(f => f.ArrivalAirport)
                .Include(f => f.DepartureAirport)
                .OrderBy(f => f.DateTime)
                .Where(f => f.DateTime > DateTime.Today.ToUniversalTime());
        }
    }
}
