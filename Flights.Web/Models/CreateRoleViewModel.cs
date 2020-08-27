using System.ComponentModel.DataAnnotations;

namespace FlightTicketsSystem.Web.Models
{
    public class CreateRoleViewModel
    {
        public string Id { get; set; }


        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
