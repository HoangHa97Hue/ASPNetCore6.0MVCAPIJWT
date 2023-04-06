using Business.GenericRepository;
using Service.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.GenericService
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        public readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            this._repository = repository;
            //this._iUnitOfWork = iUnitOfWork;
        }

        public void create(T obj)
        {
            try
            {
                _repository.create(obj);
            }
            catch(Exception ex)
            {
                
            }
        }

        public void delete(string obj)
        {
            try
            {
                _repository.delete(obj);
            }
            catch (Exception ex)
            {

            }
        }

        public void edit(T entityToUpdate)
        {
            try
            {
                _repository.edit(entityToUpdate);
            }
            catch (Exception ex)
            {

            }
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, params Expression<Func<T, object>>[] naProperties)
        {
            try
            {
                return _repository.GetAll(filter,orderBy, naProperties: naProperties);
                //_repository.GetAll(filter,);
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<T>();
            }
        }

        public T getByID(string id)
        {
            try
            {
                return _repository.getByID(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}
