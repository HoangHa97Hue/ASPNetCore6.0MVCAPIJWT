using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAPI.Entities
{
    public class OrderProcess
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string OrderProcessID { get; set; }

        [ForeignKey("Order")]
        public string OrderID { get; set; } = String.Empty;
        public Order Order{get; set;}

        public decimal PriceOrder { get; set; }
        public string StatusOrder { get; set; }
    }
}
