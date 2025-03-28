using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TrainSchedule.Models;
using System.Collections.ObjectModel;

namespace TrainSchedule.ViewModels
{

    public class AddTrainViewModel : INotifyPropertyChanged
    {
        private TrainModel _train;
        private string _errorMessage;

        public ObservableCollection<string> Cities { get; } = new ObservableCollection<string>
        {
            "Warszawa", "Krakow", "Lodz", "Wroclaw", "Poznan", "Gdansk", "Szczecin", "Bydgoszcz", "Lublin", "Katowice",
            "Bialystok", "Gdynia", "Czestochowa", "Radom", "Sosnowiec", "Torun", "Kielce", "Gliwice", "Zabrze", "Bytom",
            "Bielsko-Biala", "Olsztyn", "Rybnik", "Prudnik", "Przemysl", "Opole"
        };

        public AddTrainViewModel()
        {
            _train = new TrainModel();
            AddTrainCommand = new RelayCommand(AddTrain, CanAddTrain);
        }

        public TrainModel Train
        {
            get => _train;
            set { _train = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand AddTrainCommand { get; }

        private bool CanAddTrain(object parameter) =>
            !string.IsNullOrWhiteSpace(Train.TrainName) &&
            Cities.Contains(Train.StartStation) &&
            Cities.Contains(Train.FinishStation) &&
            Train.StartStation != Train.FinishStation &&
            !string.IsNullOrWhiteSpace(Train.Departure) &&
            !string.IsNullOrWhiteSpace(Train.Arrival) &&
            decimal.TryParse(Train.Price, out _);

        private void AddTrain(object parameter)
        {
            ErrorMessage = string.Empty;

            if (!Train.Validate(out string validationError))
            {
                ErrorMessage = validationError;
                return;
            }

            if (DatabaseService.SaveTrainToDatabase(Train))
            {
                MessageBox.Show("Pociąg został dodany!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
            else
            {
                ErrorMessage = "Błąd dodawania pociągu do bazy danych.";
            }
        }

        private void ClearFields()
        {
            Train = new TrainModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
