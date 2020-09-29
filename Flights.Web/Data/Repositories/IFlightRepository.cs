using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Repositories
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {

        IQueryable GetAllFlights();


        /// <summary>
        /// gets flights from todays date forward
        /// </summary>
        /// <returns></returns>
        IQueryable GetNextFlights();



    }
}
