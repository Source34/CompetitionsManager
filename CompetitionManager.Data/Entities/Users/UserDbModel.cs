using CompetitionManager.Data.Entities.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompetitionManager.Data.Entities.Users
{
    public class UserDbModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string CodeforcesLogin { get; set; }
        public string University { get; set; }
        public bool FromVstu { get; set; }
        public virtual List<TeamDbModel>? TeamsMemberships { get; set; } = new();
        public virtual List<TeamDbModel>? LeadedTeams { get; set; } = new();
        public string GradebookNumber { get; set; }
    }

    internal class Map : IEntityTypeConfiguration<UserDbModel>
    {
        public void Configure(EntityTypeBuilder<UserDbModel> builder)
        {
            builder.ToTable("Users")
                .HasKey(p => p.UserId)
                .HasName("pk_user_id");
        }
    }
}
