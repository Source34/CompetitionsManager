namespace CompetitionManager.Core.Seriveces
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
