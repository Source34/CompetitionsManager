using System.Collections.Generic;
using System.Linq;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.EntityViews.TaskResults
{
    public class TaskResultView
    {
        public int TaskResultId { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public double Difficult { get; set; }
        public double ResultPoints { get; set; }
        public double ElapsedTime { get; set; }

        public TaskResultView() { }
        public TaskResultView(TaskResultDto taskResult)
        {
            TaskResultId = taskResult.TaskResultId;
            TaskId = taskResult.Task.TaskId;
            Name = taskResult.Task.Name;
            Difficult = taskResult.Task.Points;
            ResultPoints = taskResult.Result;
            ElapsedTime = taskResult.ElapsedMinutes;
        }

        public static IEnumerable<TaskResultView> ConvertToView(IEnumerable<TaskResultDto> taskResult)
        {
            return taskResult.Select(tr => new TaskResultView(tr)).ToList();
        }
    }
}
