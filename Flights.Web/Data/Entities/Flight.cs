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


        public IEnumerable<SelectListItem> Airplanes { get; set; }


        public Airplane Airplane { get; set; }


        [ForeignKey("DepartureAirport")]
        public int DepartureAirportId { get; set; }


        [ForeignKey("ArrivalAirport")]
        public int ArrivalAirportId { get; set; }


        public Airport DepartureAirport { get; set; }

        public Airport ArrivalAirport { get; set; }


        public IEnumerable<SelectListItem> Airports { get; set; }

        //public IEnumerable<Airport> DepartureAirports { get; set; }






        public string CompleteFlight
        {
            get
            {
                return $"{this.AirplaneId}";
            }
        }

    }
}
