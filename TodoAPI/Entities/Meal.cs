using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Entities.Identity;
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
    public class Meal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? MealID { get; set; }

        public string MealName { get; set;}

        public string MealDescription { get; set;}

        [Display(Name = "The remaining amount")]
        public int Quantity { get; set; } //session khi luu tam quantity vao meal, roi save xuong Order, vi session luu meal va quantity roi, gio phai save xuong

        [Display(Name = "Giá sản phẩm")]
        [Range(0, int.MaxValue, ErrorMessage = "Phải nhập giá trị từ {1}")]
        public decimal Price { get; set; }

        public MealCategory? MealCategory { get; set; }

        public ICollection<OrderDetails>? OrderDetails { get; set; } = new List<OrderDetails>();

        //public IdentityUser? User { get; set; } //user Tao
        public ApplicationUser? User { get; set; } 

        public string? MealImage { get; set; }
    }
}
