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


        public int GetBusinessSeats(int flightId)
        {
            //vai buscar o id do avião
            var airplane = _context.Airplanes
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == flightId);


            //vai buscar a quantidade de lugares que há no avião
            var totalSeats = _context.Airplanes
                .AsNoTracking()
                .Include(a => a.BusinessSeats)
                .Where(a => a.Id == airplane.Id);


            //vê se o voo existe nos bilhetes
            var flightIsInTickets = _context.Tickets
                .AsNoTracking()
                .FirstOrDefault(a => a.FlightId == flightId);


            if (flightIsInTickets != null)
            {
                var travelClass = "Business";

                //vai buscar o ultimo lugar ocupado no voo se o voo existir
                var lastSeatTaken = _context.Tickets
                    .AsNoTracking()
                    .FirstOrDefault(a => a.FlightId == flightId && a.TravelClass == travelClass);




                //ultimo lugar mais um
                var nextSeat = Convert.ToInt32(lastSeatTaken.SeatNumber) + 1;


                //compara o ultimo lugar aos lugares disponiveis
                if (lastSeatTaken == totalSeats)
                {
                    //voo cheio
                    return 0;
                }


                else if (lastSeatTaken.SeatNumber == 0)
                {
                    //primeiro lugar
                    return 1;
                }


                else
                {
                    //ultimo lugar mais um
                    return nextSeat;
                }
            }

            else
            {
                return 1;
            }
        }


        public int GetEconomySeats(int flightId)
        {
            //vai buscar o id do avião
            var airplane = _context.Airplanes
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == flightId);


            //vai buscar a quantidade de lugares que há no avião
            var totalSeats = _context.Airplanes
                .AsNoTracking()
                .Include(a => a.EconomySeats)
                .Where(a => a.Id == airplane.Id);

            var travelClass = "Economy";

            //vê se o voo existe nos bilhetes
            var flightIsInTickets = _context.Tickets
                .AsNoTracking()
                .FirstOrDefault(a => a.FlightId == flightId && a.TravelClass == travelClass);


            //se sim ^o vê se os lugares são economicos ou executivos
            //var typeOfSeat = _context.Tickets
            //    .AsNoTracking()
            //    .Include(a => a.TravelClass)
            //    .LastOrDefault(a => a.FlightId == flightId);



            if (flightIsInTickets != null)
            {
                

                //vai buscar o ultimo lugar ocupado no voo se o voo existir
                var lastSeatTaken = _context.Tickets
                    .AsNoTracking()
                    .FirstOrDefault(a => a.FlightId == flightId && a.TravelClass == travelClass);


                //ultimo lugar mais um
                var nextSeat = Convert.ToInt32(lastSeatTaken.SeatNumber) + 1;


                //compara o ultimo lugar aos lugares disponiveis
                if (lastSeatTaken == totalSeats)
                {
                    //voo cheio
                    return 0;
                }


                else if (lastSeatTaken.SeatNumber == 0)
                {
                    //primeiro lugar
                    return 1;
                }


                else
                {
                    //ultimo lugar mais um
                    return nextSeat;
                }
            }

            else
            {
                return 1;
            }
        }
    }
}
