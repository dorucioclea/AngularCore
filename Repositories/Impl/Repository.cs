using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AngularCore.Data.Contexts;
using AngularCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularCore.Repositories.Impl
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        protected DbContext Context { get => _context; }

        private readonly DbSet<T> _entity;
        protected DbSet<T> Entity { get => _entity; }

        public Repository(DbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _entity.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _entity.Remove(entity);
            _context.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _entity.AsQueryable();
        }

        public virtual T GetById(string id)
        {
            return _entity.Find(id);
        }

        public virtual IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return _entity.Where(predicate).AsQueryable();
        }

        public virtual void Update(T entity)
        {
            _entity.Update(entity);
            _context.SaveChanges();
        }
    }
}