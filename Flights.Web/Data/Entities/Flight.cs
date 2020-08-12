using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Entities
{
    public class Flight : IEntity
    {
        public int Id { get; set; }


        [Display(Name = "Airplane")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select an airport.")]
        public int AirplaneId { get; set; }


        [ForeignKey("DepartureAirportId")]
        public Airport DepartureAirport { get; set; }

        [ForeignKey("ArrivalAirportId")]
        public Airport ArrivalAirport { get; set; }


        public IEnumerable<Airport> Airports { get; set; }

        public IEnumerable<Airplane> Airplanes { get; set; }

    }
}
