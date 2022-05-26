using CompetitionManager.Core.Domains.Teams;
using CompetitionManager.Data.Entities.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompetitionManager.Data.Entities.Coaches
{
    public class CoachDbModel
    {
        public int CoachId { get; set; }
        public string CodeforcesLogin { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string AcademicDegree { get; set; }
        public string University { get; set; }
        public List<TeamDbModel>? TrainingTeams { get; set; } = new();
    }
    internal class Map : IEntityTypeConfiguration<CoachDbModel>
    {
        public void Configure(EntityTypeBuilder<CoachDbModel> builder)
        {
            builder.ToTable("Coaches")
                .HasKey(p => p.CoachId)
                .HasName("pk_coach_id");
        }
    }
}
