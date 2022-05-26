using CompetitionManager.Data.Entities.Competitions;
using CompetitionManager.Data.Entities.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompetitionManager.Data.Entities.TaskResults
{
    public class TaskResultDbModel
    {
        public int TaskResultId { get; set; }
        public virtual CompetitionDbModel? Competition { get; set; }
        public virtual TeamDbModel? Team { get; set; }
        public virtual Tasks.TaskDbModel? Task { get; set; }
        public double ElapsedMinutes { get; set; }
        public double Result { get; set; }
    }

    internal class Map : IEntityTypeConfiguration<TaskResultDbModel>
    {
        public void Configure(EntityTypeBuilder<TaskResultDbModel> builder)
        {
            builder.ToTable("TaskResult")
                .HasKey(p => p.TaskResultId)
                .HasName("pk_taskResult_id");

            builder.HasOne(p => p.Task)
                .WithMany(p => p.TaskResults)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
