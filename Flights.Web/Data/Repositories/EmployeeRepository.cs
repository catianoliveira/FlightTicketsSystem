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

        //public IQueryable GetClients()
        //{
        //    //vai buscar o id do role client

        //    var roleId = _context.Roles
        //        .Include(r => r.Id)
        //        .Where(r => r.Name == "Client");

        //    var list = _context.Users
        //        .Include(r => r.UserName)
        //        .Include(r => r.FullName)
        //        .Where(r => r.RoleId == roleId);
        //}
    }
}
