using CompetitionManager.Core.Domains.Users;
using CompetitionManager.Core.Domains.Users.Repositories;
using CompetitionManager.Core.Domains.Users.Services;
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
    public class UserTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IUserRepository> _mockUserRepository;
        private IUserService _userService;

        public UserTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateAsync_SuccessCreating_ShouldVerifyUnitOfWork()
        {
            User expectedUser = new User()
            {
                FirstName = "Ivan",
                Surname = "Ivanov",
                Patronymic = "Ivanovich",
                CodeforcesLogin = "login",
                University = "VSTU",
                FromVstu = true,
                TeamsMemberships = new List<Domains.Teams.Team>(),
                LeadedTeams = new List<Domains.Teams.Team>()
            };

            _mockUserRepository
                .Setup(x => x
                    .CreateAsync(
                        It.IsAny<User>(),
                        It.IsAny<CancellationToken>()));

            await _userService.CreateAsync(expectedUser, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenUserDoesExist_ShouldReturnEntity()
        {
            User expectedUser = new User()
            {
                UserId = 1,
                FirstName = "Ivan",
                Surname = "Ivanov",
                Patronymic = "Ivanovich",
                CodeforcesLogin = "login",
                University = "VSTU",
                FromVstu = true,
                TeamsMemberships = new List<Domains.Teams.Team>(),
                LeadedTeams = new List<Domains.Teams.Team>()
            };

            _mockUserRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUser);

            User createdUser = await _userService.GetByIdAsync(expectedUser.UserId, new CancellationToken(false));

            Assert.Equal(expectedUser.UserId, createdUser.UserId);
        }

        [Fact]
        public async Task GetByIdAsync_WhenUserDoesNotExist_ShouldThrowException()
        {
            User expectedUser = new User()
            {
                UserId = 1,
                FirstName = "Ivan",
                Surname = "Ivanov",
                Patronymic = "Ivanovich",
                CodeforcesLogin = "login",
                University = "VSTU",
                FromVstu = true,
                TeamsMemberships = new List<Domains.Teams.Team>(),
                LeadedTeams = new List<Domains.Teams.Team>()
            };

            _mockUserRepository
                .Setup(x => x
                    .GetByIdAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка получения!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _userService.GetByIdAsync(expectedUser.UserId, new CancellationToken(false)));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task GetAllAsync_WhenUsersDoesExist_ShouldReturnEntity()
        {
            List<User> expectedUsers = new List<User>()
            {
                new User()
                {
                    UserId = 1,
                    FirstName = "Ivan",
                    Surname = "Ivanov",
                    Patronymic = "Ivanovich",
                    CodeforcesLogin = "login",
                    University = "VSTU",
                    FromVstu = true,
                    TeamsMemberships = new List<Domains.Teams.Team>(),
                    LeadedTeams = new List<Domains.Teams.Team>()
                },
                new User()
                {
                    UserId = 2,
                    FirstName = "Ivan1",
                    Surname = "Ivanov1",
                    Patronymic = "Ivanovich1",
                    CodeforcesLogin = "login1",
                    University = "VSTU",
                    FromVstu = true,
                    TeamsMemberships = new List<Domains.Teams.Team>(),
                    LeadedTeams = new List<Domains.Teams.Team>()
                }
        };

            _mockUserRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedUsers);

            List<User> createdUsers = await _userService.GetAllAsync(new CancellationToken(false));

            Assert.Equal(expectedUsers.Count, createdUsers.Count);
        }

        [Fact]
        public async Task GetAllAsync_WhenUsersDoesNotExist_ShouldReturnEmptyList()
        {
            _mockUserRepository
                .Setup(x => x
                    .GetAllAsync(
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<User>());

            List<User> createdUsers = await _userService.GetAllAsync(new CancellationToken(false));

            Assert.Empty(createdUsers);
        }

        public async Task UpdateAsync_WhenUserDoesExist_ShouldVerifyUnitOfWork()
        {
            User expectedUser = new User()
            {
                UserId = 1,
                FirstName = "Ivan",
                Surname = "Ivanov",
                Patronymic = "Ivanovich",
                CodeforcesLogin = "login",
                University = "VSTU",
                FromVstu = true,
                TeamsMemberships = new List<Domains.Teams.Team>(),
                LeadedTeams = new List<Domains.Teams.Team>()
            };

            _mockUserRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<User>(),
                        It.IsAny<CancellationToken>()));

            await _userService.UpdateAsync(expectedUser, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenUserDoesNotExist_ShouldThrowException()
        {
            User expectedUser = new User()
            {
                UserId = 1,
                FirstName = "Ivan",
                Surname = "Ivanov",
                Patronymic = "Ivanovich",
                CodeforcesLogin = "login",
                University = "VSTU",
                FromVstu = true,
                TeamsMemberships = new List<Domains.Teams.Team>(),
                LeadedTeams = new List<Domains.Teams.Team>()
            };

            _mockUserRepository
                .Setup(x => x
                    .UpdateAsync(
                        It.IsAny<User>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка обновления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _userService.UpdateAsync(expectedUser, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task DeleteAsync_WhenUserDoesExist_ShouldVerifyUnitOfWork()
        {
            User expectedUser = new User()
            {
                UserId = 1,
                FirstName = "Ivan",
                Surname = "Ivanov",
                Patronymic = "Ivanovich",
                CodeforcesLogin = "login",
                University = "VSTU",
                FromVstu = true,
                TeamsMemberships = new List<Domains.Teams.Team>(),
                LeadedTeams = new List<Domains.Teams.Team>()
            };

            _mockUserRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()));

            await _userService.DeleteAsync(expectedUser.UserId, new CancellationToken());

            _mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenUserDoesNotExist_ShouldThrowException()
        {
            int deletingId = 1;

            _mockUserRepository
                .Setup(x => x
                    .DeleteAsync(
                        It.IsAny<int>(),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(new ObjectNotFoundException("Ошибка удаления!"));

            var exception = await Assert.ThrowsAsync<ObjectNotFoundException>(()
                => _userService.DeleteAsync(deletingId, new CancellationToken()));

            Assert.Equal(typeof(ObjectNotFoundException), exception.GetType());
        }
    }
}
