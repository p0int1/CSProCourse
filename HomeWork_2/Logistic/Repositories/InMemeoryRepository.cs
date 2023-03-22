using AutoMapper;
using Logistic.ConsoleClient.Models;
using System.Collections.Generic;
using System.Linq;

namespace Logistic.ConsoleClient.Repositories
{
    public class InMemeoryRepository<TEntity> where TEntity : IEntity, new()
    {
        private List<TEntity> Entity { get; set; } = new List<TEntity>();
        private TEntity entityById;

        public bool CheckById(int id) 
        {
            entityById = Entity.FirstOrDefault(x => x.Id == id);
            return (entityById == null) ? false : true; 
        }

        public void Create(TEntity entity) => Entity.Add(entity);

        public TEntity Read() => DeepCopy(entityById);

        public List<TEntity> ReadAll() => Entity.Select(x => DeepCopy(x)).ToList();

        public void Update(TEntity newEntity)
        {
            Entity.Remove(entityById);
            Entity.Add(newEntity);
        }

        public void Delete() => Entity.Remove(entityById);

        public void DeleteAll() => Entity.Clear();

        private T DeepCopy<T>(T entity) where T : new()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, T>());
            var mapper = new Mapper(config);
            var clon = new T();
            mapper.Map(entity, clon);
            return clon;
        }
    }
}
