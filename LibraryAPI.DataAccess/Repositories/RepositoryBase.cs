using LibraryAPI.DataAccess.Contracts;
using LibraryAPI.DataAccess.EFCore;

namespace LibraryAPI.DataAccess.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    {
        protected readonly DataBaseContext _dataBaseContext;

        public RepositoryBase(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public void Add(TEntity entity)
        {
            _dataBaseContext.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dataBaseContext.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dataBaseContext.Update(entity);
        }
    }
}
