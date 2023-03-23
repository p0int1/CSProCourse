using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Logistic.ConsoleClient.Repositories
{
    public class XmlRepository<T> where T : class
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

        public void Create(T entity, string reportDir)
        {
            var reportPath = Path.Combine(reportDir,
                entity.GetType().GetGenericArguments()[0].ToString().Split('.').Last() +
                $"_{DateTime.Now.ToString("dd.MM.yyyy_HHmmss")}.xml");
            using (FileStream fs = new FileStream(reportPath, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, entity);
            }
        }

        public T Read(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return xmlSerializer.Deserialize(fs) as T;
            }
        }
    }
}
