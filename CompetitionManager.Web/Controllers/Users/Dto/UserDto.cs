using System.Collections.Generic;
using CompetitionManager.Web.Controllers.Teams.Dto;

namespace CompetitionManager.Web.Controllers.Users.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string CodeforcesLogin { get; set; }
        public string University { get; set; }
        public bool FromVstu { get; set; }
        public virtual List<TeamDto> TeamsMemberships { get; set; } = new();
        public virtual List<TeamDto> LeadedTeams { get; set; } = new();
        public string GradebookNumber { get; set; }
    }
}
