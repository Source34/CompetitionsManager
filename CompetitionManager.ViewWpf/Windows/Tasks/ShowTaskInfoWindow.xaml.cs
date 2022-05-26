using System.Windows;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.Windows.Tasks
{
    /// <summary>
    /// Логика взаимодействия для ShowTaskInfoWindow.xaml
    /// </summary>
    public partial class ShowTaskInfoWindow : Window
    {
        public ShowTaskInfoWindow(TaskDto task)
        {
            InitializeComponent();

            TaskInfoTaskNameTextBox.Text = task.Name;
            TaskInfoTaskTextTextBox.Text = task.Text;
            TaskInfoSolutionTextBox.Text = task.Solution;
            TaskInfoInputExampleTextBox.Text = task.InputExample;
            TaskInfoOutputExampleTextBox.Text = task.OutputExample;
            TaskInfoDifficultSlider.Value = task.Points;
        }

        private void TaskInfoOkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
