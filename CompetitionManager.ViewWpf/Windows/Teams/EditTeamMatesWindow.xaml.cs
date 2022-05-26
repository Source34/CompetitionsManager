using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.EntityViews.Users;

namespace CompetitionManager.ViewWpf.Windows.Teams
{
    /// <summary>
    /// Логика взаимодействия для EditTeamMatesWindow.xaml
    /// </summary>
    public partial class EditTeamMatesWindow : Window
    {
        public List<UserDto> Teammates { get; set; }
        private readonly List<UserDto> _oldTeammates;
        public EditTeamMatesWindow(List<UserDto> teammates)
        {
            InitializeComponent();
            Teammates = teammates;
            _oldTeammates = teammates;
            TeammatesDataGrid.ItemsSource = UserView.ConvertToView(Teammates);
        }

        private async void FindButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(QueryTextBox.Text))
            {
                MessageBox.Show("Введите запрос!");
                return;
            }

            var query = QueryTextBox.Text;
            var users = await DataAccess.Storage.GetByFioUsersAsync(query);
            UsersDataGrid.ItemsSource = UserView.ConvertToView(users);
        }

        private async void FindAllButton_Click(object sender, RoutedEventArgs e)
        {
            UsersDataGrid.ItemsSource = UserView.ConvertToView(await DataAccess.Storage.GetAllUsersAsync());
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            UsersDataGrid.ItemsSource = new List<UserDto>();
            QueryTextBox.Text = string.Empty;
        }

        private async void IncludeTeammatesButton_Click(object sender, RoutedEventArgs e)
        {
            var newMates = UsersDataGrid.SelectedItems.Cast<UserView>();

            foreach (var mate in newMates.Where(mate => Teammates.All(p => p.UserId != mate.UserId)))
                Teammates.Add(await DataAccess.Storage.GetUserByIdAsync(mate.UserId));

            TeammatesDataGrid.ItemsSource = UserView.ConvertToView(Teammates);
            TeammatesDataGrid.Items.Refresh();
        }

        private void ExcludeTeammatesButton_Click(object sender, RoutedEventArgs e)
        {
            var newMates = TeammatesDataGrid.SelectedItems.Cast<UserView>();

            foreach (var mate in newMates)
            {
                if (TeammatesDataGrid.Items.Contains(mate))
                    Teammates.RemoveAll(p => p.UserId == mate.UserId);
            }

            TeammatesDataGrid.ItemsSource = UserView.ConvertToView(Teammates);
            TeammatesDataGrid.Items.Refresh();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Teammates = _oldTeammates;
            this.Close();
        }
    }
}
