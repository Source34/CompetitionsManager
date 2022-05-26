using CompetitionManager.Core.Domains.TaskResults;
using CompetitionManager.Core.Domains.Teams;

namespace CompetitionManager.Core.Domains.Competitions
{
    public class Competition
    {
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public string Description { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
        public virtual List<Tasks.Task>? Tasks { get; set; } = new();
        public virtual List<Team>? Teams { get; set; } = new();
        public virtual List<TaskResult>? Results { get; set; } = new();
    }
}
