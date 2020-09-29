using Flights.Web.Data.Entities;
using FlightTicketsSystem.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Flights.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Airplane> Airplanes { get; set; }

        public DbSet<Airport> Airports { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Indicative> Indicatives { get; set; }

        public DbSet<Ticket> Tickets { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                .Property(p => p.BusinessPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Flight>()
                .Property(p => p.EconomyPrice)
                .HasColumnType("decimal(18,2)");

           

            modelBuilder.Entity<Airport>()
                .HasIndex(a => a.IATA)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(a => a.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(a => a.UserName)
                .IsUnique();


            modelBuilder.Ignore<SelectListItem>();
            modelBuilder.Ignore<SelectListGroup>();


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
