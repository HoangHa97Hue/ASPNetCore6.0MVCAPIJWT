using Microsoft.AspNetCore.Identity;
using Service.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TodoAPI.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(450)]
        public string OrderID { get; set; }

        public string Note { get; set; }

        public OrderProcess OrderProcess { get; set; }

        public decimal SumPrice { get; set; }


        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; }

        //[Display(Name = "Ngày cập nhật")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateUpdated { get; set; }

        //public IdentityUser User { get; set; }
        public ApplicationUser User { get; set; }

        //public Table Table { get; set; }

        //public ICollection<Order>? Meals { get; set; } // tao ra bang trung gian chua thong tin order va mon an 
        public ICollection<OrderDetails>? OrderDetails { get; set; } = new List<OrderDetails>();


    }
}
