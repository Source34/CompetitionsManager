using CompetitionManager.Core.Domains.Coaches;
using CompetitionManager.Core.Domains.Coaches.Repositories;
using CompetitionManager.Core.Domains.Coaches.Services;
using CompetitionManager.Core.Domains.Teams;
using CompetitionManager.Core.Exceptions;
using CompetitionManager.Core.Seriveces;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CompetitionManager.Core.Tests
{
    public class CoachTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ICoachRepository> _mockCoachRepository;
        private ICoachService _coachService;

        public CoachTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCoachRepository = new Mock<ICoachRepository>();
            _coachService = new CoachService(_mockCoachRepository.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateAsync_SuccessCreating_ShouldVerifyUnitOfWork()
        {
            Coach expectedCoach = new Coach()
            {
                CodeforcesLogin = "TestLogin",
                FirstName = "Vasiliy",
                Surname = "Ivanov",
                Patronymic = "Petrovich",
                AcademicDegree = "student",
                University = "VSTU",
                TrainingTeams = new List<Team>()
            };            

            _mockCoachRepository
                .Setup(x => x
                    .CreateAsync(
                        It.IsAny<Coach>(),
                        It.IsAny<CancellationToken>()));

            await _coachService.CreateAsync(expectedCoach, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCouchDoesExist_ShouldReturnEntity()
        {
            Coach expectedCoach = new Coach()
            {
                CoachId = 1,
                CodeforcesLogin = "TestLogin",
                FirstName = "Vasiliy",
                Surname = "Ivanov",
                Patronymic = "Petrovich",
                AcademicDegree = "student",
                University = "VSTU",
                TrainingTeams = new List<Team>()
            };

            _mockCoachRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCoach);

            Coach createdCoach = await _coachService.GetByIdAsync(expectedCoach.CoachId, new CancellationToken(false));

            Assert.Equal(expectedCoach.CoachId, createdCoach.CoachId);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCouchDoesNotExist_ShouldThrowException()
        {
            Coach expectedCoach = new Coach()
            {
                CoachId = 1,
                CodeforcesLogin = "TestLogin",
                FirstName = "Vasiliy",
                Surname = "Ivanov",
                Patronymic = "Petrovich",
                AcademicDegree = "student",
                University = "VSTU",
                TrainingTeams = new List<Team>()
            };

            _mockCoachRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка получения!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _coachService.GetByIdAsync(expectedCoach.CoachId, new CancellationToken(false)));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task GetAllAsync_WhenCouchsDoesExist_ShouldReturnEntity()
        {
            List<Coach> expectedCoaches = new List<Coach>()
            {
                new Coach()
                {
                    CoachId = 1,
                    CodeforcesLogin = "TestLogin",
                    FirstName = "Vasiliy",
                    Surname = "Ivanov",
                    Patronymic = "Petrovich",
                    AcademicDegree = "student",
                    University = "VSTU",
                    TrainingTeams = new List<Team>()
                },
                new Coach()
                {
                    CodeforcesLogin = "TestLogin1",
                    FirstName = "Vasiliy1",
                    Surname = "Ivanov1",
                    Patronymic = "Petrovich1",
                    AcademicDegree = "student1",
                    University = "VSTU1",
                    TrainingTeams = new List<Team>()
                }
        };

            _mockCoachRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCoaches);

            List<Coach> createdCoaches = await _coachService.GetAllAsync(new CancellationToken(false));

            Assert.Equal(expectedCoaches.Count, createdCoaches.Count);
        }

        [Fact]
        public async Task GetAllAsync_WhenCouchsDoesNotExist_ShouldReturnEmptyList()
        {
            _mockCoachRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Coach>());

            List<Coach> createdCoaches = await _coachService.GetAllAsync(new CancellationToken(false));

            Assert.Empty(createdCoaches);
        }        

        public async Task UpdateAsync_WhenCouchDoesExist_ShouldVerifyUnitOfWork()
        {
            Coach expectedCoach = new Coach()
            {
                CoachId = 1,
                CodeforcesLogin = "TestLogin",
                FirstName = "Vasiliy",
                Surname = "Ivanov",
                Patronymic = "Petrovich",
                AcademicDegree = "student",
                University = "VSTU",
                TrainingTeams = null
            };

            _mockCoachRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<Coach>(),
                        It.IsAny<CancellationToken>()));

            await _coachService.UpdateAsync(expectedCoach, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenCouchDoesNotExist_ShouldThrowException()
        {
            Coach expectedCoach = new Coach()
            {
                CoachId = 1,
                CodeforcesLogin = "TestLogin",
                FirstName = "Vasiliy",
                Surname = "Ivanov",
                Patronymic = "Petrovich",
                AcademicDegree = "student",
                University = "VSTU",
                TrainingTeams = null
            };

            _mockCoachRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<Coach>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка обновления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _coachService.UpdateAsync(expectedCoach, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task DeleteAsync_WhenCouchDoesExist_ShouldVerifyUnitOfWork()
        {
            Coach coach = new Coach()
            {
                CoachId = 1,
                CodeforcesLogin = "TestLogin",
                FirstName = "Vasiliy",
                Surname = "Ivanov",
                Patronymic = "Petrovich",
                AcademicDegree = "student",
                University = "VSTU",
                TrainingTeams = null
            };

            _mockCoachRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()));

            await _coachService.DeleteAsync(coach.CoachId, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenCouchDoesNotExist_ShouldThrowException()
        {
            int deletingId = 1;

            _mockCoachRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка удаления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _coachService.DeleteAsync(deletingId, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }
    }
}