using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Flights.Web.Helpers;
using FlightTicketsSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Controllers
{
    //[Authorize(Roles = "SuperAdmin")]
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;
        private readonly SignInManager<User> _signInManager;


        public AdminController(
            IUserHelper userHelper,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            DataContext context,
            SignInManager<User> signInManager)
        {
            _userHelper = userHelper;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }


        public IActionResult IndexClients()
        {
            return View();
        }


        public IActionResult IndexEmployees()
        {
            return View();
        }


        public IActionResult IndexRoles()
        {
            return View();
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userHelper.CheckRoleAsync(model.Role);
                    ModelState.AddModelError(string.Empty, "Role created with success");
                    return View(model);
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There's a role with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

                return View(model);
            }

            this.ModelState.AddModelError(string.Empty, "Role already exists");

            return View(model);
        }

        public async Task<ActionResult> ListUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRoleViewModel>();

            foreach (User user in users)
            {
                var thisViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    Name = user.FullName,
                    UserName = user.Email,
                    Roles = await GetUserRoles(user)
                };
                userRolesViewModel.Add(thisViewModel);
            }

            return View(userRolesViewModel);
        }

        private async Task<List<string>> GetUserRoles(User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public async Task<IActionResult> UserDetails(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }


            var model = new UserRoleViewModel
            {
                Name = user.FullName,
                UserName = user.UserName,
                Address = user.FullAdress,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                UserId = user.Id,
                Role = user.Role
            };

            return View(model);
        }
    }
}
