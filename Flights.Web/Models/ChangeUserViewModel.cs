﻿using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Models
{
    public class ChangeUserViewModel
    {
        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "You must select a {0}")]
        public int DocumentTypeId { get; set; }


        
        public DocumentType DocumentType { get; set; }


        public IEnumerable<SelectListItem> DocumentTypes { get; set; }



        [Display(Name = "Document Number")]
        [Required]
        public string DocumentNumber { get; set; }


        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }



        [Required]
        [Display(Name = "Phone Number")]
        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string Address { get; set; }



        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters")]
        public string City { get; set; }



        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string PhoneNumber { get; set; }



        [Required(ErrorMessage = "You must select a {0}")]
        [Display(Name = "Country")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country")]
        public int CountryId { get; set; }


        public Country Country { get; set; }


        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}