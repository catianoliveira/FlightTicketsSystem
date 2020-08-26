using Flights.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Entities
{
    public class ManagerViewModel
    {
        public int Id { get; set; }

        public User User { get; set; }
    }
}
