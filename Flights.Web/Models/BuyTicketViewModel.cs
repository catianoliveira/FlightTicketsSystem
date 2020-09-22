using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Models
{
    public class BuyTicketViewModel
    {
        public User User { get; set; }



        public int FlightId { get; set; }


        [Required]
        public string PassangerName { get; set; }


        public Flight Flight { get; set; }


        public int SeatNumber { get; set; }



        [Required]
        public string TravelClass { get; set; }


        [Required]
        public string PhoneNumber { get; set; }



        [Display(Name = "Indicative")]
        public int IndicativeId { get; set; }




        public IEnumerable<SelectListItem> Indicatives { get; set; }


        [Required]
        public bool Lugagge { get; set; }


        public decimal Price { get; set; }
    }
}
