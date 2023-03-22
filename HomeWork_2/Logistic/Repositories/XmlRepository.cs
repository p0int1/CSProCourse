using Logistic.ConsoleClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Logistic.ConsoleClient.Repositories
{
    public class XmlRepository<T>
    {
        public void Create(T entity, string reportDir)
        {
            var reportPath = Path.Combine(reportDir,
                entity.GetType().GetGenericArguments()[0].ToString().Split('.').Last() +
                $"_{DateTime.Now.ToString("MM.dd.yyyy_HHmmss")}.xml");

            using (StreamWriter sw = new StreamWriter(reportPath, false))
            {
                string json = JsonConvert.SerializeObject(entity);
                XNode xml = JsonConvert.DeserializeXNode("{\"entity\":" + json + "}", "root");
                sw.WriteLine(xml);
            }
        }

        public void Read(string filePath)
        {
            using (StreamReader sw = new StreamReader(filePath))
            {
                string xml = sw.ReadToEnd();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string json = JsonConvert.SerializeXmlNode(doc);
                Console.WriteLine(json);
                //return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}
