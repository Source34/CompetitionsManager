using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionManager.Core.Domains.TaskResults.Repositories
{
    public interface ITaskResultRepository
    {
        public Task<TaskResult> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<List<TaskResult>> GetAllAsync(CancellationToken cancellationToken);
        public Task CreateAsync(TaskResult taskResult, CancellationToken cancellationToken);
        public Task UpdateAsync(TaskResult taskResult, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
