using Flights.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Entities
{
    public class History : IEntity
    {
        public int Id { get; set; }

        public Ticket Ticket { get; set; }

        public User User { get; set; }

    }
}