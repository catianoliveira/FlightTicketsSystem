using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public class AirplaneRepository : GenericRepository<Airplane>, IAirplaneRepository
    {
        public AirplaneRepository(DataContext context) : base(context)
        {
            //é necessario o interface porque no startup temos que 
            //implementar o interface e o T
        }

        public IQueryable GetAllWithUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SelectListItem> GetComboAirplanes()
        {
            throw new NotImplementedException();
        }
    }
}
