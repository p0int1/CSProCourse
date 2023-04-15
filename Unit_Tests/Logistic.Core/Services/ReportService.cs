using Logistic.Enums;
using Logistic.ConsoleClient.Repositories;

namespace Logistic.ConsoleClient.Services
{
    public class ReportService<TEntity> where TEntity : class
    {
        private readonly IReportRepository<TEntity> jsonRepository; 
        private readonly IReportRepository<TEntity> xmlRepository;
        string reportDir;

        public ReportService(IReportRepository<TEntity> json, IReportRepository<TEntity> xml)
        {
            jsonRepository = json;
            xmlRepository = xml;
            string appDir = Directory.GetCurrentDirectory();
            reportDir = Path.Combine(appDir, "reports");
            Directory.CreateDirectory(reportDir);
        }

        public void CreateReport(List<TEntity> listEntity, ReportType reportType)
        {
            string reportPath = Path.Combine(reportDir,
                typeof(TEntity).ToString().Split('.').Last() +
                $"_{DateTime.Now.ToString("dd.MM.yyyy_HHmmss")}");
            if (reportType == ReportType.json)
            {
                jsonRepository.Create(listEntity, reportPath + ".json");
            }
            else if (reportType == ReportType.xml)
            {
                xmlRepository.Create(listEntity, reportPath + ".xml");
            }
        }

        public List<TEntity> LoadReport(string filePath)
        {
            var actionType = filePath.Split('.').Last();
            if (actionType == "json") 
            {
                return jsonRepository.Read(filePath);
            } 
            else if (actionType == "xml") 
            { 
                return xmlRepository.Read(filePath); 
            }
            return null;
        }
    }
}
