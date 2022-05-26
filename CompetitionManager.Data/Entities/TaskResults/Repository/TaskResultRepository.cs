using AutoMapper;
using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.TaskResults.Repositories;
using CompetitionManager.Core.Exceptions;
using CompetitionManager.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CompetitionManager.Data.Entities.TaskResults.Repository
{
    public class TaskResultRepository : ITaskResultRepository
    {
        private readonly CompetitionDbContext _competitionDbContext;
        private readonly IMapper _mapper;

        public TaskResultRepository(CompetitionDbContext competitionDbContext, IMapper mapper)
        {
            _competitionDbContext = competitionDbContext;
            _mapper = mapper;
        }

        public async Task CreateAsync(TaskResult taskResult, CancellationToken cancellationToken)
        {
            var dbCompetition = await _competitionDbContext.Competitions.FirstOrDefaultAsync(it => it.CompetitionId == taskResult.Competition.CompetitionId, cancellationToken);
            if (dbCompetition == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbCompetition exception!");

            var dbTeam = await _competitionDbContext.Teams.FirstOrDefaultAsync(it => it.TeamId == taskResult.Team.TeamId, cancellationToken);
            if (dbTeam == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbTeam exception!");

            var dbTask = await _competitionDbContext.Tasks.FirstOrDefaultAsync(it => it.TaskId == taskResult.Task.TaskId, cancellationToken);
            if (dbTask == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbTask exception!");


            var entityTaskResult = _mapper.Map<TaskResultDbModel>(taskResult);
            entityTaskResult.Competition = dbCompetition;
            entityTaskResult.Team = dbTeam;
            entityTaskResult.Task = dbTask;
            await _competitionDbContext.TaskResult.AddAsync(entityTaskResult, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var dbTaskResult = await _competitionDbContext.TaskResult
                .FirstOrDefaultAsync(it => it.TaskResultId == id, cancellationToken);
            if (dbTaskResult == null)
            {
                throw new ObjectNotFoundException($"Ошибка удаления! " +
                                                  $"Объект TaskResult c Id = {id} не найден!");
            }

            _competitionDbContext.TaskResult.Remove(dbTaskResult);
        }

        public Task<List<TaskResult>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _competitionDbContext.TaskResult
                .Include(t => t.Competition)
                .Include(t => t.Team)
                .Include(t => t.Task)
                .Select(t => _mapper.Map<TaskResult>(t))
                .ToListAsync(cancellationToken);
        }

        public async Task<TaskResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var dbTaskResult = await _competitionDbContext.TaskResult
                .Include(t => t.Competition)
                .Include(t => t.Team)
                .Include(t => t.Task)
                .AsNoTracking()
                .FirstOrDefaultAsync(it => it.TaskResultId == id, cancellationToken);

            if (dbTaskResult == null)
            {
                throw new ObjectNotFoundException($"Ошибка получения! " +
                                                  $"Объект TaskResult c Id = {id} не найден!");
            }

            return _mapper.Map<TaskResult>(dbTaskResult);
        }

        public async Task UpdateAsync(TaskResult taskResult, CancellationToken cancellationToken)
        {
            var dbTaskResult = await _competitionDbContext.TaskResult
                .Include(t => t.Competition)
                .Include(t => t.Team)
                .Include(t => t.Task)
                .FirstOrDefaultAsync(it => it.TaskResultId == taskResult.TaskResultId, cancellationToken);

            if (dbTaskResult == null)
            {
                throw new ObjectNotFoundException($"Ошибка обновления! " +
                                                  $"Объект TaskResult c Id = {taskResult.TaskResultId} не найден!");
            }

            var dbCompetition = await _competitionDbContext.Competitions.FirstOrDefaultAsync(it => it.CompetitionId == taskResult.Competition.CompetitionId, cancellationToken);
            if (dbCompetition == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbCompetition exception!");

            var dbTeam = await _competitionDbContext.Teams.FirstOrDefaultAsync(it => it.TeamId == taskResult.Team.TeamId, cancellationToken);
            if (dbTeam == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbTeam exception!");

            var dbTask = await _competitionDbContext.Tasks.FirstOrDefaultAsync(it => it.TaskId == taskResult.Task.TaskId, cancellationToken);
            if (dbTask == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbTask exception!");

            dbTaskResult.ElapsedMinutes = taskResult.ElapsedMinutes;
            dbTaskResult.Result = taskResult.Result;
            dbTaskResult.Competition = dbCompetition;
            dbTaskResult.Team = dbTeam;
            dbTaskResult.Task = dbTask;
        }
    }
}
