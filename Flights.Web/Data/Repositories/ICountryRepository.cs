using Flights.Web.Data.Entities;
using Flights.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data.Repositories
{
	public interface ICountryRepository : IGenericRepository<Country>
	{
		//IQueryable GetCountriesWithCities();
		IQueryable GetCountries();



		//Task<City> GetCityAsync(int id);


		//Task AddCityAsync(CityViewModel model);


		//Task<int> UpdateCityAsync(City city);


		//Task<int> DeleteCityAsync(City city);


		IEnumerable<SelectListItem> GetComboCountries();


		//IEnumerable<SelectListItem> GetComboCities(int countryId);


		//Task<Country> GetCountryAsync(City city);
	}
}
