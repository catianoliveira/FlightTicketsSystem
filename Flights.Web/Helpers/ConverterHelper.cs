using Flights.Web.Data.Entities;
using Flights.Web.Helpers;
using FlightTicketsSystem.Web.Data.Entities;
using FlightTicketsSystem.Web.Models;

namespace FlightTicketsSystem.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly IUserHelper _userHelper;

        public ConverterHelper(
            IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }


        public Flight ToFlight(FlightViewModel model, bool isNew)
        {
            return new Flight
            {
                ArrivalAirport = model.ArrivalAirport,
                DepartureAirport = model.DepartureAirport,
                Airplane = model.Airplane,
                BusinessPrice = model.BusinessPrice,
                EconomyPrice = model.EconomyPrice,
                DateTime = model.DateTime
            };
        }

        public FlightViewModel ToFlightViewModel(Flight model)
        {
            return new FlightViewModel
            {
                ArrivalAirport = model.ArrivalAirport,
                DepartureAirport = model.DepartureAirport,
                Airplane = model.Airplane,
                BusinessPrice = model.BusinessPrice,
                EconomyPrice = model.EconomyPrice,
                DateTime = model.DateTime,
                AirplaneId = model.AirplaneId,
                ArrivalAirportId = model.ArrivalAirportId,
                DepartureAirportId = model.DepartureAirportId,
                Id = model.Id
            };
        }

        public Ticket ToTicket(BuyTicketViewModel model, string userId)
        {
            return new Ticket
            {
                FlightId = model.FlightId,
                PassangerName = model.PassangerName,
                Lugagge = model.Lugagge,
                SeatNumber = model.SeatNumber,
                TravelClass = model.TravelClass
            };
        }

        public BuyTicketViewModel ToBuyTicketViewModel(Ticket model, string userId)
        {
            return new BuyTicketViewModel
            {
                FlightId = model.FlightId,
                PassangerName = model.PassangerName,
                Lugagge = model.Lugagge,
                SeatNumber = model.SeatNumber,
                TravelClass = model.TravelClass                
            };
        }
    }
}
