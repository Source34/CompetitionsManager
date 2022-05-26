using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.EntityViews.TaskResults;
using CompetitionManager.ViewWpf.EntityViews.Teams;

namespace CompetitionManager.ViewWpf.Windows.Competitions
{
    /// <summary>
    /// Логика взаимодействия для EditTaskResultWindow.xaml
    /// </summary>
    public partial class EditTaskResultWindow : Window
    {
        private readonly CompetitionDto _contextCompetitionDto;
        private readonly List<TeamResultView> _teamResultViews;

        public EditTaskResultWindow(CompetitionDto CompetitionDto)
        {
            InitializeComponent();
            _contextCompetitionDto = CompetitionDto;

            Title = $"Результаты соревнования: {CompetitionDto.CompetitionName}";

            TeamRatingDataGrid.Items.SortDescriptions.Add(new SortDescription("PointsSum", ListSortDirection.Descending));

            var teams = DataAccess.Storage.GetAllTeamsAsync().Result
                .Where(p=>p.Competitions
                    .Any(p => p.CompetitionId == _contextCompetitionDto.CompetitionId));

            _teamResultViews = TeamResultView.ConvertToView(teams, CompetitionDto.CompetitionId).ToList();
            TeamRatingDataGrid.ItemsSource = _teamResultViews;

            UpdateTeamsRating();
        }

        private async void TeamRatingDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeamRatingDataGrid.SelectedItem == null)
                return;
            
            var teamResultView = (TeamResultView)TeamRatingDataGrid.SelectedItem;

            ResultLabel.Content = $"Результат команды: {teamResultView.Name}";

            var taskResults = TaskResultView.ConvertToView(
                await DataAccess.Storage.GetTaskResultsByTeamIdAsync(teamResultView.TeamId, _contextCompetitionDto.CompetitionId));
            
            TaskResultsDataGrid.ItemsSource = taskResults;
            TaskResultsDataGrid.Items.Refresh();
        }

        private async void EditTaskResults_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TeamRatingDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var teamResultView = (TeamResultView)TeamRatingDataGrid.SelectedItem;

                var teamDto = _contextCompetitionDto.Teams.First(p => p.TeamId == teamResultView.TeamId);
                var editTeamTaskResultsWindow = new EditTeamTaskResultsWindow(_contextCompetitionDto, teamDto);
                editTeamTaskResultsWindow.ShowDialog();

                var taskResults = TaskResultView.ConvertToView(
                    await DataAccess.Storage.GetTaskResultsByTeamIdAsync(teamResultView.TeamId, _contextCompetitionDto.CompetitionId));

                TaskResultsDataGrid.ItemsSource = taskResults;
                TaskResultsDataGrid.Items.Refresh();

                var summaryTeamPoints = taskResults.Sum(p => p.ResultPoints);
                _teamResultViews.First(p => p.TeamId == teamResultView.TeamId).PointsSum = summaryTeamPoints;

                TeamRatingDataGrid.ItemsSource = _teamResultViews;
                TeamRatingDataGrid.Items.Refresh();
                
                TeamRatingDataGrid.Items.SortDescriptions.Add(new SortDescription("PointsSum", ListSortDirection.Descending));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private async void UpdateTeamsRating()
        {
            foreach (var teamRate in _teamResultViews)
            {
                var taskResults = TaskResultView.ConvertToView(
                    await DataAccess.Storage.GetTaskResultsByTeamIdAsync(teamRate.TeamId, _contextCompetitionDto.CompetitionId));

                var summaryTeamPoints = taskResults.Sum(p => p.ResultPoints);
                _teamResultViews.First(p => p.TeamId == teamRate.TeamId).PointsSum = summaryTeamPoints;
            }

            TeamRatingDataGrid.ItemsSource = _teamResultViews;
            TeamRatingDataGrid.Items.Refresh();
            
            TeamRatingDataGrid.Items.SortDescriptions.Add(new SortDescription("PointsSum", ListSortDirection.Descending));
        }

        private async void DeleteTaskResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TaskResultsDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите результат!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (TaskResultsDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите команду!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var taskResultView = (TaskResultView)TaskResultsDataGrid.SelectedItem;
                await DataAccess.Storage.DeleteTaskResultAsync(taskResultView.TaskResultId);

                var teamResultView = (TeamResultView)TeamRatingDataGrid.SelectedItem;
                var teamDto = _contextCompetitionDto.Teams.First(p => p.TeamId == teamResultView.TeamId);

                var taskResults = TaskResultView.ConvertToView(
                    await DataAccess.Storage.GetTaskResultsByTeamIdAsync(teamResultView.TeamId, _contextCompetitionDto.CompetitionId));

                TaskResultsDataGrid.ItemsSource = taskResults;
                TaskResultsDataGrid.Items.Refresh();

                var summaryTeamPoints = taskResults.Sum(p => p.ResultPoints);
                _teamResultViews.First(p => p.TeamId == teamResultView.TeamId).PointsSum = summaryTeamPoints;

                TeamRatingDataGrid.ItemsSource = _teamResultViews;
                TeamRatingDataGrid.Items.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
