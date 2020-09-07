using Flights.Web.Data.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public class AirportRepository : GenericRepository<Entities.Airport>, IAirportRepository
    {
        private readonly DataContext _context;
        private readonly ICountryRepository _countryRepository;

        public AirportRepository(
            DataContext context,
            ICountryRepository countryRepository) : base(context)
        {
            _context = context;
            _countryRepository = countryRepository;
        }



        //public async Task CheckCity(City city)
        //{
        //    var globalAirports = new GlobalAirports();

        //    //if (_context.Cities.Any())
        //    //{
        //    //    globalAirports.GetByCity(city);

        //    //    if (true)
        //    //    {

        //    //    }
        //    //}

        //    //else
        //    //{

        //    //}
        //}

        public IQueryable GetAllWithUsers()
        {
            return _context.Airports;
        }

        public IEnumerable<SelectListItem> GetComboAirports()
        {
            var list = _context.Airports.OrderBy(p => p.Country).Select(p => new SelectListItem
            {
                Text = p.CompleteAirport,
                Value = p.Id.ToString()

            }).ToList();


            list.Insert(0, new SelectListItem
            {
                Text = "Select an airport",
                Value = "0"
            });

            return list;
        }

        
    }
}
