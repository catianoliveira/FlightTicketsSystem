using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flights.Web.Data.Repositories
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        IEnumerable<SelectListItem> GetComboDepartures();

        IEnumerable<SelectListItem> GetComboArrivals(int departureId);


        Task<Flight> GetAirplanesAsync(int id);


        Task<Flight> GetDeparturesWithArrivalsAsync(int id);


        int GetEconomySeats(int flightId);

        int GetBusinessSeats(int flightId);

        //Ticket GetLastSeat(int? id);

    }
}
