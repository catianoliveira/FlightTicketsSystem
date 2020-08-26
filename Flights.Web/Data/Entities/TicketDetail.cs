using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Entities
{
    public class TicketDetail
    {
        public int Id { get; set; }

        [Required]
        public Flight Flight { get; set; }



        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }




        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Passangers { get; set; }


        


        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get { return this.Price * (decimal)this.Passangers; } }

    }
}
