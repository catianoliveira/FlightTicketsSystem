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

        //TODO public async Task<Flight> GetAirports(int id)
        //{
        //    return await _context.Flights
        //     .Include(c => c.)
        //     .Where(c => c.Id == id)
        //     .FirstOrDefaultAsync();
        //}

        public IEnumerable<SelectListItem> GetComboFlights()
        {
            var list = _context.Flights.Select(p => new SelectListItem
            {
                Text = p.CompleteFlight,
                Value = p.Id.ToString()

            }).ToList();


            list.Insert(0, new SelectListItem
            {
                Text = "Select a flight",
                Value = "0"
            });

            return list;
        }
    }
}
