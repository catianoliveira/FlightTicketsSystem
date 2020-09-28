using Flights.Web.Data;
using FlightTicketsSystem.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlightTicketsSystem.Web.Data.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public string GetRoleId()
        {
            var roleId = _context.Roles
                .AsNoTracking()
                .Include(r => r.Id)
                .FirstOrDefault(r => r.Name == "Client");

            return roleId.ToString();
        }
    }
}
