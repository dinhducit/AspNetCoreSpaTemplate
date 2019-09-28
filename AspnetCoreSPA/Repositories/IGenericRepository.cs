using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspnetCoreSPATemplate.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        // TODO: can add filter condition and order by as parameter
        IQueryable<TEntity> Search(string includeProperties = "");

        // TODO: add these methods if we need them.
        // void Add(TEntity entity);
        // void Update(TEntity entity);
        // void Remove(TEntity entity);
    }
}
