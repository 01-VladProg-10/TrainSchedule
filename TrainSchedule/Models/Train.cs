using System;

namespace TrainSchedule
{
    public class Train
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