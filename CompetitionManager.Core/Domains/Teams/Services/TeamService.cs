using CompetitionManager.Core.Domains.Competitions.Repositories;
using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.Teams.Repositories;
using CompetitionManager.Core.Exceptions;
using CompetitionManager.Core.Seriveces;

namespace CompetitionManager.Core.Domains.Teams.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(ITeamRepository teamRepository, ICompetitionRepository competitionRepository, IUnitOfWork unitOfWork)
        {
            _teamRepository = teamRepository;
            _competitionRepository = competitionRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Team> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _teamRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<List<Team>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _teamRepository.GetAllAsync(cancellationToken);
        }

        public async Task CreateAsync(Team team, CancellationToken cancellationToken)
        {
            await _teamRepository.CreateAsync(team, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Team team, CancellationToken cancellationToken)
        {
            await _teamRepository.UpdateAsync(team, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _teamRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<TaskResult>> GetTaskResultsByTeamIdAsync(int id, int competitionId, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(id, cancellationToken);
            var competition = await _competitionRepository.GetByIdAsync(competitionId, cancellationToken);

            // Выбираем результаты, которые есть одновременно у команды и соревнования
            var competitionsResultsIds = competition.Results?.Select(x => x.TaskResultId) ?? new List<int>();
            var results = team.Results?.Where(x => competitionsResultsIds.Contains(x.TaskResultId)).ToList() ?? new List<TaskResult>();

            return results;
        }

        public async Task<List<Team>> GetByNameAsync(string namePart, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(namePart)) return new List<Team>();
            return await _teamRepository.GetByNameAsync(namePart, cancellationToken);
        }
    }
}
