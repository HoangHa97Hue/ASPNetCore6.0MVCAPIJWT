using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Entities;
using Service.Entities.Identity;
using Service.OtherClass;

namespace Service.Contexts
{
    /// <summary>
    /// The main Db context of the application, contains all of the DbSets
    /// </summary>
    //public class MyDbContext : IdentityDbContext<IdentityUser>
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MyDbContext()
        {

        }
        //config to access current user
        public MyDbContext(DbContextOptions<MyDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProcess>().HasKey("OrderProcessID");
            modelBuilder.Entity<TableOrder>().HasNoKey();
            modelBuilder.Entity<RegistrationRequest>().HasNoKey();
            modelBuilder.Entity<AuthRequest>().HasNoKey();
            //modelBuilder.Entity<RegisterViewModel>().HasNoKey();
            modelBuilder.Entity<Order>().HasMany(x => x.OrderDetails).WithOne(x => x.Order).IsRequired();
            modelBuilder.Entity<Meal>().HasMany(x => x.OrderDetails).WithOne(x => x.Meal).IsRequired();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);//cau hinh cho DBContext den DB nao
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Default");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        //public DbSet<CartItem> OrderCartItems { get; set; }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<MealCategory> MealCategorys { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProcess> OrderProcesses { get; set; }

        //public DbSet<Photo> Photos { get; set; }
        public DbSet<TableBO> Tables { get; set; }

        public DbSet<TableOrder> TableOrders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        //Them vao de tao Create theo modle thang razor cho khoe 

        public DbSet<RegistrationRequest> RegistrationRequest { get; set; }

        public DbSet<AuthRequest> AuthRequest { get; set; }

        //public DbSet<RegisterViewModel> RegisterViewModel { get; set; }



    }
}
