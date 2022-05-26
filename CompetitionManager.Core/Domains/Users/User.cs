using CompetitionManager.Core.Domains.Teams;

namespace CompetitionManager.Core.Domains.Users
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string CodeforcesLogin { get; set; }
        public string University { get; set; }
        public bool FromVstu { get; set; }
        public List<Team>? TeamsMemberships { get; set; } = new();
        public List<Team>? LeadedTeams { get; set; } = new();
        public string GradebookNumber { get; set; }
    }
}
