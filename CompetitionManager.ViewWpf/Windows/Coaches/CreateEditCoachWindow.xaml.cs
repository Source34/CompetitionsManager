using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FluentValidation;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.EntityViews.Coach;
using CompetitionManager.ViewWpf.Validators.Coaches;

namespace CompetitionManager.ViewWpf.Windows.Coaches
{
    /// <summary>
    /// Логика взаимодействия для CreateEditCoachWindow.xaml
    /// </summary>
    public partial class CreateEditCoachWindow : Window
    {
        private readonly bool _isCreatingCall;
        private readonly int _coachId;
        private readonly List<int> _trainingTeamsIds;
        public CreateEditCoachWindow()
        {
            InitializeComponent();
            this.Title = "Создать тренера";
            _isCreatingCall = true;
            _trainingTeamsIds = new List<int>();
        }
        public CreateEditCoachWindow(CoachView coach)
        {
            InitializeComponent();
            this.Title = "Редактировать тренера";

            _coachId = coach.CoachId;
            CoachFirstNameTextBox.Text = coach.FirstName;
            CoachSurnameTextBox.Text = coach.Surname;
            CoachPatronymicTextBox.Text = coach.Patronymic;
            CoachCodeForcesLoginTextBox.Text = coach.CodeforcesLogin;
            CoachUniversityTextBox.Text = coach.University;
            CoachAcademicDegreeTextBox.Text = coach.AcademicDegree;
            _trainingTeamsIds = coach.TrainingTeams.Select(s => s.TeamId).ToList();

            _isCreatingCall = false;
        }
        private async void OkCoachCreatingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var postDto = new CreateUpdateCoachDto()
                {
                    FirstName = CoachFirstNameTextBox.Text,
                    Surname = CoachSurnameTextBox.Text,
                    Patronymic = CoachPatronymicTextBox.Text,
                    University = CoachUniversityTextBox.Text,
                    CodeforcesLogin = CoachCodeForcesLoginTextBox.Text,
                    AcademicDegree = CoachAcademicDegreeTextBox.Text,
                    TrainingTeamsIds = _trainingTeamsIds
                };

                var coachDtoValidator = new CreateUpdateCoachDtoValidator();
                var result = await coachDtoValidator.ValidateAsync(postDto, options => options.ThrowOnFailures());
                if (!result.IsValid) return;

                if (_isCreatingCall)
                {
                    await DataAccess.Storage.CreateCoachAsync(postDto);
                    this.Close();
                    return;
                }

                await DataAccess.Storage.UpdateCoachAsync(_coachId, postDto);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}",
                    "Что-то пошло не так",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void CancelCoachCreatingButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
