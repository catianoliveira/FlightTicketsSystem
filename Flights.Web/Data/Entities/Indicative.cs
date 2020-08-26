using Flights.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Data.Entities
{
    public class Indicative : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Code { get; set; }

        public string CountryAndCode
        {
            get
            {
                return $"{this.Country} - {this.Code}";
            }
        }
    }
}
