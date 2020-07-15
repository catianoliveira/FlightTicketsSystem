using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Entities
{
    public class Flight : IEntity
    {
        public int Id { get; set; }

        public Airport From { get; set; }

        public Airport To { get; set; }

        public Airplane Airplane { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public bool WasDeleted { get; set; }

    }
}
