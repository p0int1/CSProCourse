using Logistic.Enums;
using Logistic.Models;

namespace Logistic.Core.Services
{
    public interface IReportService<TEntity>
    {
        public string CreateReport(List<TEntity> listEntity, ReportType reportType, string reportDir);

        public List<TEntity> LoadReport(string filePath);
    }
}
