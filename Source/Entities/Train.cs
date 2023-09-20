namespace TakeHome.Source.Entities
{
    public class Train
    {
        public LinkedListNode<Station> currentStation;
        public List<Customer> currentCustomers = new List<Customer>();
        public bool _forward;
        public int capacity;
        public int departFrequency;
        public int trainID;

        public int distanceToNextStation;
        public string TrainName
        {
            get
            {
                if (_forward)
                {
                    return $"Forward Train {trainID}";
                }
                return $"Backwards Train {trainID}";
            }
        }
        public bool Finished
        {
            get
            {
                if (_forward)
                {
                    return currentStation.Value.stationNumber == trainSimulation.trainSchedule.numberofStations;
                }

                return currentStation.Value.stationNumber == 0;
            }
        }
        TrainSimulation trainSimulation;

        public Train(TrainSimulation simulation, bool forward, int trainID)
        {
            _forward = forward;
            this.trainID = trainID;
            if (_forward)
            {
                currentStation = simulation.stations.First;
            }
            else
            {
                currentStation = simulation.stations.Last;
            }

            trainSimulation = simulation;
            capacity = simulation.trainSchedule.capacity;
            departFrequency = simulation.trainSchedule.departFrequency;
            distanceToNextStation = simulation.trainSchedule.stationDistance;
            Debug.Log($"{TrainName} Spawns at {currentStation.Value.stationNumber}");

        }
        public void Tick(List<Customer> customers)
        {
            if (distanceToNextStation == trainSimulation.trainSchedule.stationDistance)
            {
                currentStation.Value.MoveCustomers(this);
            }

            distanceToNextStation--;
            if (distanceToNextStation > 0)
            {
                Debug.LogWarning($"{TrainName} has {distanceToNextStation} left to goto next station");
                return;
            }

            if (_forward)
            {
                currentStation = currentStation.Next;
            }
            else
            {
                currentStation = currentStation.Previous;
            }

            if (currentStation == null)
            {
                trainSimulation.RemoveTrain(this);
                return;
            }

            distanceToNextStation = trainSimulation.trainSchedule.stationDistance;
            Debug.Log($"{TrainName} just moved to {currentStation.Value.stationNumber}");

        }

        public bool IsGoingTowardsDestination(Customer customer)
        {

            int destination = customer.destinationStation;

            if (currentStation.Value.stationNumber == destination)
            {
                //do not board
                return false;
            }

            if (_forward)
            {
                return currentStation.Value.stationNumber < destination;
            }
            else
            {
                return currentStation.Value.stationNumber > destination;
            }
        }

        public void OnBoardCustomers(List<Customer> customers)
        {
            if (trainID == 4)
            {
                int e = 0;
                e++;
            }

            customers = customers.OrderByDescending(c => c.boardingStrategy.Priority).ThenByDescending(c => Math.Abs(currentStation.Value.stationNumber - c.destinationStation)).ToList();
            for (int i = 0; i < customers.Count; i++)
            {
                Customer c = customers[i];

                if (IsGoingTowardsDestination(c))
                {
                    c.BoardTrain(this);
                }
            }
        }

        public void OffloadCustomers()
        {
            for (int i = 0; i < currentCustomers.Count; i++)
            {
                Customer c = currentCustomers[i];
                if (c.destinationStation == currentStation.Value.stationNumber)
                {
                    currentCustomers.RemoveAt(i);
                    currentStation.Value.customers.Add(c);
                    Debug.Log($"Train {TrainName} arrives at {currentStation.Value.stationNumber} Passenger #{c.customerID} departs this train.");

                    trainSimulation.Arrivals++;
                }
            }
        }

        public void BoardCustomer(Customer customer)
        {
            if (currentCustomers.Count >= capacity)
            {
                return;
            }
            Debug.Log($"Train {TrainName} arrives at station {currentStation.Value.stationNumber} Passenger #{customer.customerID} boards this train.");

            currentStation.Value.customers.Remove(customer);
            currentCustomers.Add(customer);
        }
    }
}
