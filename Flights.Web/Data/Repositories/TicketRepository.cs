using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Flights.Web.Data.Repositories;
using Flights.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
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


       
        public int GetBusinessSeats(int flightId)
        {
            var airplane = _context.Flights
                .AsNoTracking()
                .Where(a => a.Airplane.Id == a.AirplaneId)
                .FirstOrDefault(a => a.Id == flightId);


            var totalSeats = _context.Airplanes
                .AsNoTracking()
                .Include(a => a.BusinessSeats)
                .Where(a => a.Id == airplane.Id);


            var travelClass = "Business";

            //vê se o voo existe nos bilhetes
            var flightIsInTickets = _context.Tickets
                .AsNoTracking()
                .LastOrDefault(a => a.FlightId == flightId && a.TravelClass == travelClass);


            if (flightIsInTickets != null)
            {
                var lastSeatTaken = _context.Tickets
                    .AsNoTracking()
                    .LastOrDefault(a => a.FlightId == flightId && a.TravelClass == travelClass);

                var nextSeat = Convert.ToInt32(lastSeatTaken.SeatNumber) + 1;


                if (lastSeatTaken == totalSeats)
                {
                    //voo cheio
                    return 0;
                }


                else if (lastSeatTaken.SeatNumber == 0)
                {
                    return 1;
                }


                else
                {
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
            var airplane = _context.Flights
                .AsNoTracking()
                .Where(a => a.Airplane.Id == a.AirplaneId)
                .FirstOrDefault(a => a.Id == flightId);


            var totalSeats = _context.Airplanes
                .AsNoTracking()
                .Include(a => a.EconomySeats)
                .Where(a => a.Id == airplane.Id);

            var travelClass = "Economy";

            var flightIsInTickets = _context.Tickets
                .AsNoTracking()
                .LastOrDefault(a => a.FlightId == flightId && a.TravelClass == travelClass);


            if (flightIsInTickets != null)
            {
                var lastSeatTaken = _context.Tickets
                    .AsNoTracking()
                    .LastOrDefault(a => a.FlightId == flightId && a.TravelClass == travelClass);


                var nextSeat = Convert.ToInt32(lastSeatTaken.SeatNumber) + 1;


                if (lastSeatTaken == totalSeats)
                {
                    return 0;
                }


                else if (lastSeatTaken.SeatNumber == 0)
                {
                    return 1;
                }


                else
                {
                    return nextSeat;
                }
            }

            else
            {
                return 1;
            }
        }


        /// <summary>
        /// returns economy price for the flight
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public decimal GetEconomyPrice(int flightId)
        {
            var price = _context.Flights
                .AsNoTracking()
                .FirstOrDefault(f => f.Id == flightId);


            return price.EconomyPrice;
        }



        /// <summary>
        /// returns business price for the flight
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public decimal GetBusinessPrice(int flightId)
        {
            var price = _context.Flights
                .AsNoTracking()
                .FirstOrDefault(f => f.Id == flightId);


            return price.BusinessPrice;
        }

       


        public IQueryable GetAllBoughtByUser(string user)
        {
           return _context.Tickets
            .Include(a => a.Flight.DepartureAirport)
            .Include(a => a.Flight.ArrivalAirport)
            .OrderBy(p => p.Flight.DateTime)
            .Where(a => a.User.Email == user)
            .Where(a => a.Flight.DateTime >= DateTime.Today.ToUniversalTime());
        }



        public IQueryable GetAllByDate()
        {
            return _context.Tickets
                 .Include(a => a.Flight.DepartureAirport)
                 .Include(a => a.Flight.ArrivalAirport)
                 .OrderBy(p => p.Flight.DateTime)
                 .Where(a => a.Flight.DateTime >= DateTime.Today.ToUniversalTime());
        }
    }
}
