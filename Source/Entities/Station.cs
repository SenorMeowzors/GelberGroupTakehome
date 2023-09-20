namespace TakeHome.Source.Entities
{
    public class Station
    {
        public List<Passenger> Passengers { get; private set; }

        public int StationNumber { get; private set; }

        public Station(int stationNumber)
        {
            Passengers = new List<Passenger>();
            StationNumber = stationNumber;
        }

        public void MovePassengers(Train t)
        {
            t.DisembarkPassengers();
            t.EmbarkPassengers(Passengers);
        }

    }
}
