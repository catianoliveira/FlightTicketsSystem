using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flights.Web.Data.Entities
{
    public class Airport : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Airport Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        

        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }



        public IEnumerable<SelectListItem> Countries { get; set; }


        public string IATA { get; set; }

        
        //public User User { get; set; }


        public string CompleteAirport
        {
            get
            {
                return $"{this.IATA} {this.City} {this.CountryId}";
            }
        }
    }
}
