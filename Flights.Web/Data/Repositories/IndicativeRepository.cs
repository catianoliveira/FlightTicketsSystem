using Flights.Web.Data;
using FlightTicketsSystem.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public class IndicativeRepository : GenericRepository<Indicative>, IIndicativeRepository
    {
        private readonly DataContext _context;

        public IndicativeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboIndicatives()
        {
            var list = _context.Indicatives.Select(c => new SelectListItem
            {
                Text = c.CountryAndCode,
                Value = c.Id.ToString()

            }).OrderBy(l => l.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select an indicative...)",
                Value = "0"
            });

            return list;
        }
    }
}
