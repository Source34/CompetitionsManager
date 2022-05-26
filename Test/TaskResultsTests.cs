using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.TaskResults.Repositories;
using CompetitionManager.Core.Domains.TaskResults.Services;
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
    public class TaskResultsTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ITaskResultRepository> _mockTaskResultRepository;
        private ITaskResultService _taskResultService;

        public TaskResultsTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockTaskResultRepository = new Mock<ITaskResultRepository>();
            _taskResultService = new TaskResultService(_mockTaskResultRepository.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateAsync_SuccessCreating_ShouldVerifyUnitOfWork()
        {
            TaskResult expectedResult = new TaskResult()
            {
                Competition = new Domains.Competitions.Competition(),
                Team = new Domains.Teams.Team(),
                Task = new Domains.Tasks.Task(),
                ElapsedMinutes = 60,
                Result = 1
            };

            _mockTaskResultRepository
                .Setup(x => x
                    .CreateAsync(
                        It.IsAny<TaskResult>(),
                        It.IsAny<CancellationToken>()));

            await _taskResultService.CreateAsync(expectedResult, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenTaskResultDoesExist_ShouldReturnEntity()
        {
            TaskResult expectedResult = new TaskResult()
            {
                TaskResultId = 1,
                Competition = new Domains.Competitions.Competition(),
                Team = new Domains.Teams.Team(),
                Task = new Domains.Tasks.Task(),
                ElapsedMinutes = 60,
                Result = 1
            };

            _mockTaskResultRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            TaskResult createdResult = await _taskResultService.GetByIdAsync(expectedResult.TaskResultId, new CancellationToken(false));

            Assert.Equal(expectedResult.TaskResultId, createdResult.TaskResultId);
        }

        [Fact]
        public async Task GetByIdAsync_WhenTaskResultDoesNotExist_ShouldThrowException()
        {
            TaskResult expectedResult = new TaskResult()
            {
                TaskResultId = 1,
                Competition = new Domains.Competitions.Competition(),
                Team = new Domains.Teams.Team(),
                Task = new Domains.Tasks.Task(),
                ElapsedMinutes = 60,
                Result = 1
            };

            _mockTaskResultRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка получения!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _taskResultService.GetByIdAsync(expectedResult.TaskResultId, new CancellationToken(false)));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task GetAllAsync_WhenTaskResultsDoesExist_ShouldReturnEntity()
        {
            List<TaskResult> expectedResults = new List<TaskResult>()
            {
                new TaskResult()
                {
                    Competition = new Domains.Competitions.Competition(),
                    Team = new Domains.Teams.Team(),
                    Task = new Domains.Tasks.Task(),
                    ElapsedMinutes = 60,
                    Result = 1
                },
                new TaskResult()
                {
                    Competition = new Domains.Competitions.Competition(),
                    Team = new Domains.Teams.Team(),
                    Task = new Domains.Tasks.Task(),
                    ElapsedMinutes = 60,
                    Result = 1
                }
            };

            _mockTaskResultRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResults);

            List<TaskResult> createdResults = await _taskResultService.GetAllAsync(new CancellationToken(false));

            Assert.Equal(expectedResults.Count, createdResults.Count);
        }

        [Fact]
        public async Task GetAllAsync_WhenTaskResultsDoesNotExist_ShouldReturnEmptyList()
        {
            _mockTaskResultRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<TaskResult>());

            List<TaskResult> createdResults = await _taskResultService.GetAllAsync(new CancellationToken(false));

            Assert.Empty(createdResults);
        }

        public async Task UpdateAsync_WhenTaskResultDoesExist_ShouldVerifyUnitOfWork()
        {
            TaskResult expectedResult = new TaskResult()
            {
                TaskResultId = 1,
                Competition = new Domains.Competitions.Competition(),
                Team = new Domains.Teams.Team(),
                Task = new Domains.Tasks.Task(),
                ElapsedMinutes = 60,
                Result = 1
            };

            _mockTaskResultRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<TaskResult>(),
                        It.IsAny<CancellationToken>()));

            await _taskResultService.UpdateAsync(expectedResult, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenTaskResultDoesNotExist_ShouldThrowException()
        {
            TaskResult expectedResult = new TaskResult()
            {
                TaskResultId = 1,
                Competition = new Domains.Competitions.Competition(),
                Team = new Domains.Teams.Team(),
                Task = new Domains.Tasks.Task(),
                ElapsedMinutes = 60,
                Result = 1
            };

            _mockTaskResultRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<TaskResult>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка обновления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _taskResultService.UpdateAsync(expectedResult, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task DeleteAsync_WhenTaskResultDoesExist_ShouldVerifyUnitOfWork()
        {
            TaskResult expectedResult = new TaskResult()
            {
                TaskResultId = 1,
                Competition = new Domains.Competitions.Competition(),
                Team = new Domains.Teams.Team(),
                Task = new Domains.Tasks.Task(),
                ElapsedMinutes = 60,
                Result = 1
            };

            _mockTaskResultRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()));

            await _taskResultService.DeleteAsync(expectedResult.TaskResultId, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenTaskResultDoesNotExist_ShouldThrowException()
        {
            int deletingId = 1;

            _mockTaskResultRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка удаления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _taskResultService.DeleteAsync(deletingId, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }
    }
}
