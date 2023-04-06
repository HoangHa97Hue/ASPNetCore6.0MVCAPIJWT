using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoAPI.Entities;

namespace TodoAPI.Contexts
{
    /// <summary>
    /// The main Db context of the application, contains all of the DbSets
    /// </summary>
    //public class MyDbContext : IdentityDbContext<IdentityUser>
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProcess>().HasKey("OrderProcessID");
            //modelBuilder.Entity<RegisterViewModel>().HasNoKey();
            modelBuilder.Entity<Order>().HasMany(x => x.OrderDetails).WithOne(x => x.Order).IsRequired();
            modelBuilder.Entity<Meal>().HasMany(x => x.OrderDetails).WithOne(x => x.Meal).IsRequired();
            base.OnModelCreating(modelBuilder);
        }


        //public DbSet<CartItem> OrderCartItems { get; set; }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<MealCategory> MealCategorys { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProcess> OrderProcesses { get; set; }


        public DbSet<OrderDetails> OrderDetails { get; set; }




    }
}
