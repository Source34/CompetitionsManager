using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.EntityViews.Users
{
    public class UserView
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string FullName => $"{Surname} {FirstName} {Patronymic}".TrimEnd(' ');
        public string CodeforcesLogin { get; set; }
        public string GradebookNumber { get; set; }
        public string University { get; set; }
        public bool FromVstu { get; set; }
        public List<TeamDto> TeamsMemberships { get; set; } = new();
        public List<TeamDto> LeadedTeams { get; set; } = new();

        public UserView(UserDto userDto)
        {
            UserId = userDto.UserId;
            FirstName = userDto.FirstName;
            Surname = userDto.Surname;
            Patronymic = userDto.Patronymic;
            CodeforcesLogin = userDto.CodeforcesLogin;
            GradebookNumber = userDto.GradebookNumber;
            University = userDto.University;
            FromVstu = userDto.FromVstu;
            TeamsMemberships = userDto.TeamsMemberships.ToList();
            LeadedTeams = userDto.LeadedTeams.ToList();
        }

        public static IEnumerable<UserView> ConvertToView(IEnumerable<UserDto> userDtos)
        {
            return userDtos.Select(u => new UserView(u));
        }
    }
}
