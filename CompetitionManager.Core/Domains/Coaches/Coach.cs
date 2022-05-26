using CompetitionManager.Core.Domains.Teams;

namespace CompetitionManager.Core.Domains.Coaches
{
    public class Coach
    {
        public int CoachId { get; set; }
        public string CodeforcesLogin { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string AcademicDegree { get; set; }
        public string University { get; set; }
        public List<Team>? TrainingTeams { get; set; } = new();
    }
}
