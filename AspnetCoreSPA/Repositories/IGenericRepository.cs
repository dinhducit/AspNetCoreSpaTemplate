using System.Linq;

namespace AspnetCoreSPATemplate.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        // TODO: can add filter condition and order by as parameter
        IQueryable<TEntity> GetAll(string includeProperties = "");

        // TODO: add these methods if we need them.
        // void Add(TEntity entity);
        // void Update(TEntity entity);
        // void Remove(TEntity entity);
    }
}
