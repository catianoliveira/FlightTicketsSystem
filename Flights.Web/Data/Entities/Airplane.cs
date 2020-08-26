using System.ComponentModel.DataAnnotations;

namespace Flights.Web.Data.Entities
{
    public class Airplane : IEntity
    {
        public int Id { get; set; }

        //TODO WasDeleted
        //public bool WasDeleted { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Display(Name = "Economy Class Seats")]
        [Required]
        public int EconomyClassSeats { get; set; }



        [Display(Name = "Business Class Seats")]
        [Required]
        public int BusinessClassSeats { get; set; }
        



        [Display(Name = "First Class Seats")]
        [Required]
        public int FirstClassSeats { get; set; }

        public int _seats;

        [Required]
        [Display(Name = "Total Seats")]
        public int Seats
        {
            get
            {
                return EconomyClassSeats + FirstClassSeats + BusinessClassSeats;
            }

            set
            {
                _seats = value;
            }
        }

       //TODO ? public User User { get; set; }
    }
}
