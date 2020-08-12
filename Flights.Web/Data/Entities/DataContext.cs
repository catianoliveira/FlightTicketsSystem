using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Airplane> Airplanes { get; set; }

        public DbSet<Airport> Airports { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //instância local serve para todos conseguirem abrir o mesmo projeto sme ter que alterar nome do servidor


        }

        //TODO quando tiver o ticket
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Ticket>()
            //    .Property(p => p.Price)
            //    .HasColumnType("decimal(18,2");

            modelBuilder.Ignore<SelectListItem>();
            modelBuilder.Ignore<SelectListGroup>();

            // habilitar a cascade delete rule
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
