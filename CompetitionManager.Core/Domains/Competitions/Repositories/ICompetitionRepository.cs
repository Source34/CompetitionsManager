using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionManager.Core.Domains.Competitions.Repositories
{
    public interface ICompetitionRepository
    {
        public Task<Competition> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<List<Competition>> GetAllAsync(CancellationToken cancellationToken);
        public Task CreateAsync(Competition competition, CancellationToken cancellationToken);
        public Task UpdateAsync(Competition competition, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
