using System.Linq;
using System.Windows;
using System.Collections.Generic;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.EntityViews.Teams;

namespace CompetitionManager.ViewWpf.Windows.Competitions
{
    /// <summary>
    /// Логика взаимодействия для EditTeamsWindow.xaml
    /// </summary>
    public partial class EditTeamsWindow : Window
    {
        public List<TeamDto> Teams { get; set; }
        private readonly List<TeamDto> _oldTeams;

        public EditTeamsWindow(IEnumerable<TeamDto> teams)
        {
            InitializeComponent();

            _oldTeams = teams.ToList();
            Teams = _oldTeams;
            SelectedTeamsDataGrid.ItemsSource =  TeamView.ConvertToView(Teams);
        }

        private async void IncludeTeamsButton_Click(object sender, RoutedEventArgs e)
        {
            var newTeams = TeamsDataGrid.SelectedItems.Cast<TeamView>();

            foreach (var team in newTeams.Where(t => Teams.All(p => p.TeamId != t.TeamId)))
                Teams.Add(await DataAccess.Storage.GetTeamByIdAsync(team.TeamId));

            SelectedTeamsDataGrid.ItemsSource = TeamView.ConvertToView(Teams);
            SelectedTeamsDataGrid.Items.Refresh();
        }

        private void ExcludeTeamsButton_Click(object sender, RoutedEventArgs e)
        {
            var teams = SelectedTeamsDataGrid.SelectedItems.Cast<TeamView>();

            foreach (var team in teams)
            {
                if (SelectedTeamsDataGrid.Items.Contains(team))
                    Teams.RemoveAll(p => p.TeamId == team.TeamId);
            }

            SelectedTeamsDataGrid.ItemsSource = TeamView.ConvertToView(Teams);
            SelectedTeamsDataGrid.Items.Refresh();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }    
        private async void FindButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(QueryTextBox.Text))
            {
                MessageBox.Show("Введите запрос!");
                return;
            }

            var query = QueryTextBox.Text;
            var tasks = await DataAccess.Storage.GetTeamByNameAsync(query);

            TeamsDataGrid.ItemsSource = tasks;
            TeamsDataGrid.Items.Refresh();
        }
        private async void FindAllButton_Click(object sender, RoutedEventArgs e)
        {
            TeamsDataGrid.ItemsSource = TeamView.ConvertToView(await DataAccess.Storage.GetAllTeamsAsync());
            TeamsDataGrid.Items.Refresh();
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            TeamsDataGrid.ItemsSource = new List<TeamView>();
            QueryTextBox.Text = string.Empty;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Teams = _oldTeams;
            this.Close();
        }
    }
}
