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
		

		IEnumerable<SelectListItem> GetComboCountries();


	}
}
