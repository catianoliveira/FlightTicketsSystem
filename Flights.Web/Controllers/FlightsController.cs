using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Flights.Web.Data.Repositories;
using FlightTicketsSystem.Web.Helpers;
using FlightTicketsSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Flights.Web.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin, Employee")]

    public class FlightsController : Controller
    {
        private readonly DataContext _context;
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IConverterHelper _converterHelper;

        public FlightsController(
            DataContext context,
            IAirplaneRepository airplaneRepository,
            IFlightRepository flightRepository,
            IAirportRepository airportRepository,
            IConverterHelper converterHelper)
        {
            _context = context;
            _airplaneRepository = airplaneRepository;
            _flightRepository = flightRepository;
            _airportRepository = airportRepository;
            _converterHelper = converterHelper;
        }

        // GET: Flights
        public IActionResult Index()
        {
            var flights = _flightRepository.GetAllFlights();
            return View(flights);
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //TODO
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }



        // GET: Flights/Create
        public IActionResult Create()
        {
            var model = new Flight
            {
                Airplanes = _airplaneRepository.GetComboAirplanes(),
                Airports = _airportRepository.GetComboAirports()
            };

            return this.View(model);
        }



        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FlightViewModel flights)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (flights.ArrivalAirportId != flights.DepartureAirportId)
                    {


                        Flight flight = new Flight
                        {
                            ArrivalAirportId = flights.ArrivalAirportId,
                            DepartureAirportId = flights.DepartureAirportId,
                            AirplaneId = flights.AirplaneId,
                            BusinessPrice = flights.BusinessPrice,
                            EconomyPrice = flights.EconomyPrice,
                            DateTime = flights.DateTime,
                            ArrivalAirport = flights.ArrivalAirport,
                            DepartureAirport = flights.DepartureAirport,
                            Tickets = new List<Ticket>()
                        };

                        await _flightRepository.CreateAsync(flight);
                    }

                    else
                    {
                        ModelState.AddModelError(string.Empty, "Arrival Airport and Departure Airport can't be the same");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }



        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _flightRepository.GetByIdAsync(id.Value);

            if (model == null)
            {
                return NotFound();
            }

            var flight = _converterHelper.ToFlightViewModel(model);

            return View(flight);
        }




        // POST: Airplanes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Flight flight)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _flightRepository.UpdateAsync(flight);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _flightRepository.ExistAsync(flight.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Erro");
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _flightRepository.GetByIdAsync(id.Value);

            if (flight == null)
            {
                return NotFound();
            }

            try
            {
                await _flightRepository.DeleteAsync(flight);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}