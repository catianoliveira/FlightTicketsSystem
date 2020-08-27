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


        public Airport DepartureAirport { get; set; }


        public Airport ArrivalAirport { get; set; }


        public IEnumerable<SelectListItem> Airports { get; set; }



        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [GreaterThanDateAttributte]
        public DateTime Date { get; set; }



        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        public DateTime Time { get; set; }



        [Required]
        [Display(Name = "Economy C. Price")]
        public double EconomyClassPrice { get; set; }




        [Required]
        [Display(Name = "Business C. Price")]
        public double BusinessClassPrice { get; set; }




        [Required]
        [Display(Name = "First C. Price")]
        public double FirstClassPrice { get; set; }




        [Required]
        [Display(Name = "Last Minute Buy")]
        public int LastMinutePrice { get; set; }




        public List<Ticket> Tickets { get; set; }


        public string EconomyLastMinutePrice
        {
            get
            {
                return $"{((LastMinutePrice * EconomyClassPrice)/100)+EconomyClassPrice}";
            }
        }

        public string BusinessLastMinutePrice
        {
            get
            {
                return $"{((LastMinutePrice * EconomyClassPrice) / 100) + EconomyClassPrice}";
            }
        }

        public string FirstLastMinutePrice
        {
            get
            {
                return $"{((LastMinutePrice * FirstClassPrice) / 100) + FirstClassPrice}";
            }
        }


        public string CompleteFlight
        {
            get
            {
                return $"{this.AirplaneId} - from {this.ArrivalAirportId} to {this.DepartureAirport}";
            }
        }

    }
}
