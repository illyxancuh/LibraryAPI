namespace LibraryAPI.DataAccess.Contracts
{
    public interface IRepositoryBase<TEntity>
    {
        public void Add(TEntity entity);

        public void Delete(TEntity entity);

        public void Update(TEntity entity);
    }
}
