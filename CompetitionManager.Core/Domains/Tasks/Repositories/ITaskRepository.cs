using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompetitionManager.Core.Domains.Tasks.Repositories
{
    public interface ITaskRepository
    {
        public System.Threading.Tasks.Task<Tasks.Task> GetByIdAsync(int id, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task<List<Tasks.Task>> GetAllAsync(CancellationToken cancellationToken);
        public System.Threading.Tasks.Task CreateAsync(CompetitionManager.Core.Domains.Tasks.Task task, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task UpdateAsync(CompetitionManager.Core.Domains.Tasks.Task task, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task DeleteAsync(int id, CancellationToken cancellationToken);
        public System.Threading.Tasks.Task<List<Tasks.Task>> GetByNameAsync(string namePart, CancellationToken cancellationToken);        
    }
}
