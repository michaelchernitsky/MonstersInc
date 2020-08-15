using Microsoft.EntityFrameworkCore;
using MonstersInc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MonstersInc.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T: class
    {
        protected MonstersIncDBContext _monstersIncDBContext { get; set; }

        public BaseRepository(MonstersIncDBContext monstersIncDBContext)
        {
            _monstersIncDBContext = monstersIncDBContext;
        }

        public IQueryable<T> Get()
        {
            return this._monstersIncDBContext.Set<T>();
        }

        public void Add(T entity, bool isSave = true)
        {
            this._monstersIncDBContext.Set<T>().Add(entity);
            if (isSave)
                this.Save();
        }

        public void Save()
        {
            this._monstersIncDBContext.SaveChanges();
        }

        public void Dispose()
        {
            this._monstersIncDBContext.Dispose();
        }

        public IQueryable<T> Get(Expression<Func<T,bool>> expression)
        {
            return this._monstersIncDBContext.Set<T>().Where(expression);
        }
    }
}
