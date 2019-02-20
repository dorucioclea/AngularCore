using System;
using System.Linq;
using System.Linq.Expressions;
using AngularCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularCore.Repositories.Impl
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected DbContext Context { get; }
        protected DbSet<T> Entity { get; }

        public Repository(DbContext context)
        {
            Context = context;
            Entity = Context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            Entity.Add(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            Entity.Remove(entity);
            Context.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            return Entity.AsQueryable();
        }

        public virtual T GetById(string id)
        {
            return Entity.Find(id);
        }

        public virtual IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return Entity.Where(predicate).AsQueryable();
        }

        public virtual void Update(T entity)
        {
            Entity.Update(entity);
            Context.SaveChanges();
        }
    }
}