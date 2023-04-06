using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Contexts;
using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Business.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public MyDbContext context;
        public DbSet<T> dbSet;

        public GenericRepository(MyDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual void create(T obj)
        {
            var query = this.dbSet.AsQueryable();
            this.dbSet.Add(obj);
            //this.dbSet = EntityState.Modified;
        }

        public virtual void delete(string obj)
        {
            T entityToDelete = dbSet.Find(obj);
            if(context.Entry(entityToDelete).State!= EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);//begin to track
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void edit(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified; // to tracking to update by unit of work
        }

        public virtual T getByID(string id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            params Expression<Func<T, object>>[] naProperties)
        {
            IQueryable<T> dbQuery = dbSet;

            if (filter != null)
            {
                dbQuery = dbQuery.Where(filter);
            }

            foreach (Expression<Func<T, object>> nProperty in naProperties)
                dbQuery = dbQuery.Include<T, object>(nProperty);

            if (orderBy != null)
            {
                dbQuery = orderBy(dbQuery);
            }

            return dbQuery.ToList();
        }

    }
}
