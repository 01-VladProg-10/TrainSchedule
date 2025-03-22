using System;

namespace TrainSchedule.Models
{
    public class TrainModel
    {
        public string TrainName { get; set; }
        public string StartStation { get; set; }
        public string FinishStation { get; set; }
        public StationDetail[] Stations { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Price { get; set; }
    }
}