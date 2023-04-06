using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Entities;
using Service.Entities.Identity;

namespace Web.ViewModel
{
    public class OrderViewModel
    {
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
        public List<SelectListItem> OrderProcessStatus { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> UserSelectOptions { get; set; } = new List<SelectListItem>();

        public string OrderProcessSearchSelected { get; set; }

        public string UserIDSelected { get; set; }

        //public IdentityUser identityUser;
        public ApplicationUser identityUser;
    }
}
