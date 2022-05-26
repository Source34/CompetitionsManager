using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.EntityViews.TaskResults;

namespace CompetitionManager.ViewWpf.Windows.Competitions
{
    /// <summary>
    /// Логика взаимодействия для EditTeamTaskResultsWindow.xaml
    /// </summary>
    public partial class EditTeamTaskResultsWindow : Window
    {
        private readonly CompetitionDto _contextCompetitionDto;
        private readonly TeamDto _contextTeamDto;
        private readonly List<TaskResultView> _taskResultsView;
        public EditTeamTaskResultsWindow(CompetitionDto сompetition, TeamDto team)
        {
            InitializeComponent();
            _contextCompetitionDto = сompetition;
            _contextTeamDto = team;

            Title = $"Результаты. Совернование - {сompetition.CompetitionName} Команда - {team.Name}";

            TasksDataGrid.ItemsSource = _contextCompetitionDto.Tasks;
            var res = DataAccess.Storage.GetTaskResultsByTeamIdAsync(_contextTeamDto.TeamId, _contextCompetitionDto.CompetitionId).Result;
            _taskResultsView = TaskResultView.ConvertToView(res).ToList();

           TaskResultsDataGrid.ItemsSource = _taskResultsView;
        }

        private void IncludeTasksButton_Click(object sender, RoutedEventArgs e)
        {
            if (TasksDataGrid.SelectedItem == null)
                return;

            var tasks = TasksDataGrid.SelectedItems.Cast<TaskDto>();
   
            foreach (var task in tasks)
            {
                if (_taskResultsView.All(p => p.TaskId != task.TaskId))
                {
                    _taskResultsView.Add(new TaskResultView()
                    {
                        Difficult = task.Points,
                        ElapsedTime = 0,
                        Name = task.Name,
                        ResultPoints = 0,
                        TaskId = task.TaskId
                    });
                }
            }
            TaskResultsDataGrid.Items.Refresh();
        }

        private void ExcludeTasksResultsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var taskResult in _taskResultsView)
                {
                    if (taskResult.ResultPoints < 0)
                        throw new Exception("Колличество баллов не может быть отрицательным!");
                    if (taskResult.ElapsedTime < 0)
                        throw new Exception("Колличество затраченного времени не может быть отрицательным!");
                }

                foreach (var taskResult in _taskResultsView)
                {                       
                    var postDto = new CreateUpdateTaskResultDto()
                    {
                        CompetitionId = _contextCompetitionDto.CompetitionId,
                        ElapsedMinutes = taskResult.ElapsedTime,
                        Result = taskResult.ResultPoints,
                        TaskId = taskResult.TaskId,
                        TeamId = _contextTeamDto.TeamId
                    };

                    if (taskResult.TaskResultId == 0)
                    {
                       await DataAccess.Storage.CreateTaskResultAsync(postDto);
                    }
                    else
                    {
                        await DataAccess.Storage.UpdateTaskResultAsync(taskResult.TaskResultId, postDto);
                    }
                }
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
