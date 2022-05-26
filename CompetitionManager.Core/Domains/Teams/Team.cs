using CompetitionManager.Core.Domains.Coaches;
using CompetitionManager.Core.Domains.Competitions;
using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.Users;

namespace CompetitionManager.Core.Domains.Teams
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public User? TeamLead { get; set; }
        public Coach? Coach { get; set; }
        public List<User>? TeamMates { get; set; } = new(); 
        public List<TaskResult>? Results { get; set; } = new();
        public List<Competition>? Competitions { get; set; } = new();
    }
}
