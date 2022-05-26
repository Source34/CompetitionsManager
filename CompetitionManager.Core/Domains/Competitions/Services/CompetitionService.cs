using CompetitionManager.Core.Domains.Competitions.Repositories;
using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.Teams.Repositories;
using CompetitionManager.Core.Seriveces;

namespace CompetitionManager.Core.Domains.Competitions.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly ICompetitionRepository _competitionRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompetitionService(ICompetitionRepository competitionRepository, ITeamRepository teamRepository, IUnitOfWork unitOfWork)
        {
            _competitionRepository = competitionRepository;
            _teamRepository = teamRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Competition> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _competitionRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<List<Competition>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _competitionRepository.GetAllAsync(cancellationToken);
        }

        public async Task CreateAsync(Competition competition, CancellationToken cancellationToken)
        {
            await _competitionRepository.CreateAsync(competition, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Competition competition, CancellationToken cancellationToken)
        {
            await _competitionRepository.UpdateAsync(competition, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _competitionRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<double> GetTeamPointsByCompetitonIdAsync(int competitionId, int teamId, CancellationToken cancellationToken)
        {
            var competition = await _competitionRepository.GetByIdAsync(competitionId, cancellationToken);
            var team = await _teamRepository.GetByIdAsync(teamId, cancellationToken);

            // Выбираем результаты, которые есть одновременно у соревнования и команды
            var competitionsResultsIds = competition.Results?.Select(x => x.TaskResultId) ?? new List<int>();
            var results = team.Results?.Where(x => competitionsResultsIds.Contains(x.TaskResultId)).ToList() ?? new List<TaskResult>();

            var totalResults = results.Select(x => x.Result).Sum();

            return totalResults;
        }
    }
}
