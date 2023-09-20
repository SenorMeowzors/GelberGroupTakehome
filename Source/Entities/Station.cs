namespace TakeHome.Source.Entities
{
    public class Station
    {
        public List<Passenger> Customers { get; private set; }

        public int StationNumber { get; private set; }

        public Station(int stationNumber)
        {
            Customers = new List<Passenger>();
            StationNumber = stationNumber;
        }

        public void MovePassengers(Train t)
        {
            t.DisembarkPassengers();
            t.EmbarkPassengers(Customers);
        }

    }
}
