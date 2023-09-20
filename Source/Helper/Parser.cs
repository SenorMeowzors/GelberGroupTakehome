using TakeHome.Source.Data;
using TakeHome.Source.Entities;
using TakeHome.Source.Entities.Strategies.BoardingStrategies;
using TakeHome.Source.Helper;
using static TakeHome.Source.Entities.Passenger;

namespace TakeHome.Source
{
    public static class Parser
    {
        public static PassengerType ParseCustomer(char type)
        {
            switch (type)
            {
                case 'A':
                    return PassengerType.BoardAnyTrain;
                case 'B':
                    return PassengerType.BoardLessThanHalfFull;
                default:
                    throw new NotImplementedException();
            }
        }

        public static TrainSimulation TrainSimulationFromFile(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string trainHeader = reader.ReadLine();
                TrainSchedule trainSchedule = ParseTrainSchedule(trainHeader);
                List<Passenger> initialPassengers = PopulateCustomers(reader);

                return new TrainSimulation(initialPassengers, trainSchedule);
            }
        }

        private static List<Passenger> PopulateCustomers(StreamReader reader)
        {
            List<Passenger> customerList = new List<Passenger>();

            int customerID = 1;
            string line;
            Debug.LogHeader($"Customers");

            while ((line = reader.ReadLine()) != null)
            {
                string[]? data = line.Split(' ');

                if (data.Length < 1)
                {
                    break;
                }

                Passenger customer = BuildCustomer(customerID, data);

                customerList.Add(customer);

                customerID++;
                Debug.Log($"{customer}");
            }

            return customerList;
        }

        private static Passenger BuildCustomer(int customerID, string[] data)
        {
            PassengerType customerType = ParseCustomer(Convert.ToChar(data[0]));
            BoardingStrategy strategy;
            if (customerType == PassengerType.BoardAnyTrain)
            {
                strategy = new BoardWhenPossible();
            }
            else
            {
                strategy = new BoardWhenLessThanHalfFull();
            }

            var timeArrived = int.Parse(data[1]);
            var destinationStation = int.Parse(data[2]);
            var startingStation = int.Parse(data[3]);

            Passenger customer = new Passenger(customerID, strategy, timeArrived, destinationStation, startingStation);
            return customer;
        }

        private static TrainSchedule ParseTrainSchedule(string trainHeader)
        {
            string[] trainData = trainHeader.Split(' ');

            if (trainData.Length != 4)
            {
                Debug.Log("Train data length unexpected: " + trainData.Length);
            }

            TrainSchedule trainSchedule = new TrainSchedule
            {
                NumberofStations = int.Parse(trainData[0]),
                StationDistance = int.Parse(trainData[1]),
                DepartFrequency = int.Parse(trainData[2]),
                Capacity = int.Parse(trainData[3])
            };
            Debug.LogHeader($"Train Data");

            Debug.Log(trainSchedule);
            return trainSchedule;
        }

    }
}
