using Flights.Web.Data.Entities;
using FlightTicketsSystem.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Repositories
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        IEnumerable<SelectListItem> GetComboDepartures();

        IEnumerable<SelectListItem> GetComboArrivals(int departureId);


        Task<Flight> GetAirplanesAsync(int id);


        Task<Flight> GetDeparturesWithArrivalsAsync(int id);


        Task<int> GetEconomySeats(int flightId);

    }
}
