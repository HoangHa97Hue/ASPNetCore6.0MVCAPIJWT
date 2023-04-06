using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Service.Database;
using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.OtherClass
{
    public class MealViewModel
    {
        //public IEnumerable<MealCategory> MealCategories { get; set; } = new List<MealCategory>();

        public List<SelectListItem> ListSelectMealCategory { get; set; } = new List<SelectListItem>();

        public string MealCategoryIDSelected { get; set; }

        public Meal Meal { get; set; }

        public IFormFile MealImage { get; set; }

        //public string? SearchString { get; set; }

        //public string? UserSelectedName { get; set; }

        /* public TaskViewModel()
         {
             TypeSearchList = new List<SelectListItem>();
             ActiveTaskBOS = new List<TaskOfAccount>();
         }*/
    }
}
