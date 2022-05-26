using System.Collections.Generic;

namespace CompetitionManager.Web.Controllers.Coaches.Dto
{
    public class CreateUpdateCoachDto
    {
        public int CoachId { get; set; }
        public string CodeforcesLogin { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string AcademicDegree { get; set; }
        public string University { get; set; }
        public List<int> TrainingTeamsIds { get; set; } = new();
    }
}
