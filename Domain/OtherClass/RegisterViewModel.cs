using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Service.OtherClass
{
    public class RegisterViewModel
    {
        public RegistrationRequest RegistrationRequest { get; set; }

        public List<SelectListItem> RoleListToSelect { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "Role is required")]
        public string RoleSelected { get; set; }
    }
}
