using System.Collections.Generic;
using System.Linq;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.EntityViews.Coach
{
    public class CoachView
    {
        public int CoachId { get; set; }
        public string CodeforcesLogin { get; set; }
        public string FullName => $"{Surname} {FirstName} {Patronymic}".TrimEnd(' ');
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string AcademicDegree { get; set; }
        public string University { get; set; }
        public List<TeamDto> TrainingTeams { get; set; } = new();
        public CoachView(CoachDto userDto)
        {
            CoachId = userDto.CoachId;
            FirstName = userDto.FirstName;
            Surname = userDto.Surname;
            Patronymic = userDto.Patronymic;
            CodeforcesLogin = userDto.CodeforcesLogin;
            University = userDto.University;
            AcademicDegree = userDto.AcademicDegree;
            TrainingTeams = userDto.TrainingTeams.ToList();
        }

        public static IEnumerable<CoachView> ConvertToView(IEnumerable<CoachDto> userDtos)
        {
            return userDtos.Select(u => new CoachView(u));
        }
    }
}
