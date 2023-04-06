//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Newtonsoft.Json;
//using Service.Repositories;
//using System.Text;
//using Service.Repositories;
//using Service.Entities;
//using NuGet.Protocol.Core.Types;
//using System.Linq.Expressions;
//using Business.GenericRepository;
//using Business.GenericService;

//namespace Business
//{
//    public class MealService : GenericService<Meal>,IMealService
//    {

//        public MealService(IGenericRepository<Meal> genericRepository) : base(genericRepository)
//        {

//        }

//        public IEnumerable<Meal> GetAll(Expression<Func<Meal, bool>>? filter, Func<IQueryable<Meal>, IOrderedQueryable<Meal>>? orderBy, params Expression<Func<Meal, object>>[] naProperties)
//        {
//            Expression<Func<Meal, object>> parameter1 = v => v.OrderDetails;
//            Expression<Func<Meal, object>>[] parameterArray = new Expression<Func<Meal, object>>[] { parameter1 };


//            return _repository.GetAll(filter, naProperties: parameterArray);  //_Repository nay nam ben class GenericService vi ctor khoi tao lam cho thang GenericService no khoi tao theo
//        }

//    }
//}
