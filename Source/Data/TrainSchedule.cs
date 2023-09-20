namespace TakeHome.Source.Data
{
    public struct TrainSchedule
    {
        public int NumberofStations { get; set; }
        public int StationDistance { get; set; }
        public int DepartFrequency { get; set; }
        public int Capacity { get; set; }

        public override string ToString()
        {
            return $"Number of Stations: {NumberofStations}\n" +
                $"Station Distance: {StationDistance}\n" +
                $"Depart Frequency: {DepartFrequency}\n" +
                $"Capacity: {Capacity}\n";
        }
    }

}
