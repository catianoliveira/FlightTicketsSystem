using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Flights.Web.Data
{
    public interface IAirplaneRepository : IGenericRepository<Airplane>
    {
        IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboAirplanes();
    }
}
