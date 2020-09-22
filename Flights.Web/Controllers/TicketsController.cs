using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flights.Web.Data;
using Flights.Web.Data.Entities;
using FlightTicketsSystem.Web.Data.Repositories;
using Flights.Web.Data.Repositories;
using FlightTicketsSystem.Web.Models;
using Flights.Web.Helpers;

namespace FlightTicketsSystem.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICountryRepository _countryRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IIndicativeRepository _indicativeRepository;
        private readonly IUserHelper _userHelper;
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IAirportRepository _airportRepository;

        public TicketsController(
            DataContext context,
            ICountryRepository countryRepository,
            IFlightRepository flightRepository,
            ITicketRepository ticketRepository,
            IAirportRepository airportRepository,
            IIndicativeRepository indicativeRepository,
            IUserHelper userHelper,
            IAirplaneRepository airplaneRepository)
        {
            _context = context;
            _countryRepository = countryRepository;
            _flightRepository = flightRepository;
            _ticketRepository = ticketRepository;
            _indicativeRepository = indicativeRepository;
            _userHelper = userHelper;
            _airplaneRepository = airplaneRepository;
            _airportRepository = airportRepository;
        }

        // GET: Tickets
        public IActionResult Index()
        {
            var tickets = _context.Tickets
                .Include(t => t.FlightId)
                .Include(t => t.Flight.ArrivalAirport.CompleteAirport)
                .Include(t => t.Flight.DepartureAirport.CompleteAirport)
                .Include(t => t.PassangerName)
                .Include(t => t.TravelClass)
                .Include(t => t.SeatNumber)
                .Include(t => t.Lugagge)
                .Where(a => a.Flight.DateTime >= DateTime.Today.ToUniversalTime());

            return View(tickets);
        }



        public IActionResult ChooseFlight()
        {
            var model = _context.Flights
                .Include(a => a.Airplane)
                .Include(a => a.DepartureAirport)
                .Include(a => a.ArrivalAirport)
                .OrderBy(p => p.DateTime);

            return View(model.ToList());
        }



        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        //public IActionResult Create()
        //{
        //    var model = new Ticket
        //    {
        //        //ArrivalAirports = _flightRepository.GetComboArrivals(0),
        //        //DepartureAirports = _flightRepository.GetComboDepartures()
        //    };

        //    return this.View(model);
        //}


        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Ticket tickets)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //await _flightRepository.CreateAsync(flight);

        //        Flight flight = _context.Flights.Find(tickets.FlightId);

        //        Ticket ticket = new Ticket
        //        {
        //            FlightId = flight.Id,
        //            PassangerName = tickets.PassangerName,
        //            TravelClass = tickets.TravelClass,
        //            SeatNumber = 1
        //            //TODO user
        //        };

        //        await _ticketRepository.CreateAsync(ticket);

        //        return RedirectToAction(nameof(Index));

        //    }
        //    return View(tickets);
        //}

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightID,PassangerName,DocumentTypeId,DocumentNumber,SeatNumber,TravelClass,Lugagge")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Flight)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }


        public async Task<JsonResult> GetArrivalsAsync(int departureAirportId)
        {
            var arrivalAirports = await _flightRepository.GetDeparturesWithArrivalsAsync(departureAirportId);
            return this.Json(arrivalAirports.ArrivalsCollection.OrderBy(c => c.Country));
        }


        public async Task<IActionResult> BuyTicket(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            var model = new BuyTicketViewModel
            {
                FlightId = id.Value,
                PhoneNumber = user.PhoneNumber,
                IndicativeId = user.IndicativeId,
                Indicatives = _indicativeRepository.GetComboIndicatives(),
                PassangerName = user.FullName,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BuyTicket(BuyTicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.TravelClass == "Economy")
                {
                    var nextSeat = _flightRepository.GetEconomySeats(model.FlightId);

                    if (nextSeat == 0)
                    {
                        this.ModelState.AddModelError(string.Empty, "Flight is full");
                    }

                    else
                    {
                        var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

                        var ticket = new Ticket
                        {
                            FlightId = model.FlightId,
                            PassangerName = model.PassangerName,
                            TravelClass = model.TravelClass,
                            SeatNumber = nextSeat,
                            User = user,

                        };


                        await _ticketRepository.CreateAsync(ticket);
                    }
                }

                else if (model.TravelClass == "Business")
                {
                    var nextSeat = _flightRepository.GetBusinessSeats(model.FlightId);

                    if (nextSeat == 0)
                    {
                        this.ModelState.AddModelError(string.Empty, "Flight is full");
                    }

                    else
                    {
                        var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

                        var ticket = new Ticket
                        {
                            FlightId = model.FlightId,
                            PassangerName = model.PassangerName,
                            TravelClass = model.TravelClass,
                            SeatNumber = nextSeat,
                            User = user,

                        };


                        await _ticketRepository.CreateAsync(ticket);
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
