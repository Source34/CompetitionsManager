using System.Collections.Generic;
using System.Linq;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.EntityViews.Teams
{
    public class TeamResultView
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public double PointsSum { get; set; }

        public TeamResultView(TeamDto team)
        {
            TeamId = team.TeamId;
            Name = team.Name;
            PointsSum = 0;
        }

        public TeamResultView(TeamDto team, double pointSum)
        {
            TeamId = team.TeamId;
            Name = team.Name;
            PointsSum = pointSum;
        }

        public static IEnumerable<TeamResultView> ConvertToView(IEnumerable<TeamDto> teams, int competitionId)
        {
            return teams.Select(tm => new TeamResultView(tm, competitionId)).ToList();
        }
    }
}
