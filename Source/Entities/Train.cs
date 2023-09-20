using TakeHome.Source.Helper;

namespace TakeHome.Source.Entities
{
    public class Train
    {
        public LinkedListNode<Station> CurrentStation { get; private set; }
        public List<Passenger> CurrentPassengers { get; private set; }

        public bool Forward { get; private set; }
        public int Capacity { get; private set; }
        public int DepartFrequency { get; private set; }
        public int ID { get; private set; }

        int _distanceToNextStation;

        TrainSimulation trainSimulation;

        public string TrainName
        {
            get
            {
                if (Forward)
                {
                    return $"Forward Train {ID}";
                }
                return $"Backwards Train {ID}";
            }
        }

        public Train(TrainSimulation simulation, bool forward, int trainID)
        {
            CurrentPassengers = new List<Passenger>();

            Forward = forward;
            ID = trainID;
            if (Forward)
            {
                CurrentStation = simulation.Stations.First;
            }
            else
            {
                CurrentStation = simulation.Stations.Last;
            }

            trainSimulation = simulation;
            Capacity = simulation.TrainSchedule.Capacity;
            DepartFrequency = simulation.TrainSchedule.DepartFrequency;
            _distanceToNextStation = simulation.TrainSchedule.StationDistance;
            Debug.Log($"{TrainName} Spawns at {CurrentStation.Value.StationNumber}");

        }

        public void Tick(List<Passenger> customers)
        {
            if (_distanceToNextStation == trainSimulation.TrainSchedule.StationDistance)
            {
                CurrentStation.Value.MovePassengers(this);
            }

            _distanceToNextStation--;
            if (_distanceToNextStation > 0)
            {
                Debug.LogWarning($"{TrainName} has {_distanceToNextStation} left to goto next station");
                return;
            }

            AdvanceTrain();
        }

        public bool IsGoingTowardsDestination(Passenger customer)
        {
            int destination = customer.DestinationStation;

            if (CurrentStation.Value.StationNumber == destination)
            {
                //do not board
                return false;
            }

            if (Forward)
            {
                return CurrentStation.Value.StationNumber < destination;
            }
            else
            {
                return CurrentStation.Value.StationNumber > destination;
            }
        }

        public void EmbarkPassengers(List<Passenger> passengers)
        {
            passengers = PrioritizeByStrategyAndDistance(passengers);

            for (int i = 0; i < passengers.Count; i++)
            {
                Passenger c = passengers[i];

                if (IsGoingTowardsDestination(c))
                {
                    c.BoardTrain(this);
                }
            }
        }

        public void DisembarkPassengers()
        {
            for (int i = 0; i < CurrentPassengers.Count; i++)
            {
                Passenger c = CurrentPassengers[i];
                if (c.DestinationStation == CurrentStation.Value.StationNumber)
                {
                    CurrentPassengers.RemoveAt(i);
                    CurrentStation.Value.Customers.Add(c);

                    Debug.Log($"Train {TrainName} arrives at {CurrentStation.Value.StationNumber} Passenger #{c.ID} departs this train.");

                    trainSimulation.OnCustomerArrived(c);
                }
            }
        }

        public void Board(Passenger customer)
        {
            if (CurrentPassengers.Count >= Capacity)
            {
                return;
            }

            Debug.Log($"Train {TrainName} arrives at station {CurrentStation.Value.StationNumber} Passenger #{customer.ID} boards this train.");

            CurrentStation.Value.Customers.Remove(customer);
            CurrentPassengers.Add(customer);
        }

        private void AdvanceTrain()
        {
            if (Forward)
            {
                CurrentStation = CurrentStation.Next;
            }
            else
            {
                CurrentStation = CurrentStation.Previous;
            }

            if (CurrentStation == null)
            {
                trainSimulation.QueueTrainRemoval(this);
                return;
            }

            _distanceToNextStation = trainSimulation.TrainSchedule.StationDistance;
            Debug.Log($"{TrainName} just moved to {CurrentStation.Value.StationNumber}");
        }

        private List<Passenger> PrioritizeByStrategyAndDistance(List<Passenger> passengers)
        {

            return passengers.OrderByDescending(c => c.BoardingStrategy.Priority)
                                   .ThenByDescending(GetDistanceToDestination)
                                   .ToList();
        }

        private int GetDistanceToDestination(Passenger c)
        {
            return Math.Abs(CurrentStation.Value.StationNumber - c.DestinationStation);
        }

    }
}
