using Flights.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Airplane> Airplanes { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //instância local serve para todos conseguirem abrir o mesmo projeto sme ter que alterar nome do servidor


        }

        public DbSet<Flights.Web.Data.Entities.Airport> Airport { get; set; }
    }
}
