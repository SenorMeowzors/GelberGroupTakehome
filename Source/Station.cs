namespace TakeHome.Source
{
    public class Station
    {
        public List<Customer> customers = new List<Customer>();

        public int stationNumber;

        public Station(int  stationNumber)
        {
            this.stationNumber = stationNumber;
        }

        public void MoveCustomers(Train t)
        {
            t.OffloadCustomers();
            t.OnBoardCustomers(customers);
        }

    }
}
