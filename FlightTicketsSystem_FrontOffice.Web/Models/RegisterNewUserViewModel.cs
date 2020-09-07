using FlightTicketsSystem_FrontOffice.Web.CustomValidation;
using FlightTicketsSystem_FrontOffice.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem_FrontOffice.Web.Models
{
    public class RegisterNewUserViewModel
    {
        public IdentityRole Role { get; set; }



        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }




        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [LessThanDate]
        public DateTime DateOfBirth { get; set; }




        [Required]
        public int IndicativeId { get; set; }
        public IEnumerable<SelectListItem> Indicatives { get; set; }




        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string Address { get; set; }



        [Required]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string PhoneNumber { get; set; }




        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "You must select a {0}")]
        public int DocumentTypeId { get; set; }




        public IEnumerable<SelectListItem> DocumentTypes { get; set; }




        public DocumentType DocumentType { get; set; }




        public string DocumentNumber { get; set; }




        [Display(Name = "City")]
        [Required]
        public string City { get; set; }


        [Display(Name = "Country")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country")]
        public int CountryId { get; set; }


        public IEnumerable<SelectListItem> Countries { get; set; }


        public Country Country { get; set; }


        [Required]
        [Display(Name = "E-mail")]
        public string EmailAddress { get; set; }



        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}