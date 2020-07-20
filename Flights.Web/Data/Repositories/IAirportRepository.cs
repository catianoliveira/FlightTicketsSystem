using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Flights.Web.Data
{
    public interface IAirportRepository : IGenericRepository<Airport>
    {
        IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboAirports();
    }
}
