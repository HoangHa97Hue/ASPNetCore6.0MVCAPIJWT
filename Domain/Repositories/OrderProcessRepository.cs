//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Service.Contexts;
//using Service.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace Service.Repositories
//{
//    public class OrderProcessRepository : IOrderProcessRepository
//    {
//        private readonly MyDbContext _dbContext;

//        public OrderProcessRepository(MyDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public OrderProcess createOrderProcess(OrderProcess order)
//        {
//            try
//            {
//                _dbContext.OrderProcesses.Add(order);

//                var result = _dbContext.SaveChanges();

//                if (result > 0)
//                {
//                    return order;
//                }
//                return new OrderProcess();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new OrderProcess();
//            }
//        }

//        public void DeleteOrderProcess(string id)
//        {
//            var order = GetOrderProcess(id);
//            try
//            {
//                _dbContext.OrderProcesses.Remove(order);

//                _dbContext.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }


//        public void EditOrderProcess(OrderProcess order)
//        {
//            _dbContext.OrderProcesses.Update(order);

//            var result = _dbContext.SaveChanges();
//        }

//        public IEnumerable<OrderProcess> GetAll()
//        {
//            try
//            {
//                return _dbContext.OrderProcesses.ToList();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new List<OrderProcess>();
//            }

//        }

//        public OrderProcess GetOrderProcess(string id)
//        {
//            try
//            {
//                return _dbContext.OrderProcesses.Single(i => i.OrderProcessID == id);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new OrderProcess();
//            }
//        }
//    }
//}
