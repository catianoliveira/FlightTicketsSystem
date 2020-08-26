using Flights.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Entities
{
    public class ClientViewModel
    {
        public int Id { get; set; }


        public User User { get; set; }


        public ICollection<Ticket> Tickets { get; set; }

        // TODO Agenda maybe? 
        //public ICollection<Agenda> Agendas { get; set; }
    }
}
