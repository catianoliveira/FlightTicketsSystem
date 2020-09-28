using Flights.Web.Data;
using Flights.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        int GetEconomySeats(int flightId);

        int GetBusinessSeats(int flightId);

        decimal GetEconomyPrice(int flightId);

        decimal GetBusinessPrice(int flightId);

        Task<Ticket> GetDetailsTicketAsync(int flightId);

        Task<List<Ticket>> GetBoughtTickets(string id);


        IQueryable GetAllBoughtByUser(string userId);


        IQueryable GetAllByDate();
    }
}
