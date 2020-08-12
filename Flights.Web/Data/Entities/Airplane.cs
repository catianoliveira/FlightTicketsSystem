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

        [Display(Name = "Economic Seats")]
        [Required]
        public int EconomicSeats { get; set; }

        [Display(Name = "Executive Seats")]
        [Required]
        public int ExecutiveSeats { get; set; }

        public int _seats;

        [Required]
        [Display(Name = "Total Seats")]
        public int Seats
        {
            get
            {
                return EconomicSeats + ExecutiveSeats;
            }

            set
            {
                _seats = value;
            }
        }

        public User User { get; set; }
    }
}
