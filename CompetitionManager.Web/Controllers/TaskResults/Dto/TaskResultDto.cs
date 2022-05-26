using System.Text.Json.Serialization;
using CompetitionManager.Web.Controllers.Competitions.Dto;
using CompetitionManager.Web.Controllers.Tasks.Dto;
using CompetitionManager.Web.Controllers.Teams.Dto;

namespace CompetitionManager.Web.Controllers.TaskResults.Dto
{
    public class TaskResultDto
    {
        public int TaskResultId { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual CompetitionDto Competition { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual TeamDto Team { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual TaskDto Task { get; set; }
        public double ElapsedMinutes { get; set; }
        public double Result { get; set; }
    }
}
