using TakeHome.Source.Data;
using TakeHome.Source.Helper;

namespace TakeHome.Source.Entities
{
    public class TrainSimulation
    {
        private List<Passenger> _passengers;
        private List<Train> _trains;
        private List<Train> _trainsToRemove;

        private int _numTrains = 0;
        private int _totalArrivals;

        public int Time { get; set; }
        public TrainSchedule TrainSchedule { get; set; }
        public LinkedList<Station> Stations { get; set; }

        public void OnPassengerArrived(Passenger passenger)
        {
            _totalArrivals++;
        }

        public TrainSimulation(List<Passenger> passengers, TrainSchedule schedule)
        {
            Stations = new LinkedList<Station>();
            TrainSchedule = schedule;

            _passengers = passengers;
            _trains = new List<Train>();
            _trainsToRemove = new List<Train>();

            CreateStations();
        }

        public bool Tick()
        {
            Debug.LogBlank();
            Debug.LogHeader($"t={Time}");

            SpawnPassengers();
            SpawnTrains();
            MoveTrains();


            if (_totalArrivals == _passengers.Count)
            {
                if(Debug.Simple)
                {
                    Console.WriteLine(Time);
                }
                else
                {
                    Debug.Log($"All Passengers arrived. Finished in t = {Time} minutes.");
                    Debug.LogBlank();
                }
                return true;
            }

            RemoveTrains();

            Time++;
            return false;
        }

        public void QueueTrainRemoval(Train train)
        {
            _trainsToRemove.Add(train);
        }


        private void CreateStations()
        {
            int stationNum = 1;
            Station headStation = new Station(stationNum);
            LinkedListNode<Station> firstStation = Stations.AddFirst(headStation);

            LinkedListNode<Station> lastestNode = firstStation;
            for (int i = stationNum; i < TrainSchedule.NumberofStations; i++)
            {
                stationNum++;
                Station station = new Station(stationNum);

                lastestNode = Stations.AddAfter(lastestNode, station);
            }
        }

        private void RemoveTrains()
        {
            foreach(var t in _trainsToRemove)
            {
                _trains.Remove(t);
            }
        }

        private void SpawnPassengers()
        {
            for (int i = 0; i < _passengers.Count; i++)
            {
                Passenger c = _passengers[i];
                if (c.TimeArrived == Time)
                {
                    var node = Stations.First(x => x.StationNumber == c.StartingStation);


                    node.Passengers.Add(c);

                    Debug.Log($"Passenger #{c.ID} arrives at Station {node.StationNumber}. They want to goto Station {c.DestinationStation}.");
                }
            }
        }

        private void MoveTrains()
        {
            for (int i = 0; i < _trains.Count; i++)
            {
                var t = _trains[i];
                if (t != null)
                {
                    t.Tick();
                }
            }
        }

        private void SpawnTrains()
        {
            if (Time % TrainSchedule.DepartFrequency != 0)
            {
                return;
            }

            Train forwardTrain = new Train(this, true, ++_numTrains);
            Train backwardsTrain = new Train(this, false, ++_numTrains);

            _trains.Add(forwardTrain);
            _trains.Add(backwardsTrain);
        }
    }
}
