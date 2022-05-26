using System;
using System.Linq;
using System.Collections.Generic;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.EntityViews.Competitions
{
    internal class CompetitionView
    {
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TasksCount { get; set; }
        public int TeamsCount { get; set; }

        public CompetitionView(CompetitionDto competition)
        {
            CompetitionId = competition.CompetitionId;
            CompetitionName = competition.CompetitionName;
            Description = competition.Description;
            StartDate = competition.StartEventDate.DateTime;
            EndDate = competition.EndEventDate.DateTime;
            TasksCount = competition.Tasks.Count;
            TeamsCount = competition.Teams.Count;
        }

        public static IEnumerable<CompetitionView> ConvertToView(IEnumerable<CompetitionDto> competitions)
        {
            return competitions.Select(cmpt => new CompetitionView(cmpt)).ToList();
        }
    }
}
