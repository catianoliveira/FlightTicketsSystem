using Flights.Web.Data;
using Flights.Web.Data.Entities;
using FlightTicketsSystem.Web.Models;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        //IEnumerable<SelectListItem> GetComboTickets();

        int GetEconomySeats(int flightId);

        int GetBusinessSeats(int flightId);

        decimal GetEconomyPrice(int flightId);

        decimal GetBusinessPrice(int flightId);

        Task<Ticket> GetDetailsTicketAsync(int flightId);
    }
}
