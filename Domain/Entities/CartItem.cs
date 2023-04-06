using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
{
    public class CartItem // chi luu tren session
    {
            public int quantity { set; get; }
            public Meal Meal { set; get; }
        //{
        //    [Key]
        //    public long CartID { get; set; }

        //    public string CartId { get; set; }

        //    //public int Quantity { get; set; }

        //    public System.DateTime DateCreated { get; set; }

        //    public Guid MealID { get; set; }

        //    public virtual Meal Meal { get; set; }
        //    //image

        //tuc la cai card nay la table chua tung card item co lien quan den Meal
        //record nao ma cung add them nhieu lan thi cho no quality +++ 
        //bang table chung se chua chung tat ca record cua cac user luon
        //link tham khao: https://learn.microsoft.com/en-us/aspnet/web-forms/overview/getting-started/getting-started-with-aspnet-45-web-forms/shopping-cart
    }
}
