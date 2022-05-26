namespace CompetitionManager.Core.Domains.TaskResults.Services
{
    public interface ITaskResultService
    {
        public Task<TaskResult> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<List<TaskResult>> GetAllAsync(CancellationToken cancellationToken);
        public Task CreateAsync(TaskResult taskResult, CancellationToken cancellationToken);
        public Task UpdateAsync(TaskResult taskResult, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
