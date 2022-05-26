using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.EntityViews.Teams;

namespace CompetitionManager.ViewWpf.Windows.Competitions
{
    /// <summary>
    /// Логика взаимодействия для CreateEditCompetitionWindow.xaml
    /// </summary>
    public partial class CreateEditCompetitionWindow : Window
    {
        private readonly int _competitionId;
        private readonly CompetitionDto _contextCompetition;
        private readonly bool _isCreating;

        public CreateEditCompetitionWindow()
        {
            InitializeComponent();

            this.Title = "Создать соревнование";
            _contextCompetition = new CompetitionDto()
            {
                Teams = new List<TeamDto>(),
                Results = new List<TaskResultDto>(),
                Tasks = new List<TaskDto>()
            };
            _isCreating = true;
        }

        public CreateEditCompetitionWindow(CompetitionDto competition)
        {
            InitializeComponent();

            this.Title = "Редактировать соревнование";
            _contextCompetition = competition;
            _isCreating = false;

            _competitionId = competition.CompetitionId;
            CompetitionNameTextBox.Text = _contextCompetition.CompetitionName;
            CompetitionDescriptionTextBox.Text = _contextCompetition.Description;
            StartCompetitionDatePicker.SelectedDate = _contextCompetition.StartEventDate.DateTime;
            EndCompetitionDatePicker.SelectedDate = _contextCompetition.EndEventDate.DateTime;
            TeamsDataGrid.ItemsSource = TeamView.ConvertToView(_contextCompetition.Teams);
            TasksDataGrid.ItemsSource = _contextCompetition.Tasks;
        }

        private void EditTeamButton_Click(object sender, RoutedEventArgs e)
        {
            var editTeamsWindow = new EditTeamsWindow(_contextCompetition.Teams);
            editTeamsWindow.ShowDialog();

            _contextCompetition.Teams = editTeamsWindow.Teams;

            TeamsDataGrid.ItemsSource = TeamView.ConvertToView(_contextCompetition.Teams);
            TeamsDataGrid.Items.Refresh();
        }

        private void DeleteTeamButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamsDataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var item = (TeamView)TeamsDataGrid.SelectedItem;

            var teams = _contextCompetition.Teams.ToList();
            teams.RemoveAll(p => p.TeamId == item.TeamId);
            _contextCompetition.Teams = teams;

            TeamsDataGrid.ItemsSource = TeamView.ConvertToView(_contextCompetition.Teams);
            TeamsDataGrid.Items.Refresh();
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var editTaskWindow = new EditTasksWindow(_contextCompetition.Tasks);
            editTaskWindow.ShowDialog();

            _contextCompetition.Tasks = editTaskWindow.Tasks;

            TasksDataGrid.ItemsSource = _contextCompetition.Tasks;
            TasksDataGrid.Items.Refresh();
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var item = (TaskDto)TasksDataGrid.SelectedItem;

            var tasks = _contextCompetition.Tasks.ToList();

            tasks.RemoveAll(p => p.TaskId == item.TaskId);
            _contextCompetition.Tasks = tasks;

            TasksDataGrid.ItemsSource = _contextCompetition.Tasks;
            TasksDataGrid.Items.Refresh();
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _contextCompetition.CompetitionName = CompetitionNameTextBox.Text;
            _contextCompetition.Description = CompetitionDescriptionTextBox.Text;
            _contextCompetition.StartEventDate = GetDateTime(StartCompetitionDatePicker.SelectedDate);
            _contextCompetition.EndEventDate = GetDateTime(EndCompetitionDatePicker.SelectedDate);


            var postDto = new CreateUpdateCompetitionDto()
            {
                CompetitionName = _contextCompetition.CompetitionName,
                Description = _contextCompetition.Description,
                StartEventDate = _contextCompetition.StartEventDate,
                EndEventDate = _contextCompetition.EndEventDate,
                Tasks = _contextCompetition.Tasks.Select(s => s.TaskId).ToList(),
                Results = _contextCompetition.Results.Select(s => s.TaskResultId).ToList(),
                Teams = _contextCompetition.Teams.Select(s => s.TeamId).ToList(),
            };

            if (_isCreating)
            {
                await DataAccess.Storage.CreateCompetitionAsync(postDto);
                this.Close();
                return;
            }

            await DataAccess.Storage.UpdateCompetitionAsync(_competitionId, postDto);
            this.Close();
        }

        private DateTime GetDateTime(DateTime? dateTime)
        {
            if (dateTime == null)
                return DateTime.Now;
            return (DateTime)dateTime;
        }



        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
