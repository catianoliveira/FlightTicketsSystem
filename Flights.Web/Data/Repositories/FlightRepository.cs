using Flights.Web.Data.Entities;
using FlightTicketsSystem.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public async Task<Flight> GetDeparturesWithArrivalsAsync(int departureAirportId)
        {
            return await _context.Flights
              .Include(c => c.ArrivalsCollection)
              .FirstOrDefaultAsync(d => d.DepartureAirportId == departureAirportId);
        }

        
        public async Task<int> GetEconomySeats(int flightId)
        {
            //vai buscar o id do avião
            var airplaneId = _context.Set<Airplane>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == flightId);


            //vai buscar a quantidade de lugares que há no avião
            var economySeats = _context.Airplanes
                .Include(a => a.EconomySeats)
                .Where(a => a.Id == airplaneId.Id);


            //vai buscar o ultimo lugar ocupado no voo
            var lastSeatTaken = _context.Tickets
                .Include(a => a.SeatNumber);


            //ultimo lugar mais um
            var nextSeat = Convert.ToInt32(lastSeatTaken) + 1;


            //compara o ultimo lugar aos lugares disponiveis
            if (lastSeatTaken == economySeats)
            {
                //voo cheio
                return 0;
            }

            else if (lastSeatTaken == null)
            {
                //primeiro lugar
                nextSeat = 1;
                return nextSeat;
            }

            else
            {
                //ultimo lugar mais um
                return nextSeat;
            }
        }


        //public async Task<Flight> GetBusinessSeats(int airplaneId)
        //{
            
        //}
    }
}
