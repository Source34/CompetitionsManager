using AutoMapper;
using CompetitionManager.Core.Domains.Competitions;
using CompetitionManager.Core.Domains.Competitions.Repositories;
using CompetitionManager.Core.Exceptions;
using CompetitionManager.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CompetitionManager.Data.Entities.Competitions.Repository
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private readonly CompetitionDbContext _competitionDbContext;
        private readonly IMapper _mapper;

        public CompetitionRepository(CompetitionDbContext competitionDbContext, IMapper mapper)
        {
            _competitionDbContext = competitionDbContext;
            _mapper = mapper;
        }

        public async Task CreateAsync(Competition competition, CancellationToken cancellationToken)
        {
            var trainingTeams = _competitionDbContext.Teams
                .Where(t => competition.Teams.Select(x => x.TeamId).Contains(t.TeamId)).ToList();

            var tasks = _competitionDbContext.Tasks
                .Where(t => competition.Tasks.Select(x => x.TaskId).Contains(t.TaskId)).ToList();

            var taskResults = _competitionDbContext.TaskResult
                .Where(t => competition.Results.Select(x => x.TaskResultId).Contains(t.TaskResultId)).ToList();

            var entityCompetition = _mapper.Map<CompetitionDbModel>(competition);
            entityCompetition.Teams = trainingTeams;
            entityCompetition.Tasks = tasks;
            entityCompetition.Results = taskResults;

            await _competitionDbContext.Competitions.AddAsync(entityCompetition, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var competition = await _competitionDbContext.Competitions
                .FirstOrDefaultAsync(it => it.CompetitionId == id, cancellationToken);

            if (competition == null)
            {
                throw new ObjectNotFoundException($"Ошибка удаления! " +
                                                  $"Объект Competition c Id = {id} не найден!");
            }

            _competitionDbContext.Competitions.Remove(competition);
        }

        public Task<List<Competition>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _competitionDbContext.Competitions
                .Include(c => c.Tasks)
                .Include(c => c.Results)
                .Include(c => c.Teams)
                    .ThenInclude(p => p.Results)
                .AsNoTracking()
                .Select(competition => _mapper.Map<Competition>(competition))
                .ToListAsync(cancellationToken);
        }

        public async Task<Competition> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var dbCoach = await _competitionDbContext.Competitions
                .Include(c => c.Tasks)
                .Include(c => c.Results)
                .Include(c => c.Teams)
                    .ThenInclude(p => p.Results)
                .AsNoTracking()
                .FirstOrDefaultAsync(it => it.CompetitionId == id, cancellationToken);

            if (dbCoach == null)
            {
                throw new ObjectNotFoundException($"Ошибка получения! " +
                                                  $"Объект Competition c Id = {id} не найден!");
            }

            return _mapper.Map<Competition>(dbCoach);
        }

        public async Task UpdateAsync(Competition competition, CancellationToken cancellationToken)
        {
            var dbCompetition = await _competitionDbContext.Competitions
                .Include(c => c.Tasks)
                .Include(c => c.Teams)
                .FirstOrDefaultAsync(x => x.CompetitionId == competition.CompetitionId);

            if (dbCompetition == null)
            {
                throw new ObjectNotFoundException($"Ошибка обновления! " +
                                                  $"Объект Competition c Id = {competition.CompetitionId} не найден!");
            }

            var trainingTeams = _competitionDbContext.Teams
                .Where(t => competition.Teams.Select(x => x.TeamId).Contains(t.TeamId)).ToList();

            var tasks = _competitionDbContext.Tasks
                .Where(t => competition.Tasks.Select(x => x.TaskId).Contains(t.TaskId)).ToList();

            var taskResults = _competitionDbContext.TaskResult
                .Where(t => competition.Results.Select(x => x.TaskResultId).Contains(t.TaskResultId)).ToList();

            dbCompetition.CompetitionName = competition.CompetitionName;
            dbCompetition.Description = competition.Description;
            dbCompetition.StartEventDate = competition.StartEventDate;
            dbCompetition.EndEventDate = competition.EndEventDate;
            dbCompetition.Teams = trainingTeams;
            dbCompetition.Tasks = tasks;
            dbCompetition.Results = taskResults;
        }
    }
}
