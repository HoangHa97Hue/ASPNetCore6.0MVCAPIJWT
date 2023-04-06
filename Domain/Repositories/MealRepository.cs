using Business.GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Contexts;
using Service.Entities;
using Service.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.Repositories
{
    public class MealRepository : GenericRepository<Meal>, IMealRepository
    {
        public MealRepository(MyDbContext context) :base(context)
        {

        }



        public IEnumerable<Meal> GetByUser(ApplicationUser user)
        {
            try
            {
                return context.Meals
                .Where(i => i.User.Id == user.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<Meal>();
            }
        }
    }
}
