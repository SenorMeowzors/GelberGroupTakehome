namespace TakeHome.Source
{
    public struct TrainSchedule
    {
        public int numberofStations;
        public int stationDistance;
        public int departFrequency;
        public int capacity;

        public override string ToString()
        {
            return $"Number of Stations: {numberofStations}\n" +
                $"Station Distance: {stationDistance}\n" +
                $"Depart Frequency: {departFrequency}\n" +
                $"Capacity: {capacity}\n";
        }
    }

}
