using Flights.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Models
{
    public class TicketViewModel
    {
        public string UserId { get; set; }



        public int FlightId { get; set; }


        [Required]
        public string PassangerName { get; set; }



        [Required]
        [Display(Name = "Bought On")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime BoughtOn { get; set; }


    }
}
