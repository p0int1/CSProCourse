namespace Logistic.Core.Services
{
    public interface IEntityService<TEntity>
    {
        public void Create(TEntity entity);

        public TEntity GetById(int entityId);

        public List<TEntity> GetAll();

        public bool DeleteById(int entityId);

        public void DeleteAll();

        public bool LoadCargo(TEntity entity, int entityId);

        public void UnloadCargo(TEntity entity, int entityId);

        public void UnloadAllCargos(TEntity entity, int entityId);
    }
}
