using Business.GenericRepository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        //public IGenericRepository<MealCategory> MealCategoryRepository { get; }
        //public IGenericRepository<Meal> MealRepository { get; }
        //public IGenericRepository<OrderProcess> OrderProcessRepository { get; }
        //public IGenericRepository<Order> OrderRepository { get;}
        //public IGenericRepository<TableOrder> TableOrderRepository { get; }
        //public IGenericRepository<Table> TableRepository { get; }

        public int Complete();

        public void Dispose();        

    }
}
