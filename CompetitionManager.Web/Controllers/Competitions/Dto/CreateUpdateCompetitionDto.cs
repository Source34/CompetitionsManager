using System;
using System.Collections.Generic;

namespace CompetitionManager.Web.Controllers.Competitions.Dto
{
    public class CreateUpdateCompetitionDto
    {
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public string Description { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
        public virtual List<int> Tasks { get; set; } = new();
        public virtual List<int> Teams { get; set; } = new();
        public virtual List<int> Results { get; set; } = new();
    }
}
