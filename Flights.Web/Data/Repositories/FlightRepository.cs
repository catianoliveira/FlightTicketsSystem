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

        public async Task<Flight> GetAirplanesAsync(int id)
        {
            return await _context.Flights
                         .Include(c => c.Airplanes)
                         .Where(c => c.Id == id)
                         .FirstOrDefaultAsync();
        }

        public IEnumerable<SelectListItem> GetComboArrivals(int departureId)
        {
            var list = _context.Flights.Where(a => a.DepartureAirportId == departureId).Select(a => new SelectListItem
            {
                Text = a.ArrivalAirport.CompleteAirport,
                Value = a.ArrivalAirportId.ToString()

            }).OrderBy(a => a.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Select an airport",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboDepartures()
        {
            var list = _context.Flights.Select(d => new SelectListItem
            {
                Text = d.DepartureAirport.CompleteAirport,
                Value = d.DepartureAirportId.ToString()

            }).OrderBy(l => l.Text).ToList();


            list.Insert(0, new SelectListItem
            {
                Text = "Select an airport",
                Value = "0"
            });

            return list;
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

        public IQueryable GetTodaysFlights()
        {
            return _context.Flights
                .Include(f => f.ArrivalAirport)
                .Include(f => f.DepartureAirport)
                .OrderBy(f => f.DateTime)
                .Where(f => f.DateTime >= DateTime.Today.ToUniversalTime());
        }
    }
}
