using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Flights.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(1)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


        public string ReturnUrl { get; set; }



        public IList<AuthenticationScheme> ExternalLogins { get; set; }

    }
}