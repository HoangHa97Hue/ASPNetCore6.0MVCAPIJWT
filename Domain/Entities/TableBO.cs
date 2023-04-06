using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
{
    public class TableBO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TableID { get; set; }

        public string TableNumber { get; set;}

        public string TablePosition { get; set;}

        public int NumberOfPeople { get; set; }

        //public List<Photo> Photos { get; set; }
        //[FromForm]
        //[NotMapped]
        //public IFormFileCollection Files { get; set; }

        //public string StatusOrder { get; set; }

        //public ICollection<Meal> MealsOrder  { get; set; }

        //public Order Order { get; set; }//ban thuoc order, order thuoc user

        //public IdentityUser user { get; set; } // them User de thang Admin lo can xem thong tin thang User, de co yeu cau


    }
}
