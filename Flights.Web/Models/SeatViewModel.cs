using System;
using System.ComponentModel.DataAnnotations;

namespace FlightTicketsSystem.Web.Models
{
    public class SeatViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name = "Seat Number")]
        [Range(1, 100)]
        public int SeatNumber { get; set; }

        //public virtual Passenger Passenger { get; set; }

        //[Required]
        //[Display(Name = "Passenger Name")]
        //public int PassengerId { get; set; }

        [Required]
        [Display(Name = "Booked Time")]
        public String Time { get; set; }

       

    }
}

