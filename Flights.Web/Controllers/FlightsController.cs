using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Flights.Web.Data.Repositories;

namespace Flights.Web.Controllers
{
    public class FlightsController : Controller
    {
        private readonly DataContext _context;
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IFlightRepository _flightRepository;

        public FlightsController(
            DataContext context,
            IAirplaneRepository airplaneRepository,
            IAirportRepository airportRepository,
            IFlightRepository flightRepository)
        {
            _context = context;
            _airplaneRepository = airplaneRepository;
            _airportRepository = airportRepository;
            _flightRepository = flightRepository;
        }

        // GET: Flights
        public IActionResult Index()
        {
            return View(_flightRepository.GetAll());
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
                //Airplanes = _airplaneRepository.GetComboAirplanes()
                //ArrivalAirports = _airportRepository.GetComboAirports()
                //DepartureAirports = _airportRepository.GetComboAirports()
            };

            return this.View(model);
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _flightRepository.CreateAsync(flight);
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AirplaneId")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Id))
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
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.Id == id);
        }
    }
}
