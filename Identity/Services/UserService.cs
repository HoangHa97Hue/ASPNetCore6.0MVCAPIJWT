using Microsoft.AspNetCore.Identity;
using Service.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<User> GetUser(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);
            return new User
            {
                Email = user.Email,
                ID = user.Id,
                Firstname = user.FirstName,
                Lastname = user.LastName
            };
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("Member");
            return users.Select(q => new User 
            {
                Email = q.Email,
                ID = q.Id,
                Firstname = q.FirstName,
                Lastname = q.LastName
            }).ToList();
        }

    }
}
