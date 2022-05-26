using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.EntityViews.Teams
{
    public class TeamCompetitionsResults
    {
        public TeamCompetitionsResults(string competitionName, string teamName, double sumPoints)
        {
            CompetitionName = competitionName;
            TeamName = teamName;
            SumPoints = sumPoints;
        }

        public string CompetitionName { get; set; }
        public string TeamName { get; set; }
        public double SumPoints { get; set; }

        public static TeamCompetitionsResults ConvertToView(string competitionName, string teamName, double sumPoints)
        {
            return new TeamCompetitionsResults(competitionName, teamName, sumPoints);
        }
    }
}
