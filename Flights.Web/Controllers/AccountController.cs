using Flights.Web.Data;
using Flights.Web.Data.Entities;
using Flights.Web.Data.Repositories;
using Flights.Web.Helpers;
using Flights.Web.Models;
using FlightTicketsSystem.Web.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly ICountryRepository _countryRepository;
        private readonly IMailHelper _mailHelper;
        private readonly IIndicativeRepository _indicativeRepository;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;
        private readonly SignInManager<User> _signInManager;

        public AccountController(
            IUserHelper userHelper,
            IConfiguration configuration,
            ICountryRepository countryRepository,
            IMailHelper mailHelper,
            IIndicativeRepository indicativeRepository,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            DataContext context,
            SignInManager<User> signInManager)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _countryRepository = countryRepository;
            _mailHelper = mailHelper;
            _indicativeRepository = indicativeRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(new LoginViewModel());
        }


        /// <summary>
        /// Checks if user and password are correct. 
        /// Locks out user for 10min if password is incorret for 5 times
        /// Send email to reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);

                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnURL"))
                    {
                        return this.Redirect(this.Request.Query["ReturnURL"].First());
                    }

                    return this.RedirectToAction("Index", "Home");
                }

                if (result.IsLockedOut)
                {
                    var user = await _userHelper.GetUserByEmailAsync(model.Username);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                        return this.View(model);
                    }

                    ModelState.AddModelError(string.Empty, "Your account is locked out, to reset your password click on the link sent to your email");

                    var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                    var link = this.Url.Action(
                        "ResetPassword",
                        "Account",
                        new { token = myToken }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendMail(model.Username, "HighFly Password Reset", $"<h1>HighFly Password Reset</h1>" +
                    $"To reset the password click in this link:</br></br>" +
                    $"<a href = \"{link}\">Reset Password</a>");
                }


                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect username or password");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            var model = new RegisterNewUserViewModel
            {
                Countries = _countryRepository.GetComboCountries(),
                Indicatives = _indicativeRepository.GetComboIndicatives(),
                RoleChoices = _userHelper.GetComboRoles()
            };
            return View(model);
        }


        /// <summary>
        /// checks if user is logged in. if yes, then the admin or super admin
        /// gets to choose the new user's role and the password is "HighFly123*".
        /// otherwise the new user's role is "client" and he gets to choose a new 
        /// password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.EmailAddress);
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.EmailAddress,
                        UserName = model.EmailAddress,
                        Address = model.Address,
                        IndicativeId = model.IndicativeId,
                        PhoneNumber = model.PhoneNumber,
                        City = model.City,
                        CountryId = model.CountryId,
                        RoleId = model.RoleID
                    };
                }

                if (User.Identity.IsAuthenticated)
                {
                    try
                    {
                        var result = await _userHelper.AddUserAsync(user, "HighFly123*");

                        if (result != IdentityResult.Success)
                        {
                            ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                            return View(model);
                        }

                        var roleName = await _roleManager.FindByIdAsync(user.RoleId);
                        var register = await _userManager.FindByIdAsync(user.Id);
                        await _userManager.AddToRoleAsync(register, roleName.ToString());
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }

                else if (!User.Identity.IsAuthenticated)
                {
                    try
                    {
                        var result = await _userHelper.AddUserAsync(user, model.Password);

                        if (result != IdentityResult.Success)
                        {
                            this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                            return this.View(model);
                        }

                        ModelState.AddModelError(string.Empty, "Click the link sent to your email to confirm your account!");

                        await _userHelper.AddUserToRoleAsync(user, "Client");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
                var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                var tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                try
                {
                    _mailHelper.SendMail(model.EmailAddress, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                    $"Complete your registration by " +
                    $"clicking link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "This user already exists.");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }

            return this.BadRequest();
        }

        public IActionResult RecoverPassword()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return View(model);
                }

                ModelState.AddModelError(string.Empty, "Click on the link send to your email to recover your password");


                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendMail(model.Email, "HighFly Password Reset", $"<h1>HighFly Password Reset</h1>" +
                $"To reset the password click in this link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");

                return View();

            }

            return View(model);
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Password reset successful.");
                    return View();
                }

                ModelState.AddModelError(string.Empty, "Error while reseting the password. Try again later.");
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "User not found.");
            return View(model);
        }


        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            var model = new ChangeUserViewModel
            {
                Countries = _countryRepository.GetComboCountries(),
                Indicatives = _indicativeRepository.GetComboIndicatives()
            };


            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.PhoneNumber = user.PhoneNumber;
                model.Address = user.Address;
                model.City = user.City;
                model.CountryId = user.CountryId;
                model.IndicativeId = user.IndicativeId;
            }

            return View(model);
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotAuthorized();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return this.NotAuthorized();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotAuthorized();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Address = model.Address;
                    user.CountryId = model.CountryId;
                    user.City = model.City;
                    user.PhoneNumber = model.PhoneNumber;
                    user.IndicativeId = model.IndicativeId;


                    var response = await _userHelper.UpdateUserAsync(user);

                    if (response.Succeeded)
                    {
                        this.ViewBag.UserMessage = "User updated";
                    }

                    else
                    {
                        this.ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }

                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found");
                }
            }

            return this.View(model);
        }
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return View(model);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}