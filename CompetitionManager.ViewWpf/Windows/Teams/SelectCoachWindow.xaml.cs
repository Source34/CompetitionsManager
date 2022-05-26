using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CompetitionManager.OpenAPIsConnector;
using CompetitionManager.ViewWpf.Data;
using CompetitionManager.ViewWpf.EntityViews.Coach;

namespace CompetitionManager.ViewWpf.Windows.Teams
{
    /// <summary>
    /// Логика взаимодействия для SelectCoachWindow.xaml
    /// </summary>
    public partial class SelectCoachWindow : Window
    {
        public CoachDto? SelectedCoach { get; set; }
        private readonly List<CoachDto> _coachDtos;
        public SelectCoachWindow()
        {
            InitializeComponent();
            _coachDtos = DataAccess.Storage.GetAllCoachesAsync().Result.ToList();
            CoachesDataGrid.ItemsSource = CoachView.ConvertToView(_coachDtos);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (CoachesDataGrid.SelectedItem != null)
            {
                var item = (CoachView)CoachesDataGrid.SelectedItem;

                SelectedCoach = _coachDtos
                    .FirstOrDefault(p => p.CoachId == item.CoachId);
                this.Close();
            }
            else
            {
                 MessageBox.Show("Выберите тренера!", "Внимание!", 
                                  MessageBoxButton.OK, MessageBoxImage.Information);
            }
               
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
