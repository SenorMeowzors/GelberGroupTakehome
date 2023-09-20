using System.Linq;
using TakeHome.Source.Data;

namespace TakeHome.Source.Entities
{
    public class TrainSimulation
    {
        List<Customer> _customers;
        public TrainSchedule trainSchedule;

        List<Train> _trains = new List<Train>();
        List<Train> _trainsToRemove = new List<Train>();

        public LinkedList<Station> stations = new LinkedList<Station>();

        public int time = 0;
        int _numTrains = 0;
        int _totalArrivals;

        public void OnCustomerArrived(Customer customer)
        {
            _totalArrivals++;
        }

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


            if (_totalArrivals == _customers.Count)
            {
                Debug.Log($"All customers arrived. Finished in t = {time} minutes");
                Debug.Log("");
                return true;
            }

            RemoveTrains();

            time++;
            return false;
        }

        private void RemoveTrains()
        {
            foreach(var t in _trainsToRemove)
            {
                _trains.Remove(t);
            }
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
            for (int i = 0; i < _trains.Count; i++)
            {
                var t = _trains[i];
                if(t != null)
                {
                    t.Tick(_customers);

                }
            }
        }

        private void SpawnTrains()
        {
            if (time % trainSchedule.departFrequency != 0)
            {
                return;
            }
            Train forwardTrain = new Train(this, true, ++_numTrains);
            Train backwardsTrain = new Train(this, false, ++_numTrains);

            _trains.Add(forwardTrain);
            _trains.Add(backwardsTrain);
        }

        public void QueueTrainRemoval(Train train)
        {
            _trainsToRemove.Add(train);
        }
    }
}
