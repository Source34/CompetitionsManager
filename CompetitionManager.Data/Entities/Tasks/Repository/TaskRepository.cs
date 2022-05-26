using AutoMapper;
using CompetitionManager.Core.Domains.Tasks.Repositories;
using CompetitionManager.Core.Exceptions;
using CompetitionManager.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CompetitionManager.Data.Entities.Tasks.Repository
{
    internal class TaskRepository : ITaskRepository
    {
        private readonly CompetitionDbContext _competitionDbContext;
        private readonly IMapper _mapper;

        public TaskRepository(CompetitionDbContext competitionDbContext, IMapper mapper)
        {
            _competitionDbContext = competitionDbContext;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task CreateAsync(Core.Domains.Tasks.Task task, CancellationToken cancellationToken)
        {
            var dbCompetitions = _competitionDbContext.Competitions
                .Where(t => task.Competitions.Select(x => x.CompetitionId).Contains(t.CompetitionId)).ToList();

            var dbTaskResults = _competitionDbContext.TaskResult
                .Where(t => task.TaskResults.Select(x => x.TaskResultId).Contains(t.TaskResultId)).ToList();

            var entityTask = _mapper.Map<TaskDbModel>(task);
            entityTask.Competitions = dbCompetitions;
            entityTask.TaskResults = dbTaskResults;

            await _competitionDbContext.Tasks.AddAsync(entityTask, cancellationToken);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var dbTask = await _competitionDbContext.Tasks
                .FirstOrDefaultAsync(it => it.TaskId == id, cancellationToken);

            if (dbTask == null)
            {
                throw new ObjectNotFoundException($"Ошибка удаления! " +
                                                  $"Объект Task c Id = {id} не найден!");
            }

            _competitionDbContext.Tasks.Remove(dbTask);
        }

        public Task<List<Core.Domains.Tasks.Task>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _competitionDbContext.Tasks
                .Include(t => t.Competitions)
                .Include(t => t.TaskResults)
                .AsNoTracking()
                .Select(t => _mapper.Map<Core.Domains.Tasks.Task>(t))
                .ToListAsync(cancellationToken);
        }

        public async Task<Core.Domains.Tasks.Task> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var dbTask = await _competitionDbContext.Tasks
                .Include(t => t.Competitions)
                .Include(t => t.TaskResults)
                .AsNoTracking()
                .FirstOrDefaultAsync(it => it.TaskId == id, cancellationToken);

            if (dbTask == null)
            {
                throw new ObjectNotFoundException($"Ошибка получения! " +
                                                  $"Объект Task c Id = {id} не найден!");
            }

            return _mapper.Map<Core.Domains.Tasks.Task>(dbTask);
        }        

        public async System.Threading.Tasks.Task UpdateAsync(Core.Domains.Tasks.Task task, CancellationToken cancellationToken)
        {
            var dbTask = await _competitionDbContext.Tasks
                .Include(t => t.Competitions)
                .FirstOrDefaultAsync(it => it.TaskId == task.TaskId, cancellationToken);

            if (dbTask == null)
            {
                throw new ObjectNotFoundException($"Ошибка обновления! " +
                                                  $"Объект Task c Id = {task.TaskId} не найден!");
            }

            var dbCompetitions = _competitionDbContext.Competitions
                .Where(t => task.Competitions.Select(x => x.CompetitionId).Contains(t.CompetitionId)).ToList();

            var dbTaskResults = _competitionDbContext.TaskResult
                .Where(t => task.TaskResults.Select(x => x.TaskResultId).Contains(t.TaskResultId)).ToList();

            dbTask.Name = task.Name;
            dbTask.Text = task.Text;
            dbTask.Solution = task.Solution;
            dbTask.InputExample = task.InputExample;
            dbTask.OutputExample = task.OutputExample;
            dbTask.Points = task.Points;
            dbTask.Competitions = dbCompetitions;
            dbTask.TaskResults = dbTaskResults;
        }

        public async Task<List<Core.Domains.Tasks.Task>> GetByNameAsync(string namePart, CancellationToken cancellationToken)
        {
            var foundTasks = await _competitionDbContext.Tasks
                .Where(task => task.Name.ToLower().Contains(namePart.ToLower()))
                .Include(t => t.Competitions)
                .Include(t => t.TaskResults)
                .AsNoTracking()
                .Select(task => _mapper.Map<Core.Domains.Tasks.Task>(task))
                .ToListAsync(cancellationToken);

            return foundTasks;
        }
    }
}
