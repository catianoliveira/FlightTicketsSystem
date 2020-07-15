using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public class AirportRepository : GenericRepository<Airport>, IAirportRepository
    {
        public AirportRepository(DataContext context) : base(context)
        {

        }

        public IQueryable GetAllWithUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetComboAirports()
        {
            throw new NotImplementedException();
        }
    }
}
