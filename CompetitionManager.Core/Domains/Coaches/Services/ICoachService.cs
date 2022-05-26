namespace CompetitionManager.Core.Domains.Coaches.Services
{
    public interface ICoachService
    {
        public Task<Coach> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<List<Coach>> GetAllAsync(CancellationToken cancellationToken);
        public Task CreateAsync(Coach coach, CancellationToken cancellationToken);
        public Task UpdateAsync(Coach coach, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
