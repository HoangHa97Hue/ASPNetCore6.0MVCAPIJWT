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
//    public class TableOderService : IMealService
//    {
//        private readonly MyDbContext _dbContext;

//        public TableOderService(MyDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public TableOrder createMeal(TableOrder meal)
//        {
//            try
//            {
//                _dbContext.TableOrders.Add(meal);

//                var result = _dbContext.SaveChanges();

//                if (result > 0)
//                {
//                    return meal;
//                }
//                return new TableOrder();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new TableOrder();
//            }
//        }

//        public void Delete(TableOrder meal)
//        {
//            try
//            {
//                _dbContext.TableOrders.Remove(meal);

//                _dbContext.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }

//        public void DeleteTableOrder(string id)
//        {
//            var meal = GetMeal(id);
//            try
//            {
//                _dbContext.TableOrders.Remove(meal);

//                _dbContext.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }
        

//        public void EditTableOrder(TableOrder tableOrder)

//        {
//            _dbContext.TableOrders.Update(meal);

//            var result = _dbContext.SaveChanges();
//        }

//        public IEnumerable<TableOrder> GetAll()
//        {
//            try
//            {
//                return _dbContext.TableOrders.ToList();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new List<TableOrder>();
//            }

//        }

//        public IEnumerable<TableOrder> GetByUser(IdentityUser user)
//        {
//            try
//            {
//                return _dbContext.TableOrders
//                .Where(i => i.User.Id == user.Id).ToList();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new List<TableOrder>();
//            }
//        }

//        public TableOrder GetTableOrder(string id)
//        {
//            try
//            {
//                return _dbContext.TableOrders.Single(i => i.TableID == id);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new Meal();
//            }
//        }

//    }
//}
