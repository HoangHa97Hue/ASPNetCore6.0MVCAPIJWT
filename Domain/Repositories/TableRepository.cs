//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
//    public class TableRepository : ITableRepository
//    {
//        private readonly MyDbContext _dbContext;

//        public TableRepository(MyDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public TableBO createTable(TableBO table)
//        {
//            try
//            {
//                _dbContext.Tables.Add(table);

//                var result = _dbContext.SaveChanges();

//                if (result > 0)
//                {
//                    return table;
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new TableBO();
//            }
//        }

//        public void Delete(TableBO table)
//        {
//            try
//            {
//                _dbContext.Tables.Remove(table);

//                _dbContext.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }

//        public void DeleteTable(string id)
//        {
//            var table = GetTable(id);
//            try
//            {
//                _dbContext.Tables.Remove(table);

//                _dbContext.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }

//        public void EditTable(TableBO table)
//        {
//            _dbContext.Tables.Update(table);

//            var result = _dbContext.SaveChanges();
//        }

//        public IEnumerable<TableBO> GetAll()
//        {
//            try
//            {
//                return _dbContext.Tables.ToList();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new List<TableBO>();
//            }

//        }

//        //public IEnumerable<TableBO> GetByUser(IdentityUser user)
//        //{
//        //    try
//        //    {
//        //        return _dbContext.Tables
//        //        .Where(i => i.User.Id == user.Id).ToList();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine(ex.ToString());
//        //        return new List<Meal>();
//        //    }
//        //}

//        public TableBO GetTable(string id)
//        {
//            try
//            {
//                return _dbContext.Tables.Single(i => i.TableID == id);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//                return new TableBO();
//            }
//        }

//    }
//}
