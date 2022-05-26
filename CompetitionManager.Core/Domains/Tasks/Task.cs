using CompetitionManager.Core.Domains.Competitions;
using CompetitionManager.Core.Domains.TaskResults;

namespace CompetitionManager.Core.Domains.Tasks
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Solution { get; set; }
        public string InputExample { get; set; }
        public string OutputExample { get; set; }
        public double Points { get; set; }
        public virtual List<Competition>? Competitions { get; set; } = new();
        public virtual List<TaskResult>? TaskResults { get; set; } = new();
    }
}
