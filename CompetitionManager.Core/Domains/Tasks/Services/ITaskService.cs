namespace CompetitionManager.Core.Domains.Tasks.Services
{
    public interface ITaskService
    {
        public System.Threading.Tasks.Task<Tasks.Task> GetByIdAsync(int id, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task<List<Tasks.Task>> GetAllAsync(CancellationToken cancellationToken);
        public System.Threading.Tasks.Task CreateAsync(Tasks.Task task, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task UpdateAsync(Tasks.Task task, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task DeleteAsync(int id, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task<List<Tasks.Task>> GetByNameAsync(string namePart, CancellationToken cancellationToken);

    }
}
