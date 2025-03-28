using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrainSchedule.Models;
using TrainSchedule.ViewModels;

namespace TrainSchedule
{
    /// <summary>
    /// Logika interakcji dla klasy TrainSearchWindow.xaml
    /// </summary>
    public partial class TrainSearchWindow : Window
    {
        private int selectedHour = 0;
        private int selectedMinute = 0;
        private List<string> polishCities = new List<string>
        {
        "Warszawa", "Krakow", "Lodz", "Wroclaw", "Poznan", "Gdansk", "Szczecin", "Bydgoszcz", "Lublin", "Katowice",
        "Bialystok", "Gdynia", "Czestochowa", "Radom", "Sosnowiec", "Torun", "Kielce", "Gliwice", "Zabrze", "Bytom",
        "Bielsko-Biala", "Olsztyn", "Rybnik", "Prudnik", "Przemysl", "Opole"
        };
        public bool IsAdmin { get; private set; }

        public TrainSearchWindow()
        {
            InitializeComponent();
            TravelDatePicker.SelectedDate = DateTime.Now;

            var user = DatabaseService.LoadUserFromConfig();
            if (user != null)
            {
                UserNameTextBlock.Text = $"{user.FirstName} {user.LastName}";
                UserInitialTextBlock.Text = user.FirstName.Substring(0, 1).ToUpper();

                IsAdmin = user.IsAdmin;  // Ustawienie wartości dla bindowania
                DataContext = this;       // Ustawienie kontekstu dla bindowania w XAML

                if (user.IsAdmin)
                {
                    AddTrainButton.Visibility = Visibility.Visible;
                }
            }
        }
        private void AddTrainButton_Click(object sender, RoutedEventArgs e)
        {
            // Tworzymy nowe okno AddTrainWindow
            var addTrainWindow = new AddTrainWindow();

            // Pokazujemy okno
            addTrainWindow.Show();
        }

        private void DeleteTrainButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz wybrany pociąg z ListView (lub innego kontrolera, z którego korzystasz)
            var selectedTrain = TrainListView.SelectedItem as TrainModel;

            if (selectedTrain != null)
            {
                // Potwierdzenie przed usunięciem
                var result = MessageBox.Show("Czy na pewno chcesz usunąć ten pociąg?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // Usunięcie pociągu z bazy danych
                    bool success = DatabaseService.DeleteTrain(selectedTrain.TrainName);

                    if (success)
                    {
                        MessageBox.Show("Pociąg został usunięty z bazy danych.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Odświeżenie listy pociągów (np. wywołanie ponownie metody Search)
                        string from = FromComboBox.Text;
                        string to = ToComboBox.Text;
                        string selectedTime = TimeButton.Content.ToString();
                        var results = DatabaseService.GetTrains(from, to, selectedTime);
                        TrainListView.ItemsSource = results;
                    }
                    else
                    {
                        MessageBox.Show("Wystąpił problem podczas usuwania pociągu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano pociągu do usunięcia.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditTrainButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected train from the ListView
            var selectedTrain = TrainListView.SelectedItem as TrainModel;

            if (selectedTrain != null)
            {
                // Open the EditTrainWindow and pass the selected train to it
                EditTrainWindow editWindow = new EditTrainWindow
                {
                    DataContext = new EditTrainViewModel(selectedTrain) // Bind the selected train to the ViewModel
                };
                editWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nie wybrano pociągu do edytowania.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ShowTimePopup_Click(object sender, RoutedEventArgs e)
        {
            TimePopup.IsOpen = true;
        }

        private void IncreaseHour_Click(object sender, RoutedEventArgs e)
        {
            selectedHour = (selectedHour + 1) % 24;
            HourText.Text = selectedHour.ToString("D2");
        }

        private void DecreaseHour_Click(object sender, RoutedEventArgs e)
        {
            selectedHour = (selectedHour - 1 + 24) % 24;
            HourText.Text = selectedHour.ToString("D2");
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            // Ustawienie nowego okna głównego
            MainWindow mainWindow = new MainWindow();

            // Ustawienie nowego okna jako głównego okna aplikacji
            Application.Current.MainWindow = mainWindow;

            // Zamknięcie bieżącego okna (TrainSearchWindow)
            this.Close();

            // Wyświetlenie nowego okna głównego
            mainWindow.Show();
        }



        private void IncreaseMinute_Click(object sender, RoutedEventArgs e)
        {
            selectedMinute = (selectedMinute + 15) % 60;
            MinuteText.Text = selectedMinute.ToString("D2");
        }

        private void DecreaseMinute_Click(object sender, RoutedEventArgs e)
        {
            selectedMinute = (selectedMinute - 15 + 60) % 60;
            MinuteText.Text = selectedMinute.ToString("D2");
        }

        private void ConfirmTime_Click(object sender, RoutedEventArgs e)
        {
            TimeButton.Content = $"{selectedHour:D2}:{selectedMinute:D2}";
            TimePopup.IsOpen = false;
        }

        private void FromComboBox_DropDownOpened(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null) return;

            if (string.IsNullOrWhiteSpace(comboBox.Text))
            {
                comboBox.ItemsSource = polishCities;
            }
        }

        private void ToComboBox_DropDownOpened(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null) return;

            if (string.IsNullOrWhiteSpace(comboBox.Text))
            {
                comboBox.ItemsSource = polishCities;
            }
        }

        private void FromComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null) return;

            string searchText = comboBox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                comboBox.ItemsSource = polishCities;
                return;
            }

            var filteredCities = polishCities
                .Where(city => city.ToLower().Contains(searchText))
                .ToList();

            comboBox.ItemsSource = filteredCities;
            comboBox.IsDropDownOpen = true;
        }

        private void ToComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null) return;

            string searchText = comboBox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                comboBox.ItemsSource = polishCities;
                return;
            }

            var filteredCities = polishCities
                .Where(city => city.ToLower().Contains(searchText))
                .ToList();

            comboBox.ItemsSource = filteredCities;
            comboBox.IsDropDownOpen = true;
        }

        private void FromComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox?.SelectedItem != null)
            {
                comboBox.Text = comboBox.SelectedItem.ToString();
            }
        }

        private void ToComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox?.SelectedItem != null)
            {
                comboBox.Text = comboBox.SelectedItem.ToString();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string from = FromComboBox.Text;
            string to = ToComboBox.Text;
            string selectedTime = TimeButton.Content.ToString();

            var results = DatabaseService.GetTrains(from, to, selectedTime);
            TrainListView.ItemsSource = results;
        }



        private void TrainListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TrainListView.SelectedItem is TrainModel selectedTrain)
            {
                var detailsWindow = new TrainDetailsWindow(selectedTrain);
                detailsWindow.Show();
            }
        }

        private void TrainListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
