using Flights.Web.Data;
using FlightTicketsSystem.Web.Data.Entities;
using System.Linq;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        string GetRoleId();
    }
}
