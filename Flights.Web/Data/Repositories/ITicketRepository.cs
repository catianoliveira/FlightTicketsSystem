using Flights.Web.Data;
using Flights.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {

        /// <summary>
        /// checks which airplane if being used in flighId, then how many seats the airplane has.
        /// checks if there's already a ticket for the flight, otherwise, sear number is 0.
        /// if there is, checks if flight is full, if not, add 1 
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        int GetEconomySeats(int flightId);


        /// <summary>
        /// checks which airplane if being used in flighId, then how many seats the airplane has.
        /// checks if there's already a ticket for the flight, otherwise, sear number is 0.
        /// if there is, checks if flight is full, if not, add 1 
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        int GetBusinessSeats(int flightId);




        /// <summary>
        /// returns business price for the flight
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        decimal GetEconomyPrice(int flightId);



        /// <summary>
        /// returns economy price for the flight
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        decimal GetBusinessPrice(int flightId);


        IQueryable GetAllBoughtByUser(string userId);


        IQueryable GetAllByDate();
    }
}
