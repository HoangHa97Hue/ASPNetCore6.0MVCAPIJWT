using Business.GenericRepository;
using Microsoft.AspNetCore.Identity;
using Service.Entities;
using Service.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public interface IMealRepository :IGenericRepository<Meal>
    {
        public IEnumerable<Meal> GetByUser(ApplicationUser user);
        //public IEnumerable<Meal> GetAll();
        //public Meal createMeal(Meal meal);

        //public Meal GetMeal(string id);

        //public void EditMeal(Meal meal);

        //public void DeleteMeal(string id);
    }
}
