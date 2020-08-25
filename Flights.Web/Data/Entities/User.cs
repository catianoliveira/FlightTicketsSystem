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

        [Required]
        public string FirstName { get; set; }



        [Required]
        public string LastName { get; set; }



        [Required]
        public DateTime DateOfBirth { get; set; }



        [Required]
        public string Indicative { get; set; }



        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }

        }



        //TODO ver o IdentityUser 

        [Required]
        [MaxLength(70, ErrorMessage = "The field {0} can only contain {1} characters")]
        public string Address { get; set; }



        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters")]
        public string City { get; set; }



        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }




        public Country Country { get; set; }





        public IEnumerable<SelectListItem> Countries { get; set; }



        
    }
}
