using Flights.Web.Data.Entities;
using FlightTicketsSystem.Web.Models;
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
            var list = _context.Flights.Where(p => p.DepartureAirportId == departureId).Select(p => new SelectListItem
            {
                Text = p.ArrivalAirport.CompleteAirport,
                Value = p.ArrivalAirportId.ToString()

            }).OrderBy(p => p.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Select an airport",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboDepartures()
        {
            var list = _context.Flights.Select(p => new SelectListItem
            {
                Text = p.DepartureAirport.CompleteAirport,
                Value = p.DepartureAirportId.ToString()

            }).OrderBy(l => l.Text).ToList();


            list.Insert(0, new SelectListItem
            {
                Text = "Select an airport",
                Value = "0"
            });

            return list;
        }

        public async Task<Flight> GetDeparturesWithArrivalsAsync(int departureAirportId)
        {
            return await _context.Flights
              .Include(c => c.ArrivalsCollection)
              .FirstOrDefaultAsync(d => d.DepartureAirportId == departureAirportId);
        }
    }
}
