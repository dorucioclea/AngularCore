using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AngularCore.Data.Models;

namespace AngularCore.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(string id);

        IQueryable<T> GetAll();

        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}