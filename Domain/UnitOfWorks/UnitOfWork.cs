using Business.GenericRepository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service.Contexts;
using Service.Entities;
using Service.Repositories;

namespace Service.UnitOfWorks
{

    /// <summary>
    /// The main class to interact with the repositories, contains all repositories of the application.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private MyDbContext _context { get; }

        public IGenericRepository<MealCategory> mealCategoryRepository;
        public IGenericRepository<Meal> mealRepository;
        public IGenericRepository<OrderProcess> orderProcessRepository;
        public IGenericRepository<Order> orderRepository;
        public IGenericRepository<TableOrder> tableOrderRepository;
        public IGenericRepository<Table> tableRepository;

        public UnitOfWork(MyDbContext context, IGenericRepository<MealCategory> mealCategoryRepository, IGenericRepository<Meal> mealRepository, IGenericRepository<OrderProcess> orderProcessRepository, IGenericRepository<Order> orderRepository, IGenericRepository<TableOrder> tableOrderRepository, IGenericRepository<Table> tableRepository)
        {
            _context = context;
            this.mealCategoryRepository = mealCategoryRepository;
            this.mealRepository = mealRepository;
            this.orderProcessRepository = orderProcessRepository;
            this.orderRepository = orderRepository;
            this.tableOrderRepository = tableOrderRepository;
            this.tableRepository = tableRepository;
        }

        public IGenericRepository<MealCategory> MealCategoryRepository 
        {
            get
            {
                if(this.mealCategoryRepository == null)
                {
                    this.mealCategoryRepository = new GenericRepository<MealCategory>(_context);
                }
                return mealCategoryRepository;
            }
        }

        public IGenericRepository<Meal> MealRepository
        {
            get
            {
                if (this.mealRepository == null)
                {
                    this.mealRepository = new GenericRepository<Meal>(_context);
                }
                return mealRepository;
            }
        }


        public IGenericRepository<OrderProcess> OrderProcessRepository
        {
            get
            {
                if (this.orderProcessRepository == null)
                {
                    this.orderProcessRepository = new GenericRepository<OrderProcess>(_context);
                }
                return orderProcessRepository;
            }
        }


        public IGenericRepository<Order> OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new GenericRepository<Order>(_context);
                }
                return orderRepository;
            }
        }

        public IGenericRepository<TableOrder> TableOrderRepository
        {
            get
            {
                if (this.tableOrderRepository == null)
                {
                    this.tableOrderRepository = new GenericRepository<TableOrder>(_context);
                }
                return tableOrderRepository;
            }
        }

        public IGenericRepository<Table> TableRepository
        {
            get
            {
                if (this.tableRepository == null)
                {
                    this.tableRepository = new GenericRepository<Table>(_context);
                }
                return tableRepository;
            }
        }

        public virtual int Complete()
        {
            return _context.SaveChanges();
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}
