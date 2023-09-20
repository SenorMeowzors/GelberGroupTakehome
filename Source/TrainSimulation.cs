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
        public int Arrivals;

        public TrainSimulation(List<Customer> customersList, TrainSchedule schedule)
        {
            _customers = customersList;
            trainSchedule = schedule;

            int stationNum = 1;
            Station headStation = new Station(stationNum);
            LinkedListNode<Station> firstStation = stations.AddFirst(headStation);

            LinkedListNode<Station> lastestNode = firstStation;
            for (int i = stationNum; i < trainSchedule.numberofStations; i++)
            {
                stationNum++;
                Station station = new Station(stationNum);

                lastestNode = stations.AddAfter(lastestNode, station);
            }
        }

        public bool Tick()
        {
            Debug.Log($"##############  t={time}  ##############");

            SpawnCustomers();
            SpawnTrains();
            MoveTrains();


            if (Arrivals == _customers.Count)
            {
                Debug.Log($"All customers arrived. Finished in t = {time} minutes");
                Debug.Log("");
                return true;
            }

            time++;
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

                    Debug.Log($"Customer {c.customerID} arrives at {node.stationNumber}. They want to goto {c.destinationStation}");
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
                //Debug.Log($"{train.TrainName} despawned");
                trains.Remove(train);
            }
        }
    }
}
