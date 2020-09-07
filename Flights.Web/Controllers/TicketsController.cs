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

namespace FlightTicketsSystem.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly DataContext _context;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IAirportRepository _airportRepository;

        public TicketsController(
            DataContext context,
            IDocumentTypeRepository documentTypeRepository,
            ICountryRepository countryRepository,
            IFlightRepository flightRepository,
            ITicketRepository ticketRepository,
            IAirportRepository airportRepository)
        {
            _context = context;
            _documentTypeRepository = documentTypeRepository;
            _countryRepository = countryRepository;
            _flightRepository = flightRepository;
            _ticketRepository = ticketRepository;
            _airportRepository = airportRepository;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Tickets
                .Include(t => t.DocumentType)
                .Include(t => t.ArrivalAirport)
                .Include(t => t.DepartureAirport);
            return View(await dataContext.ToListAsync());
        }



        public IActionResult ChooseFlight()
        {
            var model = _context.Flights
                .Include(a => a.Airplane)
                .Include(a => a.DepartureAirport)
                .Include(a => a.ArrivalAirport)
                .OrderBy(p => p.Date);

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
                .Include(t => t.DocumentType)
                .Include(t => t.ArrivalAirport)
                .Include(t => t.DepartureAirport)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            var model = new Ticket
            {
                DocumentTypes = _documentTypeRepository.GetComboDocumentTypes(),
                ArrivalAirports = _flightRepository.GetComboArrivals(0),
                DepartureAirports = _flightRepository.GetComboDepartures()
            };

            return this.View(model);
        }


        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                await _ticketRepository.CreateAsync(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

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
                .Include(t => t.DocumentType)
                .Include(t => t.DepartureAirport)
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
            var airport = await _flightRepository.GetDeparturesWithArrivalsAsync(departureAirportId);
            return this.Json(airport.ArrivalsCollection.OrderBy(c => c.Country));
        }
    }
}
