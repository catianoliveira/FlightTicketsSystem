using Flights.Web.Data;
using FlightTicketsSystem.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public class HistoryRepository : GenericRepository<History>, IHistoryRepository
    {
        private readonly DataContext _context;

        public HistoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Histories.Include(p => p.Ticket.Flight.CompleteFlight);
        }

        public async Task<List<History>> GetHistoriesFromUserId(int ticketId)
        {
            return await _context.Histories.Where(p => p.Ticket.Id == ticketId).ToListAsync();
        }
    }
}
