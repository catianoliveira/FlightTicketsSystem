using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Entities
{
    public class Ticket
    {
        public string Id { get; set; }

        //public Flight FlightId { get; set; }

        public string Name { get; set; }

        public DocumentType DocumentType { get; set; }

        public string DocumentNumber { get; set; }


        public string Seat { get; set; }
    }
}
