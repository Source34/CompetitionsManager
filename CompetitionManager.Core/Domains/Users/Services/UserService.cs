using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.TaskResults.Repositories;
using CompetitionManager.Core.Domains.Users.Repositories;
using CompetitionManager.Core.Seriveces;

namespace CompetitionManager.Core.Domains.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _userRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _userRepository.GetAllAsync(cancellationToken);
        }

        public async Task CreateAsync(User user, CancellationToken cancellationToken)
        {
            await _userRepository.CreateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<double> GetTotalResultPointsAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            var teams = user.TeamsMemberships;

            var results = new List<TaskResult>();
            teams?.ForEach(x => results.AddRange(x.Results));

            return results.Select(x => x.Result).Sum();
        }

        public Task<List<User>> GetByFioAsync(string fio, CancellationToken cancellationToken)
        {
            return _userRepository.GetByFioAsync(fio, cancellationToken);
        }
    }
}
