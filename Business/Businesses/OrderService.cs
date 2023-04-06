//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Newtonsoft.Json;
//using System.Text;
//using Service.Entities;
//using Business.GenericRepository;
//using System.Linq.Expressions;
//using Business.GenericService;

//namespace Business
//{
//    public class OrderService : GenericService<Order>, IOrderService
//    {
//        public OrderService(IGenericRepository<Order> genericRepository) : base(genericRepository)
//        {

//        }

//        public IEnumerable<Order> GetAll(Expression<Func<Order, bool>>? filter, Func<IQueryable<Order>, IOrderedQueryable<Order>>? orderBy , params Expression<Func<Order, object>>[] naProperties)
//        {
//            Expression<Func<Order, object>> parameter1 = v => v.OrderProcess;
//            Expression<Func<Order, object>> parameter2 = v => v.User;
//            Expression<Func<Order, object>>[] parameterArray = new Expression<Func<Order, object>>[] { parameter1, parameter2 };
//            //neu nhu muon truyen 2 parram (tuong ung voi dinh kem them 2 class vao )
//            //Expression<Func<MealCategory, object>>[] parameterArray = new Expression<Func<MealCategory, object>>[] { parameter1, parameter2 };

//            return _repository.GetAll(filter, orderBy: q => q.OrderBy(x => x.DateUpdated) , naProperties: parameterArray);
//        }
//    }
//}
