﻿using Flights.Web.Data.Entities;
using Flights.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Flights.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly Random _random;

        //User manager faz a gestão dos users
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        //TODO ver user manager

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();


            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");


            var user = await _userHelper.GetUserByEmailAsync("catia.nunes.oliveira@formandos.cinel.pt");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Cátia",
                    LastName = "Oliveira",
                    Email = "catia.nunes.oliveira@formandos.cinel.pt",
                    UserName = "AdminCatia",
                    PhoneNumber = "123456",
                    Address = "Rua da Luz",
                    //CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    //City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await _userHelper.AddUserAsync(user, "123456");



                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder.");
                }


                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);


                var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
                if (!isInRole)
                {
                    await _userHelper.AddUserToRoleAsync(user, "Admin");
                }


            }

        }

    }
}
