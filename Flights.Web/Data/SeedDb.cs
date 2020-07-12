using Flights.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //vê se a bd já foi criada

            if (!_context.Airplanes.Any()) //tabela produtos está vazia
            {
                this.AddAirplane("airbus123");
                await _context.SaveChangesAsync();
            }
        }

        private void AddAirplane(string model)
        {
            _context.Airplanes.Add(new Airplane
            {
                Model = model,
                EconomicSeats = _random.Next(20),
                ExecutiveSeats = _random.Next(15, 50),
                Quantity = _random.Next(10)
            });
        }
    }
}