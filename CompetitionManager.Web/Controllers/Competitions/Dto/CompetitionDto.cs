using CompetitionManager.Web.Controllers.TaskResults.Dto;
using CompetitionManager.Web.Controllers.Tasks.Dto;
using CompetitionManager.Web.Controllers.Teams.Dto;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CompetitionManager.Web.Controllers.Competitions.Dto
{
    public class CompetitionDto
    {
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public string Description { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual List<TaskDto> Tasks { get; set; } = new();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual List<TeamDto> Teams { get; set; } = new();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual List<TaskResultDto> Results { get; set; } = new();
    }
}
