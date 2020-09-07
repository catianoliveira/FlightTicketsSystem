using System.ComponentModel.DataAnnotations;

namespace FlightTicketsSystem_FrontOffice.Web.Data.Entities
{
    public class TicketDetail : IEntity
    {
        public int Id { get; set; }


        public User User { get; set; }



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
