using CompetitionManager.Data.Entities.Coaches;
using CompetitionManager.Data.Entities.Competitions;
using CompetitionManager.Data.Entities.TaskResults;
using CompetitionManager.Data.Entities.Tasks;
using CompetitionManager.Data.Entities.Teams;
using CompetitionManager.Data.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CompetitionManager.Data.Contexts
{
    public class CompetitionDbContext : DbContext
    {
        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<CoachDbModel> Coaches { get; set; }
        public DbSet<TeamDbModel> Teams { get; set; }
        public DbSet<CompetitionDbModel> Competitions { get; set; }
        public DbSet<TaskResultDbModel> TaskResult { get; set; }
        public DbSet<TaskDbModel> Tasks { get; set; }

        public CompetitionDbContext() { }
        public CompetitionDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompetitionDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
