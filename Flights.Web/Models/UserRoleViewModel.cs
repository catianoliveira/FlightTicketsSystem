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

        public RoleViewModel RoleId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        //TODO public bool IsSelected { get; set; }


        public IEnumerable<string> Roles { get; set; }
    }
}
