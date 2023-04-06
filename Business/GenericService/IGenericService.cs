using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.GenericService
{
    public interface IGenericService<T> where T : class
    {
        public  void create(T obj);
        public  void delete(string obj);

        public  void edit(T entityToUpdate);

        //public T get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] including);
        public T getByID(string id);

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
          params Expression<Func<T, object>>[] naProperties);
    }
}
