using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Logistic.ConsoleClient.Repositories
{
    public class JsonRepository<T>
    {
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        public void Create(T entity, string reportDir)
        {
            var reportPath = Path.Combine(reportDir,
                entity.GetType().GetGenericArguments()[0].ToString().Split('.').Last() +
                $"_{DateTime.Now.ToString("dd.MM.yyyy_HHmmss")}.json");
            using (FileStream fs = new FileStream(reportPath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, entity, options);
            }
        }

        public T Read(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<T>(fs);
            }
        }
    }
}
