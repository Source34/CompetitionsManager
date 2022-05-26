using System;
using System.Windows;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.Windows.Coaches
{
    /// <summary>
    /// Логика взаимодействия для CoachInfoWindow.xaml
    /// </summary>
    public partial class CoachInfoWindow : Window
    {
        public CoachInfoWindow(CoachDto coach)
        {
            InitializeComponent();

            CoachFullNameTextBox.Text = $"{coach.Surname} {coach.FirstName} {coach.Patronymic}".TrimEnd(' ');
            CoachCodeForcesLoginTextBox.Text = coach.CodeforcesLogin;
            CoachUniversityTextBox.Text = coach.University;
            CoachAcademicDegreeTextBox.Text = coach.AcademicDegree;

            CoachInfoDataGrid.ItemsSource = coach.TrainingTeams;
        }

        private void OkCoachCreatingButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
