using CompetitionManager.Web.Controllers.Competitions.Dto;
using CompetitionManager.Web.Controllers.TaskResults.Dto;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CompetitionManager.Web.Controllers.Tasks.Dto
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Solution { get; set; }
        public string InputExample { get; set; }
        public string OutputExample { get; set; }
        public double Points { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual List<CompetitionDto> Competitions { get; set; } = new();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual List<TaskResultDto> TaskResults { get; set; } = new();
    }
}
