using System;
using System.Linq;
using System.Linq.Expressions;

namespace MonstersInc.Repository
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        IQueryable<T> Get();
        void Save();
        void Add(T entity, bool isSave = true);
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
    }
}