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
    public class MealCategoryViewModel
    {


        public MealCategory MealCategory { get; set; }

        public IFormFile MealCategoryImage { get; set; }

    }
}
