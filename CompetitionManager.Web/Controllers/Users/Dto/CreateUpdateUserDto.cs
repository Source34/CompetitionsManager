using System.Collections.Generic;

namespace CompetitionManager.Web.Controllers.Users.Dto
{
    public class CreateUpdateUserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string CodeforcesLogin { get; set; }
        public string University { get; set; }
        public bool FromVstu { get; set; }
        public virtual List<int> TeamsMemberships { get; set; } = new();
        public virtual List<int> LeadedTeams { get; set; } = new();
        public string GradebookNumber { get; set; }

    }
}
