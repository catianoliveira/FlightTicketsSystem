using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Models
{
    public class DeactivateViewModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }
    }
}
