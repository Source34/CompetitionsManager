using System.Windows;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.OpenAPIsConnector;

namespace CompetitionManager.ViewWpf.Windows.Users
{
    /// <summary>
    /// Логика взаимодействия для UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        public UserInfoWindow(UserDto user)
        {
            InitializeComponent();

            FullNameTextBox.Text = $"{user.Surname} {user.FirstName} {user.Patronymic}".TrimEnd(' '); ;
            CodeForcesLoginTextBox.Text = user.CodeforcesLogin;
            UniversityTextBox.Text = user.University;
            FromVstuCheckBox.IsChecked = user.FromVstu;
            UserPointsSum.Text = DataAccess.Storage.GetUserTotalPointsByIdAsync(user.UserId).Result.ToString();
            UserGradebookTextBox.Text = user.GradebookNumber;

            UserTeamMembershipsDatagrid.ItemsSource = user.TeamsMemberships;
            UserLeadedTeamsDataGrid.ItemsSource = user.LeadedTeams;
        }

        private void OkUserInfoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
