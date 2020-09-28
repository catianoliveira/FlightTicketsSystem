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
            //vai buscar o id do aviao
            var airplane = _context.Flights
                .AsNoTracking()
                .Where(a => a.Airplane.Id == a.AirplaneId)
                .FirstOrDefault(a => a.Id == flightId);


            //vai buscar a quantidade de lugares que há no avião
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
                //vai buscar o ultimo lugar ocupado no voo se o voo existir
                var lastSeatTaken = _context.Tickets
                    .AsNoTracking()
                    .LastOrDefault(a => a.FlightId == flightId && a.TravelClass == travelClass);


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
            //vai buscar o id do aviao
            var airplane = _context.Flights
                .AsNoTracking()
                .Where(a => a.Airplane.Id == a.AirplaneId)
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
                .LastOrDefault(a => a.FlightId == flightId && a.TravelClass == travelClass);


            if (flightIsInTickets != null)
            {
                //vai buscar o ultimo lugar ocupado no voo se o voo existir
                var lastSeatTaken = _context.Tickets
                    .AsNoTracking()
                    .LastOrDefault(a => a.FlightId == flightId && a.TravelClass == travelClass);


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
                    //ultimo lugar ocupado mais um
                    return nextSeat;
                }
            }

            else
            {
                return 1;
            }
        }

        public decimal GetEconomyPrice(int flightId)
        {
            //vê o preço dos bilhetes
            var price = _context.Flights
                .AsNoTracking()
                .FirstOrDefault(f => f.Id == flightId);


            return price.EconomyPrice;
        }

        public decimal GetBusinessPrice(int flightId)
        {
            //vê o preço dos bilhetes
            var price = _context.Flights
                .AsNoTracking()
                .FirstOrDefault(f => f.Id == flightId);


            return price.BusinessPrice;
        }

        public async Task<Ticket> GetDetailsTicketAsync(int ticketId)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Flight.CompleteFlight)
                .Include(t => t.Flight.DateTime)
                .Include(t => t.PassangerName)
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            return ticket;
        }

        public async Task<List<Ticket>> GetBoughtTickets(string id)
        {
            var tickets = await _context.Tickets
                .AsNoTracking()
                .Include(t => t.PassangerName)
                .Where(t => t.User.Id == id)
                .ToListAsync();

            return tickets;
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
