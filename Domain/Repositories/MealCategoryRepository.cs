//using Business.GenericRepository;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Service.Contexts;
//using Service.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

//namespace Service.Repositories
//{
//    public class MealCategoryRepository : GenericRepository<MealCategory>, IMealCategoryRepository
//    {
//        //private readonly MyDbContext _dbContext;

//        public MealCategoryRepository(MyDbContext context) : base(context)
//        {

//        }

//        //public IEnumerable<Meal> GelAllExpression(Expression<Func<MealCategory, bool>> filter = null,
//        //    Func<IQueryable<MealCategory>, IOrderedQueryable<MealCategory>> orderBy = null,
//        //    params Expression<Func<MealCategory, object>>[] naProperties)
//        //{
//        //    IQueryable<T> dbQuery = dbSet;

//        //    if (filter != null)
//        //    {
//        //        dbQuery = dbQuery.Where(filter);
//        //    }

//        //    foreach (Expression<Func<T, object>> nProperty in naProperties)
//        //        dbQuery = dbQuery.Include<T, object>(nProperty);

//        //    if (orderBy != null)
//        //    {
//        //        dbQuery = orderBy(dbQuery);
//        //    }

//        //    return dbQuery.ToList();
//        //}


//        //public MealCategory createMealCategory(MealCategory mealCategory)
//        //{
//        //    try
//        //    {
//        //        _dbContext.MealCategorys.Add(mealCategory);

//        //        var result = _dbContext.SaveChanges();

//        //        if (result > 0)
//        //        {
//        //            return mealCategory;
//        //        }
//        //        return new MealCategory();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine(ex.ToString());
//        //        return new MealCategory();
//        //    }
//        //}

//        //public void DeleteMealCategory(string id)
//        //{
//        //    var mealCategory = GetMealCategory(id);
//        //    try
//        //    {
//        //        _dbContext.MealCategorys.Remove(mealCategory);

//        //        _dbContext.SaveChanges();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine(ex.ToString());
//        //    }
//        //}

//        //public void EditMealCategory(MealCategory mealCategory)
//        //{
//        //    _dbContext.MealCategorys.Update(mealCategory);

//        //    var result = _dbContext.SaveChanges();
//        //}

//        //public IEnumerable<MealCategory> GetAll()
//        //{
//        //    try
//        //    {
//        //        return _dbContext.MealCategorys.ToList();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine(ex.ToString());
//        //        return new List<MealCategory>();
//        //    }

//        //}

//        ////public IEnumerable<MealCategory> GetByUser(IdentityUser user)
//        ////{
//        ////    try
//        ////    {
//        ////        return _dbContext.MealCategorys
//        ////        .Where(i => i.User.Id == user.Id).ToList();
//        ////    }
//        ////    catch (Exception ex)
//        ////    {
//        ////        Console.WriteLine(ex.ToString());
//        ////        return new List<MealCategory>();
//        ////    }
//        ////}

//        //public MealCategory GetMealCategory(string id)
//        //{
//        //    try
//        //    {
//        //        return _dbContext.MealCategorys.Single(i => i.MealCategoryID == id);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        Console.WriteLine(ex.ToString());
//        //        return new MealCategory();
//        //    }
//        //}

//    }
//}
