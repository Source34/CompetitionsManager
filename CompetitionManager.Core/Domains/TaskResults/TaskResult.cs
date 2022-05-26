using CompetitionManager.Core.Domains.Competitions;
using CompetitionManager.Core.Domains.Teams;

namespace CompetitionManager.Core.Domains.TaskResults
{
    public class TaskResult
    {
        public int TaskResultId { get; set; }
        public virtual Competition? Competition { get; set; }
        public virtual Team? Team { get; set; }
        public virtual Tasks.Task? Task { get; set; }
        public double ElapsedMinutes { get; set; }
        public double Result { get; set; }
    }
}
