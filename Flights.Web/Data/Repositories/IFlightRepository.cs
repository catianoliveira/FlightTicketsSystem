using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Repositories
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        IEnumerable<SelectListItem> GetComboFlights();

        //Task<Flight> GetAirports(int id);

        Task<Flight> GetAirplanesAsync(int id);
    }
}
