using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.EntityViews.Users;
using CompetitionManager.ViewWpf.Validators.Users;
using FluentValidation;

namespace CompetitionManager.ViewWpf.Windows.Users
{
    /// <summary>
    /// Логика взаимодействия для CreateEditUserWindow.xaml
    /// </summary>
    public partial class CreateEditUserWindow : Window
    {
        private readonly bool _isCreatingCall;
        private readonly int _userId;
        private readonly List<int> _teamsMemdershipsIds;
        private readonly List<int> _leadedTeamsIds;

        public CreateEditUserWindow()
        {
            InitializeComponent();
            this.Title = "Создать участника";

            _isCreatingCall = true;
            _teamsMemdershipsIds = new List<int>();
            _leadedTeamsIds = new List<int>();
        }

        public CreateEditUserWindow(UserView user)
        {
            InitializeComponent();
            this.Title = "Редактировать участника";

            _userId = user.UserId;
            FirstNameTextBox.Text = user.FirstName;
            SurnameTextBox.Text = user.Surname;
            PatronymicTextBox.Text = user.Patronymic;
            UniversityTextBox.Text = user.University;
            GradebookNumberTextBox.Text = user.GradebookNumber;
            CodeForcesLoginTextBox.Text = user.CodeforcesLogin;
            FromVstuCheckBox.IsChecked = user.FromVstu;
            _teamsMemdershipsIds = user.TeamsMemberships.Select(s => s.TeamId).ToList();
            _leadedTeamsIds = user.LeadedTeams.Select(s => s.TeamId).ToList();

            _isCreatingCall = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void OkCreateEditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {           
                var postDto = new CreateUpdateUserDto()
                {
                    FirstName = FirstNameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    Patronymic = PatronymicTextBox.Text,
                    University = UniversityTextBox.Text,
                    GradebookNumber = GradebookNumberTextBox.Text,
                    CodeforcesLogin = CodeForcesLoginTextBox.Text,
                    FromVstu = FromVstuCheckBox.IsChecked ?? false,
                    TeamsMemberships = _teamsMemdershipsIds,
                    LeadedTeams = _leadedTeamsIds
                };
                
                CreateUpdateUserDtoValidator userValidator = new ();
                var result = await userValidator.ValidateAsync(postDto, options => options.ThrowOnFailures());
                if (!result.IsValid) return;


                if (_isCreatingCall)
                {
                    await DataAccess.Storage.CreateUserAsync(postDto);
                    this.Close();
                    return;
                }

                await DataAccess.Storage.UpdateUserAsync(_userId, postDto);
                
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

        private void FromVstuCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (FromVstuCheckBox.IsChecked != null && (bool) FromVstuCheckBox.IsChecked)
            {
                UniversityTextBox.Text = "ВолгГТУ";
            }
        }
    }
}