namespace Logistic.ConsoleClient.Repositories
{
    public interface IRepository<TEntity>
    {
        public void Create(TEntity entity);

        public TEntity Read(int id);

        public List<TEntity> ReadAll();

        public void Update(TEntity newEntity, int id);

        public bool Delete(int id);

        public void DeleteAll();
    }
}
