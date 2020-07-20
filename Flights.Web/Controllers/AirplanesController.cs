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
using Microsoft.AspNetCore.Authorization;

namespace Flights.Web.Controllers
{
    //[Authorize] //Todo meter authorize em tudo
    public class AirplanesController : Controller
    {
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IUserHelper _userHelper;

        public AirplanesController(IAirplaneRepository airplaneRepository, IUserHelper userHelper)
        {
            _airplaneRepository = airplaneRepository;
            _userHelper = userHelper;
        }

        // GET: Airplanes
        public IActionResult Index()
        {
            return View(_airplaneRepository.GetAll());
        }

        // GET: Airplanes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airplane = await _airplaneRepository.GetByIdAsync(id.Value);
            if (airplane == null)
            {
                return NotFound();
            }

            return View(airplane);
        }

        // GET: Airplanes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Airplanes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Airplane airplane)
        {
            if (ModelState.IsValid)
            {
                //TODO airplane.User = await _userHelper.GetUserByEmailAsync();

                await _airplaneRepository.CreateAsync(airplane);
                return RedirectToAction(nameof(Index));
            }
            return View(airplane);
        }

        // GET: Airplanes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airplane = await _airplaneRepository.GetByIdAsync(id.Value);
            
            if (airplane == null)
            {
                return NotFound();
            }

            return View(airplane);
        }

        // POST: Airplanes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Airplane airplane)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //TODO airplane.User = await _userHelper.GetUserByEmailAsync();
                    await _airplaneRepository.UpdateAsync(airplane);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _airplaneRepository.ExistAsync(airplane.Id))
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
            return View(airplane);
        }

        // GET: Airplanes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airplane = await _airplaneRepository.GetByIdAsync(id.Value);
            if (airplane == null)
            {
                return NotFound();
            }

            return View(airplane);
        }

        // POST: Airplanes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airplane = await _airplaneRepository.GetByIdAsync(id);
            await _airplaneRepository.DeleteAsync(airplane);
            return RedirectToAction(nameof(Index));
        }
    }
}
