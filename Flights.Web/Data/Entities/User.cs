using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }

        }

        //TODO ver o IdentityUser 

        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters")]
        public string Address { get; set; }


        //public int CityId { get; set; }

        //public City City { get; set; }
    }
}
