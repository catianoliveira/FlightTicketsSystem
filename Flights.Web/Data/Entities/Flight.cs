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

        
        [Required]
        [Display(Name = "Airplane")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select an airport.")]
        public int AirplaneId { get; set; }






        [Display(Name = "Airplane")]
        public IEnumerable<SelectListItem> Airplanes { get; set; }





        [Required]
        [Display(Name = "Departure Airport")]
        [ForeignKey("DepartureAirport")]
        public int DepartureAirportId { get; set; }




        [Required]
        [Display(Name = "Arrival Airport")]
        [ForeignKey("ArrivalAirport")]
        public int ArrivalAirportId { get; set; }



        public IEnumerable<SelectListItem> Airports { get; set; }


        



        [Required]
        [GreaterThanDateAttributte(ErrorMessage = "Flight's date must be equal or greater than today's")]
        [Display(Name = "Date and Time")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }





        [Required]
        [Display(Name = "Economy C. Price")]
        public decimal EconomyPrice { get; set; }






        [Required]
        [Display(Name = "Business C. Price")]
        public decimal BusinessPrice { get; set; }




        public ICollection<Ticket> Tickets { get; set; }





        public string CompleteFlight
        {
            get
            {
                return $"{this.DepartureAirport} -> {this.ArrivalAirport}";
            }
        }




        [Display(Name = "Departure Airport")]
        public Airport DepartureAirport { get; set; }




        [Display(Name = "Arrival Airport")]
        public Airport ArrivalAirport { get; set; }



        public Airplane Airplane { get; set; }


    }
}
