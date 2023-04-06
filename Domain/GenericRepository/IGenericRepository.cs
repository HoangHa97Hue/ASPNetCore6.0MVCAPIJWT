using Microsoft.AspNetCore.Identity;
using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        public void create(T obj);

        public  T getByID(string id);

        public void edit(T obj);

        public void delete(string obj);

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter , Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
          params Expression<Func<T, object>>[] naProperties);
    }
}
