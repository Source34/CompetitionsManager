using CompetitionManager.Data.Entities.Competitions;
using CompetitionManager.Data.Entities.TaskResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompetitionManager.Data.Entities.Tasks
{
    public class TaskDbModel
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Solution { get; set; }
        public string InputExample { get; set; }
        public string OutputExample { get; set; }
        public double Points { get; set; }
        public virtual List<CompetitionDbModel>? Competitions { get; set; } = new();
        public virtual List<TaskResultDbModel>? TaskResults { get; set; } = new();
    }

    internal class Map : IEntityTypeConfiguration<TaskDbModel>
    {
        public void Configure(EntityTypeBuilder<TaskDbModel> builder)
        {
            builder.ToTable("Task")
                .HasKey(p => p.TaskId)
                .HasName("pk_task_id");
        }
    }
}
