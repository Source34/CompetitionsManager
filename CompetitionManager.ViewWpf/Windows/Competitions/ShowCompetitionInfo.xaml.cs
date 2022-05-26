using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.EntityViews.TaskResults;
using CompetitionManager.ViewWpf.EntityViews.Teams;

namespace CompetitionManager.ViewWpf.Windows.Competitions
{
    /// <summary>
    /// Логика взаимодействия для ShowCompetitionInfo.xaml
    /// </summary>
    public partial class ShowCompetitionInfo : Window
    {
        private readonly CompetitionDto _contextCompetitionDto;
        public ShowCompetitionInfo(CompetitionDto competitionDto)
        {
            InitializeComponent();
            _contextCompetitionDto = competitionDto;

            Title = $"Информация о соревновании {_contextCompetitionDto.CompetitionName}";

            CompetitionNameTextBox.Text = _contextCompetitionDto.CompetitionName;
            CompetitionDescriptionTextBox.Text = _contextCompetitionDto.Description;
            StartCompetitionDatePicker.SelectedDate = _contextCompetitionDto.StartEventDate.DateTime;
            EndCompetitionDatePicker.SelectedDate = _contextCompetitionDto.EndEventDate.DateTime;
            TeamsDataGrid.ItemsSource = TeamView.ConvertToView(_contextCompetitionDto.Teams);
            TasksDataGrid.ItemsSource = competitionDto.Tasks;

            var teams = DataAccess.Storage.GetTeamsByCompetitionIdAsync(_contextCompetitionDto.CompetitionId).Result.ToList();

            var teamResultViews = new List<TeamResultView>();
            foreach (var team in teams)
            {
                double points = DataAccess.Storage.GetTeamPointsByCompetitonIdAsync(_contextCompetitionDto.CompetitionId, team.TeamId).Result;
                teamResultViews.Add(new TeamResultView(team, points));
            }

            TeamRatingDataGrid.ItemsSource = teamResultViews;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void TeamRatingDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TeamRatingDataGrid.SelectedItem == null)
                return;

            var teamResultView = (TeamResultView)TeamRatingDataGrid.SelectedItem;

            var taskResults = TaskResultView.ConvertToView(await DataAccess.Storage.GetTaskResultsByTeamIdAsync(teamResultView.TeamId, _contextCompetitionDto.CompetitionId));

            TaskResultsDataGrid.ItemsSource = taskResults;
            TaskResultsDataGrid.Items.Refresh();
        }
    }
}
