using Flights.Web.Data.Entities;
using FlightTicketsSystem.Web.Data.Entities;
using FlightTicketsSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Helpers
{
    public interface IConverterHelper
    {
        Flight ToFlight(FlightViewModel model, bool isNew);

        FlightViewModel ToFlightViewModel(Flight model);

        Ticket ToTicket(BuyTicketViewModel model, string userId);

        BuyTicketViewModel ToBuyTicketViewModel(Ticket model, string userId);
    }
}
