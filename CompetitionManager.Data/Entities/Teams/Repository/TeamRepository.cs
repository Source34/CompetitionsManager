using AutoMapper;
using CompetitionManager.Core.Domains.Teams;
using CompetitionManager.Core.Domains.Teams.Repositories;
using CompetitionManager.Core.Exceptions;
using CompetitionManager.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CompetitionManager.Data.Entities.Teams.Repository
{
    internal class TeamRepository : ITeamRepository
    {
        private readonly CompetitionDbContext _competitionDbContext;
        private readonly IMapper _mapper;

        public TeamRepository(CompetitionDbContext competitionDbContext, IMapper mapper)
        {
            _competitionDbContext = competitionDbContext;
            _mapper = mapper;
        }

        public async Task<Team> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var dbCoach = await _competitionDbContext.Teams
                .AsNoTracking()
                .Include(x => x.Coach)
                .Include(x => x.TeamMates)
                .Include(x => x.TeamLead)
                .Include(t => t.Results)
                    .ThenInclude(t => t.Competition)
                .Include(t => t.Results)
                    .ThenInclude(t => t.Task)
                .Include(t => t.Competitions)
                .FirstOrDefaultAsync(it => it.TeamId == id, cancellationToken);

            if (dbCoach == null)
            {
                throw new ObjectNotFoundException($"Ошибка получения! " +
                                                  $"Объект Team c Id = {id} не найден!");
            }

            return _mapper.Map<Team>(dbCoach);
        }

        public Task<List<Team>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _competitionDbContext.Teams
                .Include(x => x.Coach)
                .Include(x => x.TeamMates)
                .Include(x => x.TeamLead)
                .Include(t => t.Results)
                .Include(t => t.Competitions)
                .AsNoTracking()
                .Select(team => _mapper.Map<Team>(team))
                .ToListAsync(cancellationToken);
        }

        public async Task CreateAsync(Team team, CancellationToken cancellationToken)
        {            
            var dbCoach = await _competitionDbContext.Coaches.FirstOrDefaultAsync(it => it.CoachId == team.Coach.CoachId, cancellationToken);
            if (dbCoach == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbCoach exception!");

            var dbTeamLead = await _competitionDbContext.Users.FirstOrDefaultAsync(it => it.UserId == team.TeamLead.UserId, cancellationToken);
            if (dbTeamLead == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbTeamLead exception!");

            var dbTeamMates = _competitionDbContext.Users.ToList().Where(it => team.TeamMates.Any(p => p.UserId == it.UserId)).ToList();
            if (dbTeamMates == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbTeamMates exception!");

            var dbResults = _competitionDbContext.TaskResult.ToList().Where(it => team.Results.Any(p => p.TaskResultId == it.TaskResultId)).ToList();
            if (dbResults == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbResults exception!");

            var dbCompetitions = _competitionDbContext.Competitions.ToList().Where(it => team.Competitions.Any(p => p.CompetitionId == it.CompetitionId)).ToList();
            if (dbCompetitions == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbCompetitions exception!");

            var entity = _mapper.Map<TeamDbModel>(team);
            entity.Coach = dbCoach;
            entity.TeamMates = dbTeamMates;
            entity.TeamLead = dbTeamLead;
            entity.Results = dbResults;
            entity.Competitions = dbCompetitions;

            await _competitionDbContext.Teams.AddAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(Team team, CancellationToken cancellationToken)
        {
            var dbTeam = await _competitionDbContext.Teams
                .Include(t => t.TeamMates)
                .Include(t => t.Competitions)
                .FirstOrDefaultAsync(it => it.TeamId == team.TeamId, cancellationToken);

            if (dbTeam == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " +
                                                  $"Объект Team c Id = {team.TeamId} не найден!");
            var dbCoach = await _competitionDbContext.Coaches.FirstOrDefaultAsync(it => it.CoachId == team.Coach.CoachId, cancellationToken);
            if (dbCoach == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbCoach exception!");

            var dbTeamLead = await _competitionDbContext.Users.FirstOrDefaultAsync(it => it.UserId == team.TeamLead.UserId, cancellationToken);
            if (dbTeamLead == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbTeamLead exception!");

            var dbTeamMates = _competitionDbContext.Users.ToList().Where(it => team.TeamMates.Any(p => p.UserId == it.UserId)).ToList();
            if (dbTeamMates == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbTeamMates exception!");

            var dbResults = _competitionDbContext.TaskResult.ToList().Where(it => team.Results.Any(p => p.TaskResultId == it.TaskResultId)).ToList();
            if (dbResults == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbResults exception!");

            var dbCompetitions = _competitionDbContext.Competitions.ToList().Where(it => team.Competitions.Any(p => p.CompetitionId == it.CompetitionId)).ToList();
            if (dbCompetitions == null)
                throw new ObjectNotFoundException($"Ошибка обновления! " + $"dbCompetitions exception!");

            dbTeam.Name = team.Name;
            dbTeam.Coach = dbCoach;
            dbTeam.TeamLead = dbTeamLead;
            dbTeam.TeamMates = dbTeamMates;
            dbTeam.Results = dbResults;
            dbTeam.Competitions = dbCompetitions;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var dbTeam = await _competitionDbContext.Teams
                .FirstOrDefaultAsync(it => it.TeamId == id, cancellationToken);

            if (dbTeam == null)
            {
                throw new ObjectNotFoundException($"Ошибка удаления! " +
                                                  $"Объект Team c Id = {id} не найден!");
            }

            _competitionDbContext.Teams.Remove(dbTeam);
        }

        public Task<List<Team>> GetByNameAsync(string namePart, CancellationToken cancellationToken)
        {
            return _competitionDbContext.Teams
                .Where(team => team.Name.ToLower().Contains(namePart.ToLower()))
                .Include(x => x.Coach)
                .Include(x => x.TeamMates)
                .Include(x => x.TeamLead)
                .Include(t => t.Results)
                .Include(t => t.Competitions)
                .AsNoTracking()
                .Select(team => _mapper.Map<Team>(team))
                .ToListAsync(cancellationToken);
        }
    }
}
