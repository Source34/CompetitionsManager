using System.Collections.Generic;
using System.Linq;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.EntityViews.Teams
{
    internal class TeamView
    {
        public TeamView(TeamDto team)
        {
            TeamId = team.TeamId;
            Name = team.Name;
            CoachName = team.Coach != null ? $"{team.Coach.Surname} {team.Coach.FirstName} {team.Coach.Patronymic}".TrimEnd() : null;
            TeamleadName = team.TeamLead != null? $"{team.TeamLead.Surname} {team.TeamLead.FirstName} {team.TeamLead.Patronymic}".TrimEnd() : null;
            TeammatesCount = team.TeamMates.Count;
        }

        public int TeamId { get; set; }
        public string Name { get; set; }
        public string? CoachName { get; set; }
        public string? TeamleadName { get; set; }
        public int TeammatesCount { get; set; }

        public static IEnumerable<TeamView> ConvertToView(IEnumerable<TeamDto> teams)
        {
            return teams.Select(team => new TeamView(team)).ToList();
        }
    }
}
