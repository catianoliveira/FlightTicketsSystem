using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public class AirplaneRepository : GenericRepository<Airplane>, IAirplaneRepository
    {
        private readonly DataContext _context;

        public AirplaneRepository(DataContext context) : base(context)
        {
            //é necessario o interface porque no startup temos que 
            //implementar o interface e o T
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetComboAirplanes()
        {
            var list = _context.Airplanes.OrderBy(p => p.Model).Select(p => new SelectListItem
            {
                Text = p.Model,
                Value = p.Id.ToString()

            }).ToList();


            list.Insert(0, new SelectListItem
            {
                Text = "Select a airplane",
                Value = "0"
            });

            return list;
        }
    }
}
