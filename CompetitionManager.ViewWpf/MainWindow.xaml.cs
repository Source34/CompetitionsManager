using System;
using System.Windows;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.EntityViews.Coach;
using CompetitionManager.ViewWpf.EntityViews.Competitions;
using CompetitionManager.ViewWpf.EntityViews.Teams;
using CompetitionManager.ViewWpf.EntityViews.Users;
using CompetitionManager.ViewWpf.Windows.Coaches;
using CompetitionManager.ViewWpf.Windows.Competitions;
using CompetitionManager.ViewWpf.Windows.Tasks;
using CompetitionManager.ViewWpf.Windows.Teams;
using CompetitionManager.ViewWpf.Windows.Users;

namespace CompetitionManager.ViewWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// https://localhost:44328/swagger/v1/swagger.json
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataAccess.Init();
            InitDataGridContent();
        }

        private async void InitDataGridContent()
        {
            try
            {
                UsersDataGrid.ItemsSource = UserView.ConvertToView(await DataAccess.Storage.GetAllUsersAsync());
                CoachesDataGrid.ItemsSource = CoachView.ConvertToView(await DataAccess.Storage.GetAllCoachesAsync());
                TeamsDataGrid.ItemsSource = TeamView.ConvertToView(await DataAccess.Storage.GetAllTeamsAsync());
                CompetitionDataGrid.ItemsSource = CompetitionView.ConvertToView(await DataAccess.Storage.GetAllCompetitionsAsync());
                TasksDataGrid.ItemsSource = await DataAccess.Storage.GetAllTasksAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region User
        private async void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UsersDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
                var item = (UserView)UsersDataGrid.SelectedItem;
                await DataAccess.Storage.DeleteUserAsync(item.UserId);

                UsersDataGrid.ItemsSource = UserView.ConvertToView(await DataAccess.Storage.GetAllUsersAsync());
                UsersDataGrid.Items.Refresh();

                TeamsDataGrid.ItemsSource = TeamView.ConvertToView(await DataAccess.Storage.GetAllTeamsAsync());
                TeamsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите существующую запись!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите участника!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var item = (UserView)UsersDataGrid.SelectedItem;
            var userEditWindow = new CreateEditUserWindow(item);
            userEditWindow.ShowDialog();

            UsersDataGrid.ItemsSource = UserView.ConvertToView(await DataAccess.Storage.GetAllUsersAsync());
            UsersDataGrid.Items.Refresh();

            TeamsDataGrid.ItemsSource = TeamView.ConvertToView(await DataAccess.Storage.GetAllTeamsAsync());
            TeamsDataGrid.Items.Refresh();
        }

        private async void CreateNewUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var creatUserWindow = new CreateEditUserWindow();
                creatUserWindow.ShowDialog();

                UsersDataGrid.ItemsSource = UserView.ConvertToView(await DataAccess.Storage.GetAllUsersAsync());
                UsersDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ShowUserInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UsersDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите участника!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var item = (UserView)UsersDataGrid.SelectedItem;
                var user = await DataAccess.Storage.GetUserByIdAsync(item.UserId);
                var userShowWindow = new UserInfoWindow(user);
                userShowWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        
        #region Coach
        private async void DeleteCoachButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CoachesDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
                var item = (CoachView)CoachesDataGrid.SelectedItem;
                await DataAccess.Storage.DeleteCoachAsync(item.CoachId);

                CoachesDataGrid.ItemsSource = CoachView.ConvertToView(await DataAccess.Storage.GetAllCoachesAsync());
                CoachesDataGrid.Items.Refresh();

                TeamsDataGrid.ItemsSource = TeamView.ConvertToView(await DataAccess.Storage.GetAllTeamsAsync());
                TeamsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите существующую запись!", "Ошибка!", 
                                 MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void EditCoachButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CoachesDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var coach = (CoachView)CoachesDataGrid.SelectedItem;
                var coachEditWindow = new CreateEditCoachWindow(coach);
                coachEditWindow.ShowDialog();

                CoachesDataGrid.ItemsSource = CoachView.ConvertToView(await DataAccess.Storage.GetAllCoachesAsync());
                CoachesDataGrid.Items.Refresh();

                TeamsDataGrid.ItemsSource = TeamView.ConvertToView(await DataAccess.Storage.GetAllTeamsAsync());
                TeamsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void CreateNewCoachButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateEditCoachWindow createCoachWindow = new CreateEditCoachWindow();
                createCoachWindow.ShowDialog();

                CoachesDataGrid.ItemsSource = CoachView.ConvertToView(await DataAccess.Storage.GetAllCoachesAsync());
                CoachesDataGrid.Items.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ShowCoachInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CoachesDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var item = (CoachView)CoachesDataGrid.SelectedItem;
                var coach =  await DataAccess.Storage.GetCoachByIdAsync(item.CoachId);
                var coachInfoWindow = new CoachInfoWindow(coach);
                coachInfoWindow.ShowDialog();

            }
            catch (Exception exception)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Team
        private async void ShowTeamInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TeamsDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите участника!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var item = (TeamView)TeamsDataGrid.SelectedItem;
                var team = await DataAccess.Storage.GetTeamByIdAsync(item.TeamId);
                var teamShowWindow = new ShowTeamInfoWindow(team);
                teamShowWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void CreateNewTeamButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var teamCreateWindow = new CreateEditTeamWindow();
                teamCreateWindow.ShowDialog();

                TeamsDataGrid.ItemsSource = TeamView.ConvertToView(DataAccess.Storage.GetAllTeamsAsync().Result);
                TeamsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void EditTeamButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TeamsDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var team = (TeamView)TeamsDataGrid.SelectedItem;
                var fullTeamObj = await DataAccess.Storage.GetTeamByIdAsync(team.TeamId);

                var teamhEditWindow = new CreateEditTeamWindow(fullTeamObj);
                teamhEditWindow.ShowDialog();

                TeamsDataGrid.ItemsSource = TeamView.ConvertToView(await DataAccess.Storage.GetAllTeamsAsync());
                TeamsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteTeamButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TeamsDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var item = (TeamView)TeamsDataGrid.SelectedItem;

                await DataAccess.Storage.DeleteTeamAsync(item.TeamId);

                TeamsDataGrid.ItemsSource = TeamView.ConvertToView(await DataAccess.Storage.GetAllTeamsAsync());
                TeamsDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите существующую запись!", "Ошибка!",
                                 MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Task
        private async void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TasksDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
                var item = (TaskDto)TasksDataGrid.SelectedItem;
                await DataAccess.Storage.DeleteTaskAsync(item.TaskId);
                TasksDataGrid.ItemsSource = await DataAccess.Storage.GetAllTasksAsync();
                TasksDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Выберите существующую запись!", "Ошибка!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TasksDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var coach = (TaskDto)TasksDataGrid.SelectedItem;
                var coachEditWindow = new CreateEditNewTaskWindow(coach);
                coachEditWindow.ShowDialog();

                TasksDataGrid.ItemsSource = await DataAccess.Storage.GetAllTasksAsync();
                TasksDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var createTaskWindow = new CreateEditNewTaskWindow();
                createTaskWindow.ShowDialog();
                TasksDataGrid.ItemsSource = await DataAccess.Storage.GetAllTasksAsync();
                TasksDataGrid.Items.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ShowTaskInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TasksDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var item = (TaskDto)TasksDataGrid.SelectedItem;
                var task = await DataAccess.Storage.GetTaskByIdAsync(item.TaskId);
                var taskInfoWindow = new ShowTaskInfoWindow(task);
                taskInfoWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Competition
        private async void DeleteCompetitionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CompetitionDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите Соревнование!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var competitionView = (CompetitionView)CompetitionDataGrid.SelectedItem;
                await DataAccess.Storage.DeleteCompetitionAsync(competitionView.CompetitionId);

                CompetitionDataGrid.ItemsSource = CompetitionView.ConvertToView(await DataAccess.Storage.GetAllCompetitionsAsync());
                CompetitionDataGrid.Items.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Выберите существующую запись!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void EditCompetitiomButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompetitionDataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите соревнование!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var competitionView = (CompetitionView)CompetitionDataGrid.SelectedItem;
            var teamFullObj = await DataAccess.Storage.GetCompetitionByIdAsync(competitionView.CompetitionId);

            var editCompetitionWindowWindow = new CreateEditCompetitionWindow(teamFullObj);
            editCompetitionWindowWindow.ShowDialog();

            CompetitionDataGrid.ItemsSource = CompetitionView.ConvertToView(await DataAccess.Storage.GetAllCompetitionsAsync());
            CompetitionDataGrid.Items.Refresh();
        }

        private async void CreateCompetitionButton_Click(object sender, RoutedEventArgs e)
        {
            var createCompetition = new CreateEditCompetitionWindow();
            createCompetition.ShowDialog();

            CompetitionDataGrid.ItemsSource = CompetitionView.ConvertToView(await DataAccess.Storage.GetAllCompetitionsAsync());
            CompetitionDataGrid.Items.Refresh();
        }
        private async void EditResultsButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompetitionDataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите запись!", "Внимание!", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            var competitionView = (CompetitionView)CompetitionDataGrid.SelectedItem;
            var competition =  await DataAccess.Storage.GetCompetitionByIdAsync(competitionView.CompetitionId);
            var editResultWindow = new EditTaskResultWindow(competition);
            editResultWindow.ShowDialog();
        }
        #endregion

        private async void ShowCompetitionInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CompetitionDataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите участника!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var item = (CompetitionView)CompetitionDataGrid.SelectedItem;
                var comp = await DataAccess.Storage.GetCompetitionByIdAsync(item.CompetitionId);
                var compShowWindow = new ShowCompetitionInfo(comp);
                compShowWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Что пошло не так!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
