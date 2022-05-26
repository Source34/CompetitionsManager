using CompetitionManager.Core.Domains.TaskResults;

namespace CompetitionManager.Core.Domains.Competitions.Services
{
    public interface ICompetitionService
    {
        public Task<Competition> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<List<Competition>> GetAllAsync(CancellationToken cancellationToken);
        public Task CreateAsync(Competition competition, CancellationToken cancellationToken);
        public Task UpdateAsync(Competition competition, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
        public Task<double> GetTeamPointsByCompetitonIdAsync(int competitionId, int teamId, CancellationToken cancellationToken);
    }
}
