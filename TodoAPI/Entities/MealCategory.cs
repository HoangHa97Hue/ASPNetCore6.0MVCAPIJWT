using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAPI.Entities
{
    public class MealCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? MealCategoryID { get; set; }

        public string MealCategoryName { get; set;}


        public ICollection<Meal>? Meals { get; set; }

        public string? MealCategoryImage { get; set; }

    }
}
