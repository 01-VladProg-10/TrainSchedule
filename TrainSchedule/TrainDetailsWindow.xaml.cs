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

namespace TrainSchedule
{
    /// <summary>
    /// Logika interakcji dla klasy TrainDetailsWindow.xaml
    /// </summary>
    public partial class TrainDetailsWindow : Window
    {
        public ObservableCollection<StationDetail> StationDetails { get; set; }

        public TrainDetailsWindow(Train train)
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
            for (int i = 0; i < train.Stations.Length; i++)
            {
                StationDetails.Add(new StationDetail
                {
                    StationName = train.Stations[i],
                    DepartureTime = train.StationsDepartureTime[i],
                    ArrivalTime = train.StationsArrivalTime[i]
                });
            }

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
