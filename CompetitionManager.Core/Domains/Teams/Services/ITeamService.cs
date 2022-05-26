using CompetitionManager.Core.Domains.TaskResults;

namespace CompetitionManager.Core.Domains.Teams.Services
{
    public interface ITeamService
    {
        public Task<Team> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<List<Team>> GetAllAsync(CancellationToken cancellationToken);
        public Task CreateAsync(Team team, CancellationToken cancellationToken);
        public Task UpdateAsync(Team team, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
        public Task<List<TaskResult>> GetTaskResultsByTeamIdAsync(int id, int competitionId, CancellationToken cancellationToken);
        public Task<List<Team>> GetByNameAsync(string namePart, CancellationToken cancellationToken);

    }
}
