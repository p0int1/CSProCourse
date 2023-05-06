namespace Logistic.ConsoleClient.Repositories
{
    public interface IReportRepository<TEntity>
    {
        public void Create(List<TEntity> entity, string reportDir);

        public List<TEntity> Read(string filePath);
    }
}
