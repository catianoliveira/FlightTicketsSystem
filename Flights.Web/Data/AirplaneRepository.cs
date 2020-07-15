using Flights.Web.Data.Entities;
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
    }
}
