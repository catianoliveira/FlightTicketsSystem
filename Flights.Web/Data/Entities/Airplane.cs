using System;
using System.ComponentModel.DataAnnotations;

namespace Flights.Web.Data.Entities
{
    public class Airplane : IEntity
    {
        public int Id { get; set; }


        public User User { get; set; }





        //TODO WasDeleted
        //public bool WasDeleted { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        

        [Display(Name = "Economy Class Seats")]
        [Required]
        public int EconomySeats { get; set; }



        [Display(Name = "Business Class Seats")]
        [Required]
        public int BusinessSeats { get; set; }
        



        [Required]
        [Display(Name = "Total Seats")]
        public int Seats
        {
            get
            {
                return EconomySeats + BusinessSeats;
            }
        }

       //TODO ? public User User { get; set; }
    }
}
