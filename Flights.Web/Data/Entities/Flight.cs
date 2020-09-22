using FlightTicketsSystem.Web.CustomValidation;
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

        public User user { get; set; }



        [Required]
        [Display(Name = "Airplane")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select an airport.")]
        public int AirplaneId { get; set; }


        public IEnumerable<SelectListItem> Airplanes { get; set; }


        public Airplane Airplane { get; set; }


        [Required]
        [Display(Name = "Departure Airport")]
        [ForeignKey("DepartureAirport")]
        public int DepartureAirportId { get; set; }


        [Required]
        [Display(Name = "Arrival Airport")]
        [ForeignKey("ArrivalAirport")]
        public int ArrivalAirportId { get; set; }


        public ICollection<Airport> ArrivalsCollection { get; set; }


        public ICollection<Airport> DeparturesCollection { get; set; }


        public IEnumerable<SelectListItem> AirportsEnumerable { get; set; }

        public Airport DepartureAirport { get; set; }


        public Airport ArrivalAirport { get; set; }




        [Required]
        [Display(Name = "Date and Time")]
        public DateTime DateTime { get; set; }



        [Required]
        [Display(Name = "Economy C. Price")]
        public decimal EconomyPrice { get; set; }




        [Required]
        [Display(Name = "Business C. Price")]
        public decimal BusinessPrice { get; set; }



        public string CompleteFlight
        {
            get
            {
                return $"{this.DepartureAirport} -> {this.ArrivalAirport}";
            }
        }

    }
}
