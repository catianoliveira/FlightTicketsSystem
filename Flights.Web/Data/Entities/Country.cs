using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Flights.Web.Data.Entities
{
    public class Country : IEntity
    {
        public int Id { get; set; }



        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters")]
        [Required]
        public string Name { get; set; }


        public ICollection<Airport> Airports { get; set; }
    }
}
