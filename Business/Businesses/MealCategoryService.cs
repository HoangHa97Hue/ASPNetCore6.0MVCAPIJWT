//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Newtonsoft.Json;
//using System.Text;
//using Service.Repositories;
//using Service.Entities;
//using Service.UnitOfWorks;
//using System.Linq.Expressions;
//using Business.GenericRepository;
//using Business.GenericService;

//namespace Business
//{
//    public class MealCategoryService : GenericService<MealCategory>,IMealCategoryService
//    {
//        public MealCategoryService(IGenericRepository<MealCategory> genericRepository): base(genericRepository)
//        {

//        }


//        public IEnumerable<MealCategory> GetAll(Expression<Func<MealCategory, bool>>? filter, Func<IQueryable<MealCategory>, IOrderedQueryable<MealCategory>>? orderBy , params Expression<Func<MealCategory, object>>[] naProperties)
//        {
//            Expression<Func<MealCategory, object>> parameter1 = v => v.Meals;
//            Expression<Func<MealCategory, object>>[] parameterArray = new Expression<Func<MealCategory, object>>[] { parameter1};

//            return _repository.GetAll(filter,naProperties: parameterArray);  
//        }
//    }
//}
