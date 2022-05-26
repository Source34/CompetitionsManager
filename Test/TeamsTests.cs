using CompetitionManager.Core.Domains.Competitions.Repositories;
using CompetitionManager.Core.Domains.Teams;
using CompetitionManager.Core.Domains.Teams.Repositories;
using CompetitionManager.Core.Domains.Teams.Services;
using CompetitionManager.Core.Exceptions;
using CompetitionManager.Core.Seriveces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CompetitionManager.Core.Tests
{
    public class TeamsTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ITeamRepository> _mockTeamRepository;
        private Mock<ICompetitionRepository> _competitionRepository;
        private ITeamService _teamService;

        public TeamsTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockTeamRepository = new Mock<ITeamRepository>();
            _competitionRepository = new Mock<ICompetitionRepository>();
            _teamService = new TeamService(_mockTeamRepository.Object, _competitionRepository.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateAsync_SuccessCreating_ShouldVerifyUnitOfWork()
        {
            Team expectedTeam = new Team()
            {
                Name = "TestName",
                TeamLead = new Domains.Users.User(),
                Coach = new Domains.Coaches.Coach(),
                TeamMates = new List<Domains.Users.User>(),
                Results = new List<Domains.TaskResults.TaskResult>(),
                Competitions = new List<Domains.Competitions.Competition>()
            };

            _mockTeamRepository
                .Setup(x => x
                    .CreateAsync(
                        It.IsAny<Team>(),
                        It.IsAny<CancellationToken>()));

            await _teamService.CreateAsync(expectedTeam, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenTeamDoesExist_ShouldReturnEntity()
        {
            Team expectedTeam = new Team()
            {
                TeamId = 1,
                Name = "TestName",
                TeamLead = new Domains.Users.User(),
                Coach = new Domains.Coaches.Coach(),
                TeamMates = new List<Domains.Users.User>(),
                Results = new List<Domains.TaskResults.TaskResult>(),
                Competitions = new List<Domains.Competitions.Competition>()
            };

            _mockTeamRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTeam);

            Team createdTeam = await _teamService.GetByIdAsync(expectedTeam.TeamId, new CancellationToken(false));

            Assert.Equal(expectedTeam.TeamId, createdTeam.TeamId);
        }

        [Fact]
        public async Task GetByIdAsync_WhenTeamDoesNotExist_ShouldThrowException()
        {
            Team expectedTeam = new Team()
            {
                TeamId = 1,
                Name = "TestName",
                TeamLead = new Domains.Users.User(),
                Coach = new Domains.Coaches.Coach(),
                TeamMates = new List<Domains.Users.User>(),
                Results = new List<Domains.TaskResults.TaskResult>(),
                Competitions = new List<Domains.Competitions.Competition>()
            };

            _mockTeamRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка получения!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _teamService.GetByIdAsync(expectedTeam.TeamId, new CancellationToken(false)));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task GetAllAsync_WhenTeamsDoesExist_ShouldReturnEntity()
        {
            List<Team> expectedTeams = new List<Team>()
            {
                new Team()
                {
                    TeamId = 1,
                    Name = "TestName",
                    TeamLead = new Domains.Users.User(),
                    Coach = new Domains.Coaches.Coach(),
                    TeamMates = new List<Domains.Users.User>(),
                    Results = new List<Domains.TaskResults.TaskResult>(),
                    Competitions = new List<Domains.Competitions.Competition>()
                },
                new Team()
                {
                    TeamId = 2,
                    Name = "TestName1",
                    TeamLead = new Domains.Users.User(),
                    Coach = new Domains.Coaches.Coach(),
                    TeamMates = new List<Domains.Users.User>(),
                    Results = new List<Domains.TaskResults.TaskResult>(),
                    Competitions = new List<Domains.Competitions.Competition>()
                }
        };

            _mockTeamRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTeams);

            List<Team> createdTeams = await _teamService.GetAllAsync(new CancellationToken(false));

            Assert.Equal(expectedTeams.Count, createdTeams.Count);
        }

        [Fact]
        public async Task GetAllAsync_WhenTeamsDoesNotExist_ShouldReturnEmptyList()
        {
            _mockTeamRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Team>());

            List<Team> createdTeams = await _teamService.GetAllAsync(new CancellationToken(false));

            Assert.Empty(createdTeams);
        }

        public async Task UpdateAsync_WhenTeamDoesExist_ShouldVerifyUnitOfWork()
        {
            Team expectedTeam = new Team()
            {
                TeamId = 1,
                Name = "TestName",
                TeamLead = new Domains.Users.User(),
                Coach = new Domains.Coaches.Coach(),
                TeamMates = new List<Domains.Users.User>(),
                Results = new List<Domains.TaskResults.TaskResult>(),
                Competitions = new List<Domains.Competitions.Competition>()
            };

            _mockTeamRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<Team>(),
                        It.IsAny<CancellationToken>()));

            await _teamService.UpdateAsync(expectedTeam, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenTeamDoesNotExist_ShouldThrowException()
        {
            Team expectedTeam = new Team()
            {
                TeamId = 1,
                Name = "TestName",
                TeamLead = new Domains.Users.User(),
                Coach = new Domains.Coaches.Coach(),
                TeamMates = new List<Domains.Users.User>(),
                Results = new List<Domains.TaskResults.TaskResult>(),
                Competitions = new List<Domains.Competitions.Competition>()
            };

            _mockTeamRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<Team>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка обновления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _teamService.UpdateAsync(expectedTeam, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task DeleteAsync_WhenTeamDoesExist_ShouldVerifyUnitOfWork()
        {
            Team expectedTeam = new Team()
            {
                TeamId = 1,
                Name = "TestName",
                TeamLead = new Domains.Users.User(),
                Coach = new Domains.Coaches.Coach(),
                TeamMates = new List<Domains.Users.User>(),
                Results = new List<Domains.TaskResults.TaskResult>(),
                Competitions = new List<Domains.Competitions.Competition>()
            };

            _mockTeamRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()));

            await _teamService.DeleteAsync(expectedTeam.TeamId, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenTeamDoesNotExist_ShouldThrowException()
        {
            int deletingId = 1;

            _mockTeamRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка удаления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _teamService.DeleteAsync(deletingId, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }
    }
}
