using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Flights.Web.Data.Repositories;
using Flights.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Controllers
{
    public class AirportsController : Controller
    {
        private readonly IAirportRepository _airportRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IUserHelper _userHelper;
        private readonly DataContext _dataContext;

        public AirportsController(
            IAirportRepository airportRepository,
            ICountryRepository countryRepository,
            IUserHelper userHelper,
            DataContext dataContext)
        {
            _airportRepository = airportRepository;
            _countryRepository = countryRepository;
            _userHelper = userHelper;
            _dataContext = dataContext;
        }

        // GET: Airports
        public IActionResult Index()
        {
            var model = _airportRepository.GetAll().OrderBy(p => p.Country).ToList();

            return View(model);
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


            var model = new Airport
            {
                IATA = airport.IATA,
                City = airport.City,
                Name = airport.Name,
                Country = airport.Country,
            };

            return View(model);
        }

        // GET: Airports/Create
        public IActionResult Create()
        {
            return this.View();
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



        // POST: Airports/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //TODO return new NotFoundViewResult("NotFound");
            }

            var airport = await _airportRepository.GetByIdAsync(id.Value);
            await _airportRepository.DeleteAsync(airport);

            return RedirectToAction(nameof(Index));
        }

        // GET: Airplanes/Edit/5
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

            var model = new Airport
            {
                IATA = airport.IATA,
                City = airport.City,
                Name = airport.Name,
                Country = airport.Country
            };

            return View(model);
        }

        // POST: Airplanes/Edit/5
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
    }
}

