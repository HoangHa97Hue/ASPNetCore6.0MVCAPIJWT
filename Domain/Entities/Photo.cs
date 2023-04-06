//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Service.Entities
//{
//    public class Photo
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.None)]
//        public string ID { get; set; }
//        public byte[] Bytes { get; set; }
//        public string Description { get; set; }
//        public string FileExtension { get; set; }
//        public decimal Size { get; set; }
//        public string? MealID { get; set; }
//        [ForeignKey("MealID")]
//        public Order Meal { get; set; }

//        //public string? MealCategoryID { get; set; }
//        //[ForeignKey("MealCategoryID")]
//        //public MealCategory MealCategory { get; set; }

//        //public string? TableID { get; set; }
//        //[ForeignKey("TableID")]
//        //public Table Table { get; set; }

//    }
//}
