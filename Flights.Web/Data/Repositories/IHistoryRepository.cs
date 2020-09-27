using Flights.Web.Data;
using FlightTicketsSystem.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public interface IHistoryRepository : IGenericRepository<History>
    {
        IQueryable GetAllWithUsers();

        Task<List<History>> GetHistoriesFromUserId(int ticketId);
    }
}