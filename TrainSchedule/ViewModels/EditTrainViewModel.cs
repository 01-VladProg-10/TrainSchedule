using System.Collections.ObjectModel;
using System.ComponentModel;
using TrainSchedule.Models;
using TrainSchedule.ViewModels;

public class EditTrainViewModel : INotifyPropertyChanged
{
    private TrainModel _selectedTrain;
    private ObservableCollection<string> _cities;
    private string _errorMessage;

    public ObservableCollection<string> Cities { get; set; } = new ObservableCollection<string>
    {
        "Warszawa", "Krakow", "Lodz", "Wroclaw", "Poznan", "Gdansk", "Szczecin", "Bydgoszcz", "Lublin", "Katowice",
        "Bialystok", "Gdynia", "Czestochowa", "Radom", "Sosnowiec", "Torun", "Kielce", "Gliwice", "Zabrze", "Bytom",
        "Bielsko-Biala", "Olsztyn", "Rybnik", "Prudnik", "Przemysl", "Opole"
     };

    public TrainModel SelectedTrain
    {
        get => _selectedTrain;
        set
        {
            if (_selectedTrain != value)
            {
                _selectedTrain = value;
                OnPropertyChanged(nameof(SelectedTrain));
            }
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (_errorMessage != value)
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
    }

    public RelayCommand SaveTrainCommand { get; private set; }

    public EditTrainViewModel(TrainModel selectedTrain)
    {
        SelectedTrain = selectedTrain;
        SaveTrainCommand = new RelayCommand(SaveTrain);
    }

    private void SaveTrain(object parameter)
    {
        // Logika zapisywania zmian do bazy danych lub innej logiki
        if (string.IsNullOrWhiteSpace(SelectedTrain.TrainName))
        {
            ErrorMessage = "Nazwa pociągu jest wymagana.";
            return;
        }

        // Przykład zapisu
        bool success = DatabaseService.UpdateTrain(SelectedTrain);

        if (success)
        {
            ErrorMessage = "Pociąg został zapisany.";
        }
        else
        {
            ErrorMessage = "Wystąpił problem podczas zapisywania.";
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
