using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.EntityViews.Coach;
using CompetitionManager.ViewWpf.EntityViews.Users;
using CompetitionManager.ViewWpf.Validators.Teams;
using FluentValidation;

namespace CompetitionManager.ViewWpf.Windows.Teams
{
    /// <summary>
    /// Логика взаимодействия для CreateEditTeamWindow.xaml
    /// </summary>
    public partial class CreateEditTeamWindow : Window
    {
        private readonly bool _isCreatingCall;
        private readonly int _teamId;
        private readonly TeamDto _contextTeamDto;

        public CreateEditTeamWindow()
        {
            InitializeComponent();
            this.Title = "Создать участника";
            _isCreatingCall = true;

            _contextTeamDto = new TeamDto()
            {
                TeamMates = new List<UserDto>(),
                Competitions = new List<CompetitionDto>(),
                Results = new List<TaskResultDto>()
            };
        }

        public CreateEditTeamWindow(TeamDto contextTeam)
        {
            InitializeComponent();
            this.Title = "Редактировать участника";
            _isCreatingCall = false;

            _teamId = contextTeam.TeamId;
            _contextTeamDto = contextTeam;

            TeamNameTextBox.Text = contextTeam.Name;
            TeamLeadNameTextBox.Text = contextTeam.TeamLead != null ? $"{contextTeam.TeamLead.Surname} " +
                                                                      $"{contextTeam.TeamLead.FirstName} " +
                                                                      $"{contextTeam.TeamLead.Patronymic}".TrimEnd() : null;
            CoachNameTextBox.Text = contextTeam.Coach != null ? $"{contextTeam.Coach.Surname} " +
                                                                $"{contextTeam.Coach.FirstName} " +
                                                                $"{contextTeam.Coach.Patronymic}".TrimEnd() : null;

            TeammatesDataGrid.ItemsSource = UserTeamMateView.ConvertToView(contextTeam.TeamMates);
        }

        private void SetTeamLeadButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeammatesDataGrid.Items.Count == 0)
            {
                MessageBox.Show($"Сначала необходимо сформировать состава команды!", 
                                 "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var teamLeadWindow = new SelectTeamLeadWindow(_contextTeamDto.TeamMates.ToList());
            teamLeadWindow.ShowDialog();

            if (teamLeadWindow.SelectedTeamLead == null)
                return;

            _contextTeamDto.TeamLead = teamLeadWindow.SelectedTeamLead;
            TeamLeadNameTextBox.Text = new UserView(_contextTeamDto.TeamLead).FullName;
        }

        private void SetCoachButton_Click(object sender, RoutedEventArgs e)
        {
            var coachWindow = new SelectCoachWindow();
            coachWindow.ShowDialog();

            if (coachWindow.SelectedCoach == null)
                return;

            _contextTeamDto.Coach = coachWindow.SelectedCoach;
            CoachNameTextBox.Text = new CoachView(_contextTeamDto.Coach).FullName;
        }

        private void SetTeamatesButton_Click(object sender, RoutedEventArgs e)
        {
            var teamMatesWindow = new EditTeamMatesWindow(_contextTeamDto.TeamMates.ToList());
            teamMatesWindow.ShowDialog();

            _contextTeamDto.TeamMates = teamMatesWindow.Teammates;

            TeammatesDataGrid.ItemsSource = UserView.ConvertToView(_contextTeamDto.TeamMates);
            TeammatesDataGrid.Items.Refresh();
        }

        private async void OkTeamCreationEditButton_Click(object sender, RoutedEventArgs e)
        {
            _contextTeamDto.Name = TeamNameTextBox.Text;

            var teamValidator = new TeamValidator();
            var result = await teamValidator.ValidateAsync(_contextTeamDto, options => options.ThrowOnFailures());

            if (!result.IsValid)
                return;

            var postDto = new CreateUpdateTeamDto()
            {
                Name = _contextTeamDto.Name,
                TeamLeadId = _contextTeamDto.TeamLead.UserId,
                CoachId = _contextTeamDto.Coach.CoachId,
                TeamMatesIds = _contextTeamDto.TeamMates.Select(s => s.UserId).ToList(),
                Competitions = _contextTeamDto.Competitions.Select(s => s.CompetitionId).ToList(),
                Results = _contextTeamDto.Results.Select(s => s.TaskResultId).ToList()
            };

            if (_isCreatingCall)
            {
                await DataAccess.Storage.CreateTeamAsync(postDto);
                this.Close();
                return;
            }

            await DataAccess.Storage.UpdateTeamAsync(_teamId, postDto);
            this.Close();
        }

        private void CancelCreateEditButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
