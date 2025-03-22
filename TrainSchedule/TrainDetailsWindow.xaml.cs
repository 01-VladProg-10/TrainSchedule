using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TrainSchedule
{
    /// <summary>
    /// Logika interakcji dla klasy TrainDetailsWindow.xaml
    /// </summary>
    public partial class TrainDetailsWindow : Window
    {
        public ObservableCollection<StationDetail> StationDetails { get; set; }

        public TrainDetailsWindow(TrainModel train)
        {
            InitializeComponent();
            DataContext = train;

            StationDetails = new ObservableCollection<StationDetail>();

            // Добавляем начальную станцию
            StationDetails.Add(new StationDetail
            {
                StationName = train.StartStation,
                DepartureTime = train.Departure,
                ArrivalTime = "-"
            });

            // Добавляем промежуточные станции
            

            // Добавляем конечную станцию
            StationDetails.Add(new StationDetail
            {
                StationName = train.FinishStation,
                DepartureTime = "-",
                ArrivalTime = train.Arrival
            });
        }
    }
}
