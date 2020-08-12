using Flights.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Models
{
    public class AirportsViewModel : Airport
    {
        public Country Country { get; set; }
    }
}
