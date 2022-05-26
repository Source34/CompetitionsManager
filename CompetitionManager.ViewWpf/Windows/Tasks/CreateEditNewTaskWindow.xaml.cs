using System;
using System.Windows;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.Validators;
using CompetitionManager.ViewWpf.Validators.Tasks;
using FluentValidation;

namespace CompetitionManager.ViewWpf.Windows.Tasks
{
    /// <summary>
    /// Логика взаимодействия для CreateEditNewTaskWindow.xaml
    /// </summary>
    public partial class CreateEditNewTaskWindow : Window
    {
        private readonly bool _isCreatingCall;
        private readonly int _taskId;
        public CreateEditNewTaskWindow()
        {
            InitializeComponent();
            this.Title = "Создать задачу";
            _isCreatingCall = true;
        }

        public CreateEditNewTaskWindow(TaskDto task)
        {
            InitializeComponent();
            this.Title = "Редактировать участника";

            _taskId = task.TaskId;
            TaskNameTextBox.Text = task.Name;
            TaskContentTextBox.Text = task.Text;
            TaskSolutionTextBox.Text = task.Solution;
            TaskInputExampleTextBox.Text = task.InputExample;
            TaskOutputExampleTextBox.Text = task.OutputExample;
            TaskDifficultSlider.Value = task.Points;

            _isCreatingCall = false;
        }

        private async void CreateNewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var postDto = new CreateUpdateTaskDto()
                {
                    Name = TaskNameTextBox.Text,
                    Text = TaskContentTextBox.Text,
                    Solution = TaskSolutionTextBox.Text,
                    InputExample = TaskInputExampleTextBox.Text,
                    OutputExample = TaskOutputExampleTextBox.Text,
                    Points = TaskDifficultSlider.Value
                };

                var taskValidator = new CreateUpdateTaskValidator();
                var result = await taskValidator.ValidateAsync(postDto, options => options.ThrowOnFailures());
                if (!result.IsValid) return;

                if (_isCreatingCall)
                {
                    await DataAccess.Storage.CreateTaskAsync(postDto);
                    this.Close();
                    return;
                }

                await DataAccess.Storage.UpdateTaskAsync(_taskId, postDto);

                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void CancelNewTaskCreatingButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
