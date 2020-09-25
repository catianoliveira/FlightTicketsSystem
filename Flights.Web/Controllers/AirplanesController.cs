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
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "SuperAdmin")]
    //[Authorize(Roles = "Employee")]

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

            try
            {
                await _airplaneRepository.DeleteAsync(airplane);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
