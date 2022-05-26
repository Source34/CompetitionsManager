namespace CompetitionManager.Core.Domains.Users.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
        public Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
        public Task CreateAsync(User user, CancellationToken cancellationToken);
        public Task UpdateAsync(User user, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
        public Task<List<User>> GetByFioAsync(string fioParts, CancellationToken cancellationToken);

    }
}
