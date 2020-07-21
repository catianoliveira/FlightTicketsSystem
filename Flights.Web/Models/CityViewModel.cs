using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Models
{
    public class CityViewModel
    {
        public int CountryId { get; set; }

        public int CityId { get; set; }


        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters")]
        [Required]
        [Display(Name = "City")]
        public string Name { get; set; }
    }
}
