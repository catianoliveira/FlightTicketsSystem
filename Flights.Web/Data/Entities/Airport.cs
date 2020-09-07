using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Flights.Web.Data.Entities
{
    public class Airport : IEntity
    {

        Country country = new Country();

        public int Id { get; set; }

        [Required]
        [Display(Name = "Airport Name")]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }


        [Required]
        public string Country { get; set; }



        public string IATA { get; set; }


        //public User User { get; set; }


        public string CompleteAirport
        {
            get
            {
                return $"{this.IATA} {this.City} {this.Country}";           
            }
        }
    }
}
