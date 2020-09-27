using Flights.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Entities
{
    public class Employee : IEntity
    {

        [Key]
        public int Id { get; set; }

        public User UserId { get; set; }

        [Required(ErrorMessage = "Must insert the {0}")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        
        [Display(Name = "Phone Number")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Must insert the {0}")]
        [EmailAddress]
        public string Email { get; set; }
    }
}