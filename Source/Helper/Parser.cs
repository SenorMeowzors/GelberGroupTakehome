using TakeHome.Source.Data;
using TakeHome.Source.Entities;
using TakeHome.Source.Entities.Strategies.BoardingStrategies;
using TakeHome.Source.Helper;
using static TakeHome.Source.Entities.Passenger;

namespace TakeHome.Source
{
    public static class Parser
    {
        public static PassengerType ParsePassenger(char type)
        {
            switch (type)
            {
                case 'A':
                    return PassengerType.BoardAnyTrain;
                case 'B':
                    return PassengerType.BoardLessThanHalfFull;
                default:
                    Debug.Log($"Invalid Passenger Type: {type}");
                    throw new NotImplementedException();
            }
        }

        public static TrainSimulation TrainSimulationFromFile(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string trainHeader = reader.ReadLine();
                try
                {
                    TrainSchedule trainSchedule = ParseTrainSchedule(trainHeader);
                    List<Passenger> initialPassengers = PopulatePassengers(reader);
                    return new TrainSimulation(initialPassengers, trainSchedule);
                }
                catch (Exception ex)
                {
                    Debug.LogHeader("Error", true);
                    Debug.Log(ex.Message, true);
                    Debug.Log($"Failed to read file: {fileName}", true);
                    Debug.LogHeader("Error", true);
                    return null;
                }
            }
        }

        private static List<Passenger> PopulatePassengers(StreamReader reader)
        {
            List<Passenger> passengersList = new List<Passenger>();

            int passengerID = 1;
            string line;
            Debug.LogHeader($"Customers");

            while ((line = reader.ReadLine()) != null)
            {
                string[]? data = line.Split(' ');

                if (data.Length < 1)
                {
                    break;
                }

                Passenger passenger = BuildPassenger(passengerID, data);

                passengersList.Add(passenger);

                passengerID++;
                Debug.Log($"{passenger}");
            }

            return passengersList;
        }

        private static Passenger BuildPassenger(int passengerID, string[] data)
        {
            PassengerType type = ParsePassenger(Convert.ToChar(data[0]));
            BoardingStrategy strategy;
            if (type == PassengerType.BoardAnyTrain)
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

            return new Passenger(passengerID, strategy, timeArrived, destinationStation, startingStation);
        }

        private static TrainSchedule ParseTrainSchedule(string trainHeader)
        {
            if(trainHeader == null)
            {
                throw new ArgumentNullException(nameof(trainHeader));
            }
            string[] trainData = trainHeader.Split(' ');

            if (trainData.Length != 4)
            {
                Console.WriteLine("Train data length unexpected: " + trainData.Length);
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
