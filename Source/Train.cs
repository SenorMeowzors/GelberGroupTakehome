namespace TakeHome.Source
{
    public class Train
    {
        public int currentStation;
        public List<Customer> currentCustomers = new List<Customer>();
        public bool _forward;
        public int capacity;
        public int departFrequency;
        public int trainSpeed = 1;
        public string TrainName
        {
            get
            {
                if(_forward)
                {
                    return "Forward Train";
                }
                return "Backwards Train";
            }
        }
        public bool Finished
        {
            get
            {
                if(_forward)
                {
                    return currentStation == trainSimulation.trainSchedule.numberofStations;
                }

                return currentStation == 0;
            }
        }
        TrainSimulation trainSimulation;

        public Train(TrainSimulation simulation, bool forward)
        {
            this._forward = forward;
            if(_forward)
            {
                currentStation = 0;
            }
            else
            {
                currentStation = simulation.trainSchedule.numberofStations;
            }
            capacity = simulation.trainSchedule.capacity;
            departFrequency  = simulation.trainSchedule.departFrequency;
            trainSimulation = simulation;
        }

        public void Tick(List<Customer> customers)
        {

            if (_forward)
            {
                if (currentStation >= trainSimulation.trainSchedule.numberofStations)
                {
                    return;
                }
                currentStation += trainSpeed;
            }
            else
            {
                if (currentStation <= 0)
                {
                    return;
                }
                currentStation -= trainSpeed;
            }

            OffloadCustomers();
            OnBoardCustomers(customers);
        }

        public void OnBoardCustomers(List<Customer> customers)
        {
            for (int i = 0; i < customers.Count; i++)
            {
                Customer c = customers[i];
                if (c.startingStation == currentStation)
                {
                    c.BoardTrain(this);
                }
            }
        }

        private void OffloadCustomers()
        {
            for (int i = 0; i < currentCustomers.Count; i++)
            {
                Customer c = currentCustomers[i];
                if (c.destinationStation == currentStation)
                {
                    currentCustomers.RemoveAt(i);
                    Console.WriteLine(c.customerID + $" arrived at {currentStation}");
                }
            }
        }

        public void BoardCustomer(Customer customer)
        {
            if(currentCustomers.Count >= capacity)
            {
                return;
            }
            currentCustomers.Add(customer);
        }
    }
}
