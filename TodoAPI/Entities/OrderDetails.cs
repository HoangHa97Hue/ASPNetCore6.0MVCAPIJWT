using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAPI.Entities
{
    public class OrderDetails
    {
        [Key]
        public string OrderDetailID { get; set; } = String.Empty;

        [ForeignKey("Order")]
        public string OrderID { get; set; } = String.Empty;
        [ForeignKey("Meal")]
        public string MealID { get; set; } = String.Empty;

        public  Order Order { get; set; }
        public  Meal Meal { get; set; }

        public int MealQuantity { get; set; }

        public decimal MealPrice { get; set; }



    }
}
