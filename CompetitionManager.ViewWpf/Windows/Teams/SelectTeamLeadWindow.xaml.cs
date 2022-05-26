using System.Linq;
using System.Windows;
using System.Collections.Generic;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.EntityViews.Users;

namespace CompetitionManager.ViewWpf.Windows.Teams
{
    /// <summary>
    /// Логика взаимодействия для SelectTeamLeadWindow.xaml
    /// </summary>
    public partial class SelectTeamLeadWindow : Window
    {
        public UserDto? SelectedTeamLead { get; private set; }

        private readonly List<UserDto> _userDtos;

        public SelectTeamLeadWindow(List<UserDto> users)
        {
            InitializeComponent();

            _userDtos = users;
            TeammatesDataGrid.ItemsSource = UserView.ConvertToView(users);
            SelectedTeamLead = null;

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeammatesDataGrid.SelectedItem != null)
            {
                SelectedTeamLead = _userDtos
                    .FirstOrDefault(p => p.UserId == ((UserView)TeammatesDataGrid.SelectedItem).UserId);
                this.Close();
            }
            else MessageBox.Show("Выберите тимлида!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
