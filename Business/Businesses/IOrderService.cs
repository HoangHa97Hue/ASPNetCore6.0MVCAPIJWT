//using Business.GenericService;
//using Microsoft.AspNetCore.Identity;
//using Service.Entities;
//using System.Linq.Expressions;

//namespace Business
//{
//    public interface IOrderService :IGenericService<Order>
//    {
//        //IEnumerable<Order> GetByUser(IdentityUser user);
//        //IEnumerable<Order> GetByUserAndOrderProcess(IdentityUser user, String orderProcess);

//        //IEnumerable<Order> GetByOrderProcess(String orderProcess);
//        IEnumerable<Order> GetAll(Expression<Func<Order, bool>>? filter,
//            Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy ,
//            params Expression<Func<Order, object>>[] naProperties);

//    }
//}
