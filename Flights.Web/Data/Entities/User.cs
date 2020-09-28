using FlightTicketsSystem.Web.CustomValidation;
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

        public string RoleId { get; set; }


        public IEnumerable<SelectListItem> RoleChoices { get; set; }


        public IdentityRole Role { get; set; }




        [Required]
        public string FirstName { get; set; }



        [Required]
        public string LastName { get; set; }



        [Required]
        [LessThanDate(ErrorMessage = "Date of birth must be less than today's day")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }




        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }

        }




        [Required]
        [MaxLength(70, ErrorMessage = "The field {0} can only contain {1} characters")]
        public string Address { get; set; }





        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters")]
        public string City { get; set; }





        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }




        public IEnumerable<SelectListItem> Countries { get; set; }



        [Display(Name = "Indicative")]
        public int IndicativeId { get; set; }




        public IEnumerable<SelectListItem> Indicatives { get; set; }


    }
}
