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

        public TrainSearchWindow()
        {
            InitializeComponent();
            TravelDatePicker.SelectedDate = DateTime.Now;
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
            DateTime date = TravelDatePicker.SelectedDate ?? DateTime.Now;

            var results = new List<Train>
        {
            new Train
            {
                TrainName = "IC 12345",
                StartStation = "Варшава",
                FinishStation = "Краков",
                Departure = "16:00",
                Arrival = "18:30",
                Price = "50 PLN",
                Stations = new[] { "Лодзь", "Катовице" },
                StationsDepartureTime = new[] { "16:45", "17:30" },
                StationsArrivalTime = new[] { "16:50", "17:35" }
            },
            new Train
            {
                TrainName = "TLK 67890",
                StartStation = "Варшава",
                FinishStation = "Краков",
                Departure = "17:10",
                Arrival = "19:40",
                Price = "45 PLN",
                Stations = new[] { "Лодзь", "Катовице" },
                StationsDepartureTime = new[] { "17:55", "18:40" },
                StationsArrivalTime = new[] { "18:00", "18:45" }
            }
        };

            TrainListView.ItemsSource = results;
        }

        private void TrainListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TrainListView.SelectedItem is Train selectedTrain)
            {
                var detailsWindow = new TrainDetailsWindow(selectedTrain);
                detailsWindow.Show();
            }
        }

        private void TrainListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    public class TrainRoute
    {
        public string TrainName { get; set; }
        public string StartStation { get; set; }
        public string FinishStation { get; set; }
        public string[] StationsDepartureTime { get; set; }
        public string[] StationsArrivalTime { get; set; }
        public string[] Stations { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Price { get; set; }
    }

}
