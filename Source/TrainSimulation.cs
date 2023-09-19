using System.Linq;
namespace TakeHome.Source
{
    public class TrainSimulation
    {
        List<Customer> _customers;
        public TrainSchedule trainSchedule;

        List<Train> trains = new List<Train>();
        public LinkedList<Station> stations = new LinkedList<Station>();

        public int time = 0;
        int numTrains = 0;
        public TrainSimulation(List<Customer> customersList, TrainSchedule schedule)
        {
            _customers = customersList;
            trainSchedule = schedule;

            int stationNum = 1;
            Station headStation = new Station(stationNum);
            LinkedListNode<Station> firstStation = stations.AddFirst(headStation);
            for(int i =0 ; i < trainSchedule.numberofStations-1; i++)
            {
                stationNum++;
                Station station = new Station(stationNum);

                stations.AddAfter(firstStation, station);
            }
        }

        public bool Tick()
        {
            Console.WriteLine($"##############  t={time}  ##############");

            SpawnCustomers();
            SpawnTrains();
            MoveTrains();


            time++;
            if (trains.All(e => e.Finished))
            {
                Console.WriteLine($"Finished in t = " + time);
                return true;
            }

            return false;
        }

        private void SpawnCustomers()
        {
            for (int i = 0; i < _customers.Count; i++)
            {
                Customer c = _customers[i];
                if (c.timeArrived == time)
                {
                    var node = stations.First(x => x.stationNumber == c.startingStation);

                    
                    node.customers.Add(c);

                    Console.WriteLine($"Customer {c.customerID} arrived at {node.stationNumber}");
                }
            }
        }

        void MoveTrains()
        {
            for(int i = 0; i <  trains.Count; i++)
            {
                var t = trains[i];

                t.Tick(_customers);
            }
        }

        private void SpawnTrains()
        {
            if(time % trainSchedule.departFrequency != 0)
            {
                return;
            }
            trains.Add(new Train(this, true, ++numTrains));
            trains.Add(new Train(this, false, ++numTrains));
        }

        public void RemoveTrain(Train train)
        {
            if(trains.Contains(train))
            {
                //Console.WriteLine($"{train.TrainName} despawned");
                trains.Remove(train);
            }
        }
    }
}
