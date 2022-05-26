using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.EntityViews.Teams;
using CompetitionManager.ViewWpf.EntityViews.Users;

namespace CompetitionManager.ViewWpf.Windows.Teams
{
    /// <summary>
    /// Логика взаимодействия для ShowTeamInfoWindow.xaml
    /// </summary>
    public partial class ShowTeamInfoWindow : Window
    {
        public ShowTeamInfoWindow(TeamDto team)
        {
            InitializeComponent();

            TeamInfoTeamNameTextBox.Text = team.Name;
            TeamInfoTeamLeadTextBox.Text = team.TeamLead != null ? $"{team.TeamLead.Surname} {team.TeamLead.FirstName} {team.TeamLead.Patronymic}".TrimEnd() : null;
            TeamInfoCoachTextBox.Text = team.Coach != null ? $"{team.Coach.Surname} {team.Coach.FirstName} {team.Coach.Patronymic}".TrimEnd() : null;

            TeamInfoMatesDataGrid.ItemsSource = UserView.ConvertToView(team.TeamMates);

            var results = new List<TeamCompetitionsResults>();
            foreach (var competion in team.Competitions)
            {
                double points = DataAccess.Storage.GetTeamPointsByCompetitonIdAsync(competion.CompetitionId, team.TeamId).Result;
                results.Add(new TeamCompetitionsResults(competion.CompetitionName, team.Name, points));
            }

            TeamInfoResultsDataGrid.ItemsSource = results;
        }

        private void TeamInfoOkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
