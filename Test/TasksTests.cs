using CompetitionManager.Core.Domains.Tasks.Repositories;
using CompetitionManager.Core.Domains.Tasks.Services;
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
    public class TasksTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ITaskRepository> _mockTaskRepository;
        private ITaskService _taskService;

        public TasksTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockTaskRepository = new Mock<ITaskRepository>();
            _taskService = new TaskService(_mockTaskRepository.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateAsync_SuccessCreating_ShouldVerifyUnitOfWork()
        {
            Domains.Tasks.Task expectedTask = new Domains.Tasks.Task()
            {
                Name = "TestTask",
                Text = "some text",
                Solution = "some solution",
                InputExample = "sample",
                OutputExample = "out",
                Points = 1,
                Competitions = new List<Domains.Competitions.Competition>(),
                TaskResults = new List<Domains.TaskResults.TaskResult>()

            };

            _mockTaskRepository
                .Setup(x => x
                    .CreateAsync(
                        It.IsAny<Domains.Tasks.Task>(),
                        It.IsAny<CancellationToken>()));

            await _taskService.CreateAsync(expectedTask, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenTaskDoesExist_ShouldReturnEntity()
        {
            Domains.Tasks.Task expectedTask = new Domains.Tasks.Task()
            {
                TaskId = 1,
                Name = "TestTask",
                Text = "some text",
                Solution = "some solution",
                InputExample = "sample",
                OutputExample = "out",
                Points = 1,
                Competitions = new List<Domains.Competitions.Competition>(),
                TaskResults = new List<Domains.TaskResults.TaskResult>()
            };

            _mockTaskRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTask);

            Domains.Tasks.Task createdTask = await _taskService.GetByIdAsync(expectedTask.TaskId, new CancellationToken(false));

            Assert.Equal(expectedTask.TaskId, createdTask.TaskId);
        }

        [Fact]
        public async Task GetByIdAsync_WhenTaskDoesNotExist_ShouldThrowException()
        {
            Domains.Tasks.Task expectedTask = new Domains.Tasks.Task()
            {
                TaskId = 1,
                Name = "TestTask",
                Text = "some text",
                Solution = "some solution",
                InputExample = "sample",
                OutputExample = "out",
                Points = 1,
                Competitions = new List<Domains.Competitions.Competition>(),
                TaskResults = new List<Domains.TaskResults.TaskResult>()
            };

            _mockTaskRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка получения!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _taskService.GetByIdAsync(expectedTask.TaskId, new CancellationToken(false)));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task GetAllAsync_WhenTasksDoesExist_ShouldReturnEntity()
        {
            List<Domains.Tasks.Task> expectedTasks = new List<Domains.Tasks.Task>()
            {
                new Domains.Tasks.Task()
                {
                    TaskId = 1,
                    Name = "TestTask",
                    Text = "some text",
                    Solution = "some solution",
                    InputExample = "sample",
                    OutputExample = "out",
                    Points = 1,
                    Competitions = new List<Domains.Competitions.Competition>(),
                    TaskResults = new List<Domains.TaskResults.TaskResult>()
                },
                new Domains.Tasks.Task()
                {
                    TaskId = 2,
                    Name = "TestTask1",
                    Text = "some another text",
                    Solution = "some solution1",
                    InputExample = "sample1",
                    OutputExample = "out1",
                    Points = 2,
                    Competitions = new List<Domains.Competitions.Competition>(),
                    TaskResults = new List<Domains.TaskResults.TaskResult>()
                }
        };

            _mockTaskRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTasks);

            List<Domains.Tasks.Task> createdTasks = await _taskService.GetAllAsync(new CancellationToken(false));

            Assert.Equal(expectedTasks.Count, createdTasks.Count);
        }

        [Fact]
        public async Task GetAllAsync_WhenTasksDoesNotExist_ShouldReturnEmptyList()
        {
            _mockTaskRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Domains.Tasks.Task>());

            List<Domains.Tasks.Task> createdTasks = await _taskService.GetAllAsync(new CancellationToken(false));

            Assert.Empty(createdTasks);
        }

        public async Task UpdateAsync_WhenTaskDoesExist_ShouldVerifyUnitOfWork()
        {
            Domains.Tasks.Task expectedTask = new Domains.Tasks.Task()
            {
                TaskId = 1,
                Name = "TestTask",
                Text = "some text",
                Solution = "some solution",
                InputExample = "sample",
                OutputExample = "out",
                Points = 1,
                Competitions = new List<Domains.Competitions.Competition>(),
                TaskResults = new List<Domains.TaskResults.TaskResult>()
            };

            _mockTaskRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<Domains.Tasks.Task>(),
                        It.IsAny<CancellationToken>()));

            await _taskService.UpdateAsync(expectedTask, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenTaskDoesNotExist_ShouldThrowException()
        {
            Domains.Tasks.Task expectedTask = new Domains.Tasks.Task()
            {
                TaskId = 1,
                Name = "TestTask",
                Text = "some text",
                Solution = "some solution",
                InputExample = "sample",
                OutputExample = "out",
                Points = 1,
                Competitions = new List<Domains.Competitions.Competition>(),
                TaskResults = new List<Domains.TaskResults.TaskResult>()
            };

            _mockTaskRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<Domains.Tasks.Task>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка обновления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _taskService.UpdateAsync(expectedTask, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task DeleteAsync_WhenTaskDoesExist_ShouldVerifyUnitOfWork()
        {
            Domains.Tasks.Task expectedTask = new Domains.Tasks.Task()
            {
                TaskId = 1,
                Name = "TestTask",
                Text = "some text",
                Solution = "some solution",
                InputExample = "sample",
                OutputExample = "out",
                Points = 1,
                Competitions = new List<Domains.Competitions.Competition>(),
                TaskResults = new List<Domains.TaskResults.TaskResult>()
            };

            _mockTaskRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()));

            await _taskService.DeleteAsync(expectedTask.TaskId, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenTaskDoesNotExist_ShouldThrowException()
        {
            int deletingId = 1;

            _mockTaskRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка удаления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _taskService.DeleteAsync(deletingId, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }
    }
}
