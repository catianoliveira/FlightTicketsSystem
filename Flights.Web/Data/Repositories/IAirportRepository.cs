using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public interface IAirportRepository : IGenericRepository<Airport>
    {
        IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboAirports();

        //Task CheckCity(City city);

        //Task<Airport> GetAirportWithCountriesAsync(int id);

        Task<Airport> GetCountries(int id);

    }
}
