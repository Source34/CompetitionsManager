using System.Collections.Generic;

namespace CompetitionManager.Web.Controllers.Teams.Dto
{
    public class CreateUpdateTeamDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public int TeamLeadId { get; set; }
        public int CoachId { get; set; }
        public List<int> TeamMatesIds { get; set; } = new();
        public List<int> Results { get; set; } = new();
        public List<int> Competitions { get; set; } = new();
    }
}
