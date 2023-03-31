using System.Linq;
using System.Collections.Generic;
using Logistic.ConsoleClient.Models;
using AutoMapper;

namespace Logistic.ConsoleClient.Repositories
{
    public class InMemeoryRepository<TEntity> where TEntity : IEntity, new()
    {
        private int lastlId = 0;
        private readonly List<TEntity> _entities = new List<TEntity>();
        private readonly MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TEntity>());

        private T DeepCopy<T>(T entity) => new Mapper(config).Map<T>(entity);

        public void Create(TEntity entity)
        {
            entity.Id = ++lastlId;
            _entities.Add(DeepCopy(entity));
        }

        public TEntity Read(int id)
        {
            var entityById = _entities.FirstOrDefault(x => x.Id == id);
            if (entityById == null) return entityById;
            return DeepCopy(entityById);
        }

        public List<TEntity> ReadAll() => DeepCopy(_entities);

        public void Update(TEntity newEntity, int id)
        {
            var entityById = _entities.FirstOrDefault(x => x.Id == id);
            newEntity.Id = entityById.Id;
            _entities.Remove(entityById);
            _entities.Add(DeepCopy(newEntity));
        }

        public bool Delete(int id)
        {
            var entityById = _entities.FirstOrDefault(x => x.Id == id);
            _entities.Remove(entityById);
            return true;
        }

        public void DeleteAll()
        {
            lastlId = 0;
            _entities.Clear();
        }
    }
}
