using System.Collections.Generic;
using System.Linq;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.EntityViews.Users
{
    public class UserTeamMateView
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string FullName => $"{Surname} {FirstName} {Patronymic}".TrimEnd(' ');
        public string CodeforcesLogin { get; set; }

        public UserTeamMateView(UserDto userDto)
        {
            UserId = userDto.UserId;
            FirstName = userDto.FirstName;
            Surname = userDto.Surname;
            Patronymic = userDto.Patronymic;
            CodeforcesLogin = userDto.CodeforcesLogin;
        }
        public static IEnumerable<UserTeamMateView> ConvertToView(IEnumerable<UserDto> userDtos)
        {
            return userDtos.Select(u => new UserTeamMateView(u));
        }
    }
}
