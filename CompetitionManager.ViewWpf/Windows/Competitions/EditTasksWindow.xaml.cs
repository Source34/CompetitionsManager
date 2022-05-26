using System.Linq;
using System.Windows;
using System.Collections.Generic;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;

namespace CompetitionManager.ViewWpf.Windows.Competitions
{
    /// <summary>
    /// Логика взаимодействия для EditTasksWindow.xaml
    /// </summary>
    public partial class EditTasksWindow : Window
    {
        public List<TaskDto> Tasks { get; set; }
        private readonly List<TaskDto> _oldTasks;

        public EditTasksWindow(IEnumerable<TaskDto> tasks)
        {
            InitializeComponent();

            _oldTasks = new List<TaskDto>(tasks);
            Tasks = new List<TaskDto>(_oldTasks);
            SelectedTasksDataGrid.ItemsSource = Tasks;
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            TasksDataGrid.ItemsSource = new List<TaskDto>();
            QueryTextBox.Text = string.Empty;
        }

        private async void IncludeTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var newTasks = TasksDataGrid.SelectedItems.Cast<TaskDto>();

            foreach (var task in newTasks.Where(t => Tasks.All(p => p.TaskId != t.TaskId)))
                Tasks.Add(await DataAccess.Storage.GetTaskByIdAsync(task.TaskId));

            SelectedTasksDataGrid.ItemsSource = Tasks;
            SelectedTasksDataGrid.Items.Refresh();
        }

        private void ExcludeTasksButton_Click(object sender, RoutedEventArgs e)
        {
            var tasks = SelectedTasksDataGrid.SelectedItems.Cast<TaskDto>();

            foreach (var task in tasks)
            {
                if (SelectedTasksDataGrid.Items.Contains(task))
                    Tasks.RemoveAll(p => p.TaskId == task.TaskId);
            }

            SelectedTasksDataGrid.ItemsSource = Tasks;
            SelectedTasksDataGrid.Items.Refresh();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Tasks = _oldTasks;
            this.Close();
        }

        private async void FindAllButton_Click(object sender, RoutedEventArgs e)
        {
            TasksDataGrid.ItemsSource = await DataAccess.Storage.GetAllTasksAsync();
            TasksDataGrid.Items.Refresh();
        }

        private async void FindButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(QueryTextBox.Text))
            {
                MessageBox.Show("Введите запрос!");
                return;
            }
            var query = QueryTextBox.Text;
            var tasks = await DataAccess.Storage.GetTaskByNameAsync(query);

            TasksDataGrid.ItemsSource = tasks;
            TasksDataGrid.Items.Refresh();
        }
    }
}
