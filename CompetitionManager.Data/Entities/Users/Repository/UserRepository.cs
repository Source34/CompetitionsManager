using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CompetitionManager.Data.Contexts;
using CompetitionManager.Core.Exceptions;
using CompetitionManager.Core.Domains.Users;
using CompetitionManager.Core.Domains.Users.Repositories;
using System.Text.RegularExpressions;

namespace CompetitionManager.Data.Entities.Users.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly CompetitionDbContext _competitionDbContext;
        private readonly IMapper _mapper;

        public UserRepository(CompetitionDbContext competitionDbContext, IMapper mapper)
        {
            _competitionDbContext = competitionDbContext;
            _mapper = mapper;
        }
        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var dbUser = await _competitionDbContext.Users
                .Include(u => u.TeamsMemberships).ThenInclude(x => x.Results)
                .Include(u => u.LeadedTeams).ThenInclude(x => x.Results)
                .AsNoTracking()
                .FirstOrDefaultAsync(it => it.UserId == id, cancellationToken);

            if (dbUser == null)
            {
                throw new ObjectNotFoundException($"Ошибка получения! " +
                                                  $"Объект User c Id = {id} не найден!");
            }

            return _mapper.Map<User>(dbUser);
        }

        public Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _competitionDbContext.Users
                .Include(u => u.TeamsMemberships)
                .Include(u => u.LeadedTeams)
                .AsNoTracking()
                .Select(user => _mapper.Map<User>(user))
                .ToListAsync(cancellationToken);
        }

        public async Task CreateAsync(User user, CancellationToken cancellationToken)
        {
            await _competitionDbContext.Users.AddAsync(_mapper.Map<UserDbModel>(user), cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var dbUser = await _competitionDbContext.Users
                .Include(u => u.TeamsMemberships)
                .FirstOrDefaultAsync(it => it.UserId == user.UserId, cancellationToken);

            if (dbUser == null)
            {
                throw new ObjectNotFoundException($"Ошибка обновления! " +
                                                  $"Объект User c Id = {user.UserId} не найден!");
            }

            var dbTeamsMembership = _competitionDbContext.Teams
                .Where(t => user.TeamsMemberships.Select(x => x.TeamId).Contains(t.TeamId)).ToList();

            var dbLeadedTeams = _competitionDbContext.Teams
                .Where(t => user.LeadedTeams.Select(x => x.TeamId).Contains(t.TeamId)).ToList();

            dbUser.FirstName = user.FirstName;
            dbUser.Surname = user.Surname;
            dbUser.Patronymic = user.Patronymic;
            dbUser.University = user.University;
            dbUser.FromVstu = user.FromVstu;
            dbUser.CodeforcesLogin = dbUser.CodeforcesLogin;
            dbUser.TeamsMemberships = dbTeamsMembership;
            dbUser.LeadedTeams = dbLeadedTeams;
            dbUser.GradebookNumber = user.GradebookNumber;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var dbUser = await _competitionDbContext.Users
                .FirstOrDefaultAsync(it => it.UserId == id, cancellationToken);

            if (dbUser == null)
            {
                throw new ObjectNotFoundException($"Ошибка удаления! " +
                                                  $"Объект User c Id = {id} не найден!");
            }

            _competitionDbContext.Users.Remove(dbUser);
        }

        public Task<List<User>> GetByFioAsync(string fio, CancellationToken cancellationToken)
        {
            Regex trimmer = new Regex(@"\s\s+");
            var fioParts = trimmer
                .Replace(fio, " ").Trim()
                .Split(' ')
                .Select(part => part.ToLower())
                .ToList();

            // условие ниже вызывает ошибку
            // .Where(user => fioParts.Any(fioPart => user.FirstName.ToLower().Contains(fioPart)))

            List<User> foundUsers = new List<User>();
            foreach(var fioPart in fioParts)
            {
                foundUsers.AddRange(_competitionDbContext.Users
                .Where(user => 
                    user.FirstName.ToLower().Contains(fioPart) || 
                    user.Surname.ToLower().Contains(fioPart) || 
                    user.Patronymic.ToLower().Contains(fioPart))
                .Include(u => u.TeamsMemberships)
                .Include(u => u.LeadedTeams)
                .AsNoTracking()
                .Select(user => _mapper.Map<User>(user))
                .ToList());
            }

            return Task.FromResult(foundUsers.DistinctBy(x => x.UserId).ToList());
        }
    }
}
