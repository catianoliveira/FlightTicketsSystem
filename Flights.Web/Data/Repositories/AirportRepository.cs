using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public class AirportRepository : GenericRepository<Airport>, IAirportRepository
    {
        private readonly DataContext _context;

        public AirportRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Airports.Include(p => p.User);
        }

        public IEnumerable<SelectListItem> GetComboAirports()
        {
            var list = _context.Airports.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToList();


            list.Insert(0, new SelectListItem
            {
                Text = "Select a airport",
                Value = "0"
            });

            return list;
        }
    }
}
