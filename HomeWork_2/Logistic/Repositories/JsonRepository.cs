using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Logistic.ConsoleClient.Repositories
{
    public class JsonRepository<T>
    {
        public void Create(T entity, string reportDir)
        {
            var reportPath = Path.Combine(reportDir,
                entity.GetType().GetGenericArguments()[0].ToString().Split('.').Last() +
                $"_{DateTime.Now.ToString("MM.dd.yyyy_HHmmss")}.json");
            using (StreamWriter sw = new StreamWriter(reportPath, false))
            {
                string json = JsonConvert.SerializeObject(entity, Formatting.Indented);
                sw.WriteLine(json);
            }
        }

        public T Read(string filePath)
        {
            using (StreamReader sw = new StreamReader(filePath))
            {
                string json = sw.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}
