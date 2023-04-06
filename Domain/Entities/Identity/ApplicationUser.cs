using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities.Identity
{
    // if create table in DB for User ==> we can use Application user instead of Identity User
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            
        }

        public ApplicationUser(string userName) : base(userName) { }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
