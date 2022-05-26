using CompetitionManager.Core.Seriveces;

namespace CompetitionManager.Data.Contexts
{
    public class CompetitionUnitOfWork : IUnitOfWork
    {
        private readonly CompetitionDbContext _context;
        public CompetitionUnitOfWork(CompetitionDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
