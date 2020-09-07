using Flights.Web.Data;
using Flights.Web.Data.Entities;
using FlightTicketsSystem.Web.Models;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        //IEnumerable<SelectListItem> GetComboTickets();



    }
}
