using Flights.Web.Data.Entities;
using FlightTicketsSystem.Web.CustomValidation;
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


        [Required]
        public string FirstName { get; set; }



        [Required]
        public string LastName { get; set; }



        [Required]
        [LessThanDate(ErrorMessage = "Date of birth must be less than today's day")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }


    }
}
