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
using Microsoft.AspNetCore.Authorization;

namespace FlightTicketsSystem.Web.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin, Employee, Client")]
    public class TicketsController : Controller
    {
        private readonly DataContext _context;
        private readonly ICountryRepository _countryRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IIndicativeRepository _indicativeRepository;
        private readonly IUserHelper _userHelper;
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IMailHelper _mailHelper;
        private readonly IAirportRepository _airportRepository;

        public TicketsController(
            DataContext context,
            ICountryRepository countryRepository,
            IFlightRepository flightRepository,
            ITicketRepository ticketRepository,
            IAirportRepository airportRepository,
            IIndicativeRepository indicativeRepository,
            IUserHelper userHelper,
            IAirplaneRepository airplaneRepository,
            IMailHelper mailHelper)
        {
            _context = context;
            _countryRepository = countryRepository;
            _flightRepository = flightRepository;
            _ticketRepository = ticketRepository;
            _indicativeRepository = indicativeRepository;
            _userHelper = userHelper;
            _airplaneRepository = airplaneRepository;
            _mailHelper = mailHelper;
            _airportRepository = airportRepository;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Client"))
            {
                var user = User.Identity.Name;

                var client = _ticketRepository.GetAllBoughtByUser(user);

                return View(client);
            }

            else
            {
                return View(_ticketRepository.GetAllByDate());
            }            
        }

        [AllowAnonymous]
        public IActionResult ChooseFlight()
        {
            var model = _flightRepository.GetTodaysFlights();

            return View(model);
        }

        // GET: Tickets/Details/5
        [Authorize(Roles = "Admin, SuperAdmin, Employee")]
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


        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin, SuperAdmin, Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketRepository.GetByIdAsync(id.Value);

            if (ticket == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);


            var model = new Ticket
            {
                PassangerName = ticket.PassangerName,
                User = user,
                TravelClass = ticket.TravelClass,
            };

            return View(model);
        }

        // POST: Airplanes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, SuperAdmin, Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _ticketRepository.UpdateAsync(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _ticketRepository.ExistAsync(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }


        [Authorize(Roles = "Admin, SuperAdmin, Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _ticketRepository.GetByIdAsync(id.Value);

            if (ticket == null)
            {
                return NotFound();
            }

            try
            {
                await _ticketRepository.DeleteAsync(ticket);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }


        [AllowAnonymous]
        public async Task<IActionResult> BuyTicket(int id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (id == 0)
                {
                    return NotFound();
                }

                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                var businessPrice = _ticketRepository.GetBusinessPrice(id);
                var economyPrice = _ticketRepository.GetEconomyPrice(id);

                var model = new BuyTicketViewModel
                {
                    FlightId = id,
                    PhoneNumber = user.PhoneNumber,
                    IndicativeId = user.IndicativeId,
                    Indicatives = _indicativeRepository.GetComboIndicatives(),
                    PassangerName = user.FullName,
                    EconomyPrice = economyPrice,
                    BusinessPrice = businessPrice
                };

                return View(model);
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [Authorize(Roles = "Client, SuperAdmin, Admin, Employee")]
        [HttpPost]
        public async Task<IActionResult> BuyTicket(BuyTicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

                    if (model.TravelClass.Contains("Economy"))
                    {
                        var nextSeat = _ticketRepository.GetEconomySeats(model.FlightId);

                        if (nextSeat == 0)
                        {
                            this.ModelState.AddModelError(string.Empty, "Flight is full");
                        }

                        else
                        {
                            var ticket = new Ticket
                            {
                                FlightId = model.FlightId,
                                PassangerName = model.PassangerName,
                                TravelClass = "Economy",
                                SeatNumber = nextSeat,
                                User = user,
                            };

                            await _ticketRepository.CreateAsync(ticket);

                            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                            var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                            {
                                userid = user.Id,
                                token = myToken
                            }, protocol: HttpContext.Request.Scheme);


                            var flight = await _flightRepository.GetByIdAsync(model.FlightId);

                            var arrival = await _airportRepository.GetByIdAsync(flight.ArrivalAirportId);

                            var departure = await _airportRepository.GetByIdAsync(flight.DepartureAirportId);


                            _mailHelper.SendMail(user.Email, $"Your reservation for flight nº HFA000{ticket.FlightId}:", $"<h1>Was completed successfully!</h1>" +
                                $"Here are the details:<br/>From {departure.CompleteAirport} to {arrival.CompleteAirport}<br/>" +
                                $"On: {flight.DateTime}<br/><br/>Name: {ticket.PassangerName}<br/>Travel Class: {ticket.TravelClass}<br/>" +
                                $"Seat Number: {ticket.SeatNumber}");
                        }
                    }

                    else if (model.TravelClass.Contains("Business"))
                    {
                        var nextSeat = _ticketRepository.GetBusinessSeats(model.FlightId);

                        if (nextSeat == 0)
                        {
                            this.ModelState.AddModelError(string.Empty, "Flight is full");
                        }

                        else
                        {
                            var ticket = new Ticket
                            {
                                FlightId = model.FlightId,
                                PassangerName = model.PassangerName,
                                TravelClass = "Business",
                                SeatNumber = nextSeat,
                                User = user
                            };

                            await _ticketRepository.CreateAsync(ticket);

                            var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                            var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                            {
                                userid = user.Id,
                                token = myToken
                            }, protocol: HttpContext.Request.Scheme);

                            var flight = await _flightRepository.GetByIdAsync(model.FlightId);

                            var arrival = await _airportRepository.GetByIdAsync(flight.ArrivalAirportId);

                            var departure = await _airportRepository.GetByIdAsync(flight.DepartureAirportId);


                            _mailHelper.SendMail(user.Email, $"Your reservation for flight nº HFA000{ticket.FlightId}:", $"<h1>Was completed successfully!</h1>" +
                                $"Here are the details:<br/>From {departure.CompleteAirport} to {arrival.CompleteAirport}<br/>" +
                                $"On: {flight.DateTime}<br/><br/>Name: {ticket.PassangerName}<br/>Travel Class: {ticket.TravelClass}<br/>" +
                                $"Seat Number: {ticket.SeatNumber}");
                        }
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

