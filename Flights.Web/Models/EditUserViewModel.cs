﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }


        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }



        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }


        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }


        public IEnumerable<SelectListItem> Roles { get; set; }


        [Display(Name = "Role")]
        public string SelectedRole { get; set; }



        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters")]
        public string City { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }



        [Display(Name = "Indicative")]
        public int IndicativeId { get; set; }




        public IEnumerable<SelectListItem> Indicatives { get; set; }

    }
}
