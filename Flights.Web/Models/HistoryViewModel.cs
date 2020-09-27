using FlightTicketsSystem.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightTicketsSystem.Web.Models
{
    public class HistoryViewModel : History
    {
        public int UserId { get; set; }



        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Service Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a service type.")]
        public int TicketId { get; set; }



        public IEnumerable<SelectListItem> Tickets { get; set; }
    }
}
