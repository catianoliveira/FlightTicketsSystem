using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Flights.Web.Helpers;
using FlightTicketsSystem.Web.Data.Entities;
using FlightTicketsSystem.Web.Models;

namespace FlightTicketsSystem.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly IUserHelper _userHelper;
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IAirportRepository _airportRepository;

        public ConverterHelper(
            IUserHelper userHelper,
            IAirplaneRepository airplaneRepository,
            IAirportRepository airportRepository)
        {
            _userHelper = userHelper;
            _airplaneRepository = airplaneRepository;
            _airportRepository = airportRepository;
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
                DateTime = model.DateTime,
                Tickets = model.Tickets
            };
        }

        public FlightViewModel ToFlightViewModel(Flight model)
        {
            return new FlightViewModel
            {
                Airplanes = _airplaneRepository.GetComboAirplanes(),
                Airports = _airportRepository.GetComboAirports(),
                ArrivalAirport = model.ArrivalAirport,
                DepartureAirport = model.DepartureAirport,
                Airplane = model.Airplane,
                BusinessPrice = model.BusinessPrice,
                EconomyPrice = model.EconomyPrice,
                DateTime = model.DateTime,
                AirplaneId = model.AirplaneId,
                ArrivalAirportId = model.ArrivalAirportId,
                DepartureAirportId = model.DepartureAirportId,
                Id = model.Id,
                Tickets = model.Tickets
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
