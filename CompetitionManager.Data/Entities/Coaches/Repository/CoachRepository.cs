using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CompetitionManager.Data.Contexts;
using CompetitionManager.Core.Exceptions;
using CompetitionManager.Core.Domains.Coaches;
using CompetitionManager.Core.Domains.Coaches.Repositories;

namespace CompetitionManager.Data.Entities.Coaches.Repository
{
    public class CoachRepository : ICoachRepository
    {
        private readonly CompetitionDbContext _competitionDbContext;
        private readonly IMapper _mapper;

        public CoachRepository(CompetitionDbContext competitionDbContext, IMapper mapper)
        {
            _competitionDbContext = competitionDbContext;
            _mapper = mapper;
        }

        public async Task<Coach> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var dbCoach = await _competitionDbContext.Coaches
                .Include(coach => coach.TrainingTeams)
                .AsNoTracking()
                .FirstOrDefaultAsync(it => it.CoachId == id, cancellationToken);

            if (dbCoach == null)
            {
                throw new ObjectNotFoundException($"Ошибка получения! " +
                                                  $"Объект Coach c Id = {id} не найден!");
            }

            return _mapper.Map<Coach>(dbCoach);
        }

        public Task<List<Coach>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _competitionDbContext.Coaches
                .Include(coach => coach.TrainingTeams)
                .AsNoTracking()
                .Select(user => _mapper.Map<Coach>(user))                
                .ToListAsync(cancellationToken);
        }

        public async Task CreateAsync(Coach coach, CancellationToken cancellationToken)
        {
            var trainingTeams = _competitionDbContext.Teams
                .Where(t => coach.TrainingTeams.Select(x => x.TeamId).Contains(t.TeamId)).ToList();

            var entityCoach = _mapper.Map<CoachDbModel>(coach);
            entityCoach.TrainingTeams = trainingTeams;
            await _competitionDbContext.Coaches.AddAsync(entityCoach, cancellationToken);
        }

        public async Task UpdateAsync(Coach coach, CancellationToken cancellationToken)
        {
            var dbCoach = await _competitionDbContext.Coaches
                .FirstOrDefaultAsync(it => it.CoachId == coach.CoachId, cancellationToken);

            if (dbCoach == null)
            {
                throw new ObjectNotFoundException($"Ошибка обновления! " +
                                                  $"Объект Coach c Id = {coach.CoachId} не найден!");
            }

            var trainingTeams = _competitionDbContext.Teams
                .Where(t => coach.TrainingTeams.Select(x => x.TeamId).Contains(t.TeamId)).ToList();

            dbCoach.FirstName = coach.FirstName;
            dbCoach.Surname = coach.Surname;
            dbCoach.Patronymic = coach.Patronymic;
            dbCoach.AcademicDegree = coach.AcademicDegree;
            dbCoach.CodeforcesLogin = coach.CodeforcesLogin;
            dbCoach.University = coach.University;
            dbCoach.TrainingTeams = trainingTeams;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var dbUser = await _competitionDbContext.Coaches
                .FirstOrDefaultAsync(it => it.CoachId == id, cancellationToken);

            if (dbUser == null)
            {
                throw new ObjectNotFoundException($"Ошибка удаления! " +
                                                  $"Объект Coach c Id = {id} не найден!");
            }

            _competitionDbContext.Coaches.Remove(dbUser);
        }
    }
}
