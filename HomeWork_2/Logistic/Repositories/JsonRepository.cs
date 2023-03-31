using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace Logistic.ConsoleClient.Repositories
{
    public class JsonRepository<TEntity> : IReportRepository<TEntity> where TEntity : class
    {
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        public void Create(List<TEntity> entity, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, entity, options);
            }
        }

        public List<TEntity> Read(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<List<TEntity>>(fs);
            }
        }
    }
}
