using CompetitionManager.Core.Domains.Competitions;
using CompetitionManager.Core.Domains.Competitions.Repositories;
using CompetitionManager.Core.Domains.Competitions.Services;
using CompetitionManager.Core.Exceptions;
using CompetitionManager.Core.Seriveces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompetitionManager.Core.Domains.Teams.Repositories;
using CompetitionManager.Core.Domains.Teams.Services;
using Xunit;

namespace CompetitionManager.Core.Tests
{
    public class CompetitionsTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ICompetitionRepository> _mockCompetitionRepository;
        private Mock<ITeamRepository> _mockTeamRepository;
        private ICompetitionService _competitionService;

        public CompetitionsTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCompetitionRepository = new Mock<ICompetitionRepository>();
            _mockTeamRepository = new Mock<ITeamRepository>();
            _competitionService = new CompetitionService(_mockCompetitionRepository.Object, _mockTeamRepository.Object,  _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateAsync_SuccessCreating_ShouldVerifyUnitOfWork()
        {
            Competition expectedCompetition = new Competition()
            {
                CompetitionName = "TestCompetition",
                Description = "some desribtion",
                StartEventDate = DateTime.Now,
                EndEventDate = DateTime.Now,
                Tasks = new List<Domains.Tasks.Task>(),
                Teams = new List<Domains.Teams.Team>(),
                Results = new List<Domains.TaskResults.TaskResult>()
            };

            _mockCompetitionRepository
                .Setup(x => x
                    .CreateAsync(
                        It.IsAny<Competition>(),
                        It.IsAny<CancellationToken>()));

            await _competitionService.CreateAsync(expectedCompetition, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCompetitionDoesExist_ShouldReturnEntity()
        {
            Competition expectedCompetition = new Competition()
            {
                CompetitionId = 1,
                CompetitionName = "TestCompetition",
                Description = "some desribtion",
                StartEventDate = DateTime.Now,
                EndEventDate = DateTime.Now,
                Tasks = new List<Domains.Tasks.Task>(),
                Teams = new List<Domains.Teams.Team>(),
                Results = new List<Domains.TaskResults.TaskResult>()
            };

            _mockCompetitionRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCompetition);

            Competition createdCompetition = await _competitionService.GetByIdAsync(expectedCompetition.CompetitionId, new CancellationToken(false));

            Assert.Equal(expectedCompetition.CompetitionId, createdCompetition.CompetitionId);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCompetitionDoesNotExist_ShouldThrowException()
        {
            Competition expectedCompetition = new Competition()
            {
                CompetitionId = 1,
                CompetitionName = "TestCompetition",
                Description = "some desribtion",
                StartEventDate = DateTime.Now,
                EndEventDate = DateTime.Now,
                Tasks = new List<Domains.Tasks.Task>(),
                Teams = new List<Domains.Teams.Team>(),
                Results = new List<Domains.TaskResults.TaskResult>()
            };

            _mockCompetitionRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка получения!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _competitionService.GetByIdAsync(expectedCompetition.CompetitionId, new CancellationToken(false)));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task GetAllAsync_WhenCompetitionsDoesExist_ShouldReturnEntity()
        {
            List<Competition> expectedCompetitions = new List<Competition>()
            {
                new Competition()
                {
                    CompetitionId = 1,
                    CompetitionName = "TestCompetition",
                    Description = "some desribtion",
                    StartEventDate = DateTime.Now,
                    EndEventDate = DateTime.Now,
                    Tasks = new List<Domains.Tasks.Task>(),
                    Teams = new List<Domains.Teams.Team>(),
                    Results = new List<Domains.TaskResults.TaskResult>()
                },
                new Competition()
                {
                    CompetitionId = 2,
                    CompetitionName = "TestCompetition1",
                    Description = "some another desribtion",
                    StartEventDate = DateTime.Now,
                    EndEventDate = DateTime.Now,
                    Tasks = new List<Domains.Tasks.Task>(),
                    Teams = new List<Domains.Teams.Team>(),
                    Results = new List<Domains.TaskResults.TaskResult>()
                }
            };

            _mockCompetitionRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCompetitions);

            List<Competition> createdCompetitions = await _competitionService.GetAllAsync(new CancellationToken(false));

            Assert.Equal(expectedCompetitions.Count, createdCompetitions.Count);
        }

        [Fact]
        public async Task GetAllAsync_WhenCompetitionsDoesNotExist_ShouldReturnEmptyList()
        {
            _mockCompetitionRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Competition>());

            List<Competition> createdCompetitions = await _competitionService.GetAllAsync(new CancellationToken(false));

            Assert.Empty(createdCompetitions);
        }

        public async Task UpdateAsync_WhenCompetitionDoesExist_ShouldVerifyUnitOfWork()
        {
            Competition expectedCompetition = new Competition()
            {
                CompetitionId = 1,
                CompetitionName = "TestCompetition",
                Description = "some desribtion",
                StartEventDate = DateTime.Now,
                EndEventDate = DateTime.Now,
                Tasks = new List<Domains.Tasks.Task>(),
                Teams = new List<Domains.Teams.Team>(),
                Results = new List<Domains.TaskResults.TaskResult>()
            };

            _mockCompetitionRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<Competition>(),
                        It.IsAny<CancellationToken>()));

            await _competitionService.UpdateAsync(expectedCompetition, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenCompetitionDoesNotExist_ShouldThrowException()
        {
            Competition expectedCompetition = new Competition()
            {
                CompetitionId = 1,
                CompetitionName = "TestCompetition",
                Description = "some desribtion",
                StartEventDate = DateTime.Now,
                EndEventDate = DateTime.Now,
                Tasks = new List<Domains.Tasks.Task>(),
                Teams = new List<Domains.Teams.Team>(),
                Results = new List<Domains.TaskResults.TaskResult>()
            };

            _mockCompetitionRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<Competition>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка обновления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _competitionService.UpdateAsync(expectedCompetition, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task DeleteAsync_WhenCompetitionDoesExist_ShouldVerifyUnitOfWork()
        {
            Competition competition = new Competition()
            {
                CompetitionId = 1,
                CompetitionName = "TestCompetition",
                Description = "some desribtion",
                StartEventDate = DateTime.Now,
                EndEventDate = DateTime.Now,
                Tasks = new List<Domains.Tasks.Task>(),
                Teams = new List<Domains.Teams.Team>(),
                Results = new List<Domains.TaskResults.TaskResult>()
            };

            _mockCompetitionRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()));

            await _competitionService.DeleteAsync(competition.CompetitionId, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenCompetitionDoesNotExist_ShouldThrowException()
        {
            int deletingId = 1;

            _mockCompetitionRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка удаления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _competitionService.DeleteAsync(deletingId, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }
    }
}
