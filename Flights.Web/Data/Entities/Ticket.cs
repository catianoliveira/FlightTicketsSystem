using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Entities
{
    public class Ticket : IEntity
    {
        public int Id { get; set; }


        [Required]
        public User User { get; set; }


        [Required]
        [ForeignKey("Departure Airport")]
        public int DepartureAirportId { get; set; }


        [Required]
        [ForeignKey("Arrival Airport")]
        public int ArrivalAirportId { get; set; }


        public Flight DepartureAirport { get; set; }



        public Flight ArrivalAirport { get; set; }



        public IEnumerable<SelectListItem> ArrivalAirports { get; set; }

        public IEnumerable<SelectListItem> DepartureAirports { get; set; }




        [Required]
        public string PassangerName { get; set; }



        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "You must select a {0}")]
        public int DocumentTypeId { get; set; }



        public DocumentType DocumentType { get; set; }



        public IEnumerable<SelectListItem> DocumentTypes { get; set; }



        public string DocumentNumber { get; set; }



        public int SeatNumber { get; set; }


        public string TravelClass { get; set; }


        public bool Lugagge { get; set; }


        public string CompleteTicket
        {
            get
            {
                return $"{this.ArrivalAirport} {this.DepartureAirport}";
            }
        }
    }
}
