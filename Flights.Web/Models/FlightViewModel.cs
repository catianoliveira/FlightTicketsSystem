using Flights.Web.Data.Entities;
using FlightTicketsSystem.Web.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace FlightTicketsSystem.Web.Models
{
    public class FlightViewModel : Flight
    {


        public int AirplaneId { get; set; }



        public int DepartureAirportId { get; set; }



        public int ArrivalAirportId { get; set; }



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



    }
}
