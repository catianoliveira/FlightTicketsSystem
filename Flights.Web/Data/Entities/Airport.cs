using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Entities
{
    public class Airport : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(3)]
        [Display(Name = "Airport")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public User User { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
