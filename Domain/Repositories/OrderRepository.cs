//using Business.GenericRepository;
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
//    public class OrderRepository : GenericRepository<Order>,IOrderRepository

//    {
//        //private readonly MyDbContext _dbContext;

//        //public OrderRepository(MyDbContext dbContext)
//        //{
//        //    _dbContext = dbContext;
//        //}

//        public OrderRepository(MyDbContext context) : base(context) { }

//        //public Order createOrder(Order order)
//        //{
//        //    try
//        //    {
//        //        _dbContext.Orders.Add(order);

//        //        var result = _dbContext.SaveChanges();

//        //        if (result > 0)
//        //        {
//        //            return order;
//        //        }
//        //        return new Order();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine(ex.ToString());
//        //        return new Order();
//        //    }
//        //}

//        //public void DeleteOrder(string id)
//        //{
//        //    var order = GetOrder(id);
//        //    try
//        //    {
//        //        _dbContext.Orders.Remove(order);

//        //        _dbContext.SaveChanges();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine(ex.ToString());
//        //    }
//        //}

//        //public void EditOrder(Order order)
//        //{
//        //    _dbContext.Orders.Update(order);

//        //    var result = _dbContext.SaveChanges();
//        //}

//        //public IEnumerable<Order> GetAll()
//        //{
//        //    try
//        //    {
//        //        return _dbContext.Orders.Include(d => d.OrderProcess).Include(d => d.User).ToList();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine(ex.ToString());
//        //        return new List<Order>();
//        //    }

//        //}

//        public IEnumerable<Order> GetByOrderProcess(string orderProcess)
//        {
//            try
//            {
//                return context.Orders.Include(d => d.OrderProcess)
//                    .Where(d => d.OrderProcess.StatusOrder.Equals(orderProcess)).ToList();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new List<Order>();
//            }
//        }

//        public IEnumerable<Order> GetByUser(IdentityUser user)
//        {
//            try
//            {
//                return context.Orders.Include(d => d.OrderProcess).Include(d => d.User)
//                .Where(i => i.User.Id == user.Id).ToList();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new List<Order>();
//            }
//        }

//        public IEnumerable<Order> GetByUserAndOrderProcess(IdentityUser user, string orderProcess)
//        {
//            try
//            {
//                return context.Orders.Include(d => d.OrderProcess).Include(d => d.User)
//                    .Where(d => d.OrderProcess.StatusOrder.Equals(orderProcess)).Where(i => i.User.Id.Equals(user.Id)).ToList(); 
//            }
//            catch(Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new List<Order>();
//            }
//        }



//        //public Order GetOrder(string id)
//        //{
//        //    try
//        //    {
//        //        return _dbContext.Orders.Single(i => i.OrderID == id);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine(ex.ToString());
//        //        return new Order();
//        //    }
//        //}

//    }
//}
