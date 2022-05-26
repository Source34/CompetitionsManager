using CompetitionManager.Core.Domains.Coaches.Repositories;
using CompetitionManager.Core.Seriveces;

namespace CompetitionManager.Core.Domains.Coaches.Services
{
    public class CoachService : ICoachService
    {
        private readonly ICoachRepository _coachRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CoachService(ICoachRepository coachRepository, IUnitOfWork unitOfWork)
        {
            _coachRepository = coachRepository;
            _unitOfWork = unitOfWork;
        }
        public Task<Coach> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return _coachRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<List<Coach>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _coachRepository.GetAllAsync(cancellationToken);
        }

        public async Task CreateAsync(Coach coach, CancellationToken cancellationToken)
        {
            await _coachRepository.CreateAsync(coach, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Coach user, CancellationToken cancellationToken)
        {
            await _coachRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _coachRepository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}