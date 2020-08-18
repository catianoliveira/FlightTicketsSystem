using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Entities
{
    public class User : IdentityUser
    {
        //TODO cada user tem uma classe e um controlador


        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }



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




        public string PhoneNumber { get; set; }



        //TODO ver o IdentityUser 

        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters")]
        public string Address { get; set; }


        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }


        public IEnumerable<SelectListItem> Countries { get; set; }



        [Display(Name = "Full Name")]
        public string FullNameWithDocument
        {
            get
            {
                return $"{FirstName} {LastName} - {Document}";
            }

        }
    }
}
