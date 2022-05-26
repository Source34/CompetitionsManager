using System.Collections.Generic;
using System.Text.Json.Serialization;
using CompetitionManager.Web.Controllers.Users.Dto;
using CompetitionManager.Web.Controllers.Coaches.Dto;
using CompetitionManager.Web.Controllers.TaskResults.Dto;
using CompetitionManager.Web.Controllers.Competitions.Dto;

namespace CompetitionManager.Web.Controllers.Teams.Dto
{
    public class TeamDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public UserDto TeamLead { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CoachDto Coach { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<UserDto> TeamMates { get; set; } = new();
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<TaskResultDto> Results { get; set; } = new();
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CompetitionDto> Competitions { get; set; } = new();
    }
}
