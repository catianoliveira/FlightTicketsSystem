using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightTicketsSystem.Web.Models
{
    public class UserRoleViewModel
    {

       
        public string UserId { get; set; }


        public string Name { get; set; }




        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }



        public IEnumerable<string> Roles { get; set; }


        public DateTime DateOfBirth { get; set; }


        public string Address { get; set; }


        public string PhoneNumber { get; set; }
    }
}
