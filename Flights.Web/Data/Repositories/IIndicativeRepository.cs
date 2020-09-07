using Flights.Web.Data;
using FlightTicketsSystem.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public interface IIndicativeRepository : IGenericRepository<Indicative>
    {
        IEnumerable<SelectListItem> GetComboIndicatives();

    }
}
