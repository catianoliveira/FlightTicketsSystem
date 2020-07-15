using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Flights.Web.Helpers;

namespace Flights.Web.Controllers
{
    public class AirportsController : Controller
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IUserHelper _userHelper;

        public AirportsController(IAirportRepository airportRepository, IUserHelper userHelper)
        {
            _airportRepository = airportRepository;
            _userHelper = userHelper;
        }

        // GET: Airports
        public IActionResult Index()
        {
            return View(_airportRepository.GetAll());
        }

        // GET: Airports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _airportRepository.GetByIdAsync(id.Value);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // GET: Airports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Airports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Airport airport)
        {
            if (ModelState.IsValid)
            {
                //TODO airplane.User = await _userHelper.GetUserByEmailAsync();
                await _airportRepository.CreateAsync(airport);
                return RedirectToAction(nameof(Index));
            }
            return View(airport);
        }

        // GET: Airports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _airportRepository.GetByIdAsync(id.Value);

            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // POST: Airports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Airport airport)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //TODO airplane.User = await _userHelper.GetUserByEmailAsync();
                    await _airportRepository.UpdateAsync(airport);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _airportRepository.ExistAsync(airport.Id))
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
            return View(airport);
        }

        // GET: Airports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _airportRepository.GetByIdAsync(id.Value);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // POST: Airports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airport = await _airportRepository.GetByIdAsync(id);
            await _airportRepository.DeleteAsync(airport);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AirportExists(Airport airport)
        {
            return await _airportRepository.ExistAsync(airport.Id);
        }
    }
}
