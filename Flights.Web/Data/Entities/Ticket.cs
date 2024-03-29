﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flights.Web.Data.Entities
{
    public class Ticket : IEntity
    {
        public int Id { get; set; }




        [Required]
        public User User { get; set; }



        [Required]
        [Display(Name = "Flight Id")]
        [ForeignKey("Flight")]
        public int FlightId { get; set; }





        [Required]
        [Display(Name = "Passanger Name")]
        public string PassangerName { get; set; }



        [Required]
        [Display(Name = "Seat Number")]
        public int SeatNumber { get; set; }



        [Required]
        [Display(Name = "Travel Class")]
        public string TravelClass { get; set; }



        [Required]
        public bool Lugagge { get; set; }





        public Flight Flight { get; set; }
    }
}
