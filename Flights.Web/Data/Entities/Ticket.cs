using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Entities
{
    public class Ticket
    {
        public string Id { get; set; }


        [Required]
        [ForeignKey("Flight")]
        public int FlightID { get; set; }



        public Flight Flight { get; set; }


        [Required]
        public string Name { get; set; }



        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "You must select a {0}")]
        public int DocumentTypeId { get; set; }



        public DocumentType DocumentType { get; set; }



        public IEnumerable<SelectListItem> DocumentTypes { get; set; }




        public string DocumentNumber { get; set; }



        public string Seat { get; set; }



        public bool Lugagge { get; set; }
    }
}
