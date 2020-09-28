using Flights.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Models
{
    public class TicketViewModel
    {
        public string UserId { get; set; }



        public int FlightId { get; set; }


        [Required]
        public string PassangerName { get; set; }




        
        public int SeatNumber { get; set; }



        
        public string TravelClass { get; set; }



        
        public bool Lugagge { get; set; }

    }
}
