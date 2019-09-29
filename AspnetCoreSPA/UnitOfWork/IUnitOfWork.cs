using AspnetCoreSPATemplate.Repositories;


namespace AspnetCoreSPATemplate.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;

        // TODO: add these methods if we need them.
        // void Save();
    }
}
