using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Logistic.ConsoleClient.Repositories
{
    public class XmlRepository<TEntity> : IReportRepository<TEntity> where TEntity : class
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TEntity>));

        public void Create(List<TEntity> entity, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, entity);
            }
        }

        public List<TEntity> Read(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return xmlSerializer.Deserialize(fs) as List<TEntity>;
            }
        }
    }
}
