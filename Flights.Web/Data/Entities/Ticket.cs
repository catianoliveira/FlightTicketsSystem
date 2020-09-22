using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Entities
{
    public class Ticket : IEntity
    {
        public int Id { get; set; }


        [Required]
        public User User { get; set; }


        
        [Required]
        [ForeignKey("Flight")]
        public int FlightId { get; set; }


        public Flight Flight { get; set; }



        [Required]
        public string PassangerName { get; set; }



        public int SeatNumber { get; set; }



        [Required]
        public string TravelClass { get; set; }



        [Required]
        public bool Lugagge { get; set; }


    }
}
