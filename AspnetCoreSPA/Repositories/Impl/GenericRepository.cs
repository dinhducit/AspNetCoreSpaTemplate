using System;
using System.Linq;
using AspnetCoreSPATemplate.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreSPATemplate.Repositories.Impl
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        internal readonly ApplicationDbContext _context;
        internal readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll(string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            // include relate object
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
    }
}
