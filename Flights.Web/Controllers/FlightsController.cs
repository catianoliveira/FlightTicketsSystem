using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Flights.Web.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Flights.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "SuperAdmin")]
    //[Authorize(Roles = "Employee")]

    public class FlightsController : Controller
    {
        private readonly DataContext _context;
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IAirportRepository _airportRepository;


        public FlightsController(
            DataContext context,
            IAirplaneRepository airplaneRepository,
            IFlightRepository flightRepository,
            IAirportRepository airportRepository)
        {
            _context = context;
            _airplaneRepository = airplaneRepository;
            _flightRepository = flightRepository;
            _airportRepository = airportRepository;
        }

        // GET: Flights
        public IActionResult Index()
        {
            var model = _context.Flights
                .Include(a => a.Airplane)
                .Include(a => a.DepartureAirport)
                .Include(a => a.ArrivalAirport)
                .OrderBy(p => p.DateTime);

            return View(model.ToList());
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
                AirportsEnumerable = _airportRepository.GetComboAirports()
            };

            return this.View(model);
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flight flights)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Flight flight = new Flight
                    {
                        ArrivalAirportId = flights.ArrivalAirportId,
                        DepartureAirportId = flights.DepartureAirportId,
                        ArrivalsCollection = new List<Airport>(),
                        DeparturesCollection = new List<Airport>(),
                        AirplaneId = flights.AirplaneId,
                        BusinessPrice = flights.BusinessPrice,
                        EconomyPrice = flights.EconomyPrice,
                        DateTime = flights.DateTime,
                        ArrivalAirport = flights.ArrivalAirport,
                        DepartureAirport = flights.DepartureAirport
                    };

                    await _flightRepository.CreateAsync(flight);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(flights);
        }


        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            var model = new Flight
            {
                AirportsEnumerable = _airportRepository.GetComboAirports(),
                Airplanes = _airplaneRepository.GetComboAirplanes(),
                AirplaneId = flight.AirplaneId,
                DepartureAirportId = flight.DepartureAirportId,
                ArrivalAirportId = flight.ArrivalAirportId,
                BusinessPrice = flight.BusinessPrice,
                EconomyPrice = flight.EconomyPrice,
                DateTime = flight.DateTime
            };

            return View(model);
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
                    //TODO airplane.User = await _userHelper.GetUserByEmailAsync();
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
                        throw;
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