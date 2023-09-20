using TakeHome.Source.BoardingStrategies;
using TakeHome.Source.Data;
using TakeHome.Source.Entities;
using static TakeHome.Source.Entities.Customer;

namespace TakeHome.Source
{
    internal class Program
    {
        public static CustomerType ParseCustomer(char type)
        {
            switch (type)
            {
                case 'A':
                    return CustomerType.BoardAnyTrain;
                case 'B':
                    return CustomerType.BoardLessThanHalfFull;
                default:
                    throw new Exception(); //TOOD CHANGE
                    return CustomerType.BoardAnyTrain;
            }
        }
        public static TrainSimulation SetupSimulation(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Debug.Log($"File {fileName} does not exist!");
                return null;
            }

            using (StreamReader reader = new StreamReader(fileName))
            {
                string trainHeader = reader.ReadLine();
                TrainSchedule trainSchedule = ReadTrainSchedule(trainHeader);

                List<Customer> customerList = PopulateCustomers(reader);

                return new TrainSimulation(customerList, trainSchedule);
            }

        }

        private static List<Customer> PopulateCustomers(StreamReader reader)
        {
            List<Customer> customerList = new List<Customer>();

            int customerID = 1;
            string line;
            Debug.Log($"##############  Customers  ##############");

            while ((line = reader.ReadLine()) != null)
            {
                string[]? data = line.Split(' ');

                if (data.Length < 1)
                {
                    break;
                }

                CustomerType customerType = ParseCustomer(Convert.ToChar(data[0]));
                BoardingStrategy strategy = null;
                if (customerType == CustomerType.BoardAnyTrain)
                {
                    strategy = new BoardWhenPossible();
                }
                else
                {
                    strategy = new BoardWhenLessThanHalfFull();
                }

                Customer customer = new Customer();
                strategy.Customer = customer;

                customer.customerID = customerID;
                customer.boardingStrategy = strategy;
                customer.timeArrived = int.Parse(data[1]);
                customer.destinationStation = int.Parse(data[2]);
                customer.startingStation = int.Parse(data[3]);

                customerList.Add(customer);
                customerID++;
                Debug.Log($"{customer}");
            }

            return customerList;
        }

        private static TrainSchedule ReadTrainSchedule(string trainHeader)
        {
            string[] trainData = trainHeader.Split(' ');

            if (trainData.Length != 4)
            {
                Debug.Log("Train data length unexpected: " + trainData.Length);
            }

            TrainSchedule trainSchedule = new TrainSchedule
            {
                numberofStations = int.Parse(trainData[0]),
                stationDistance = int.Parse(trainData[1]),
                departFrequency = int.Parse(trainData[2]),
                capacity = int.Parse(trainData[3])
            };
            Debug.Log($"##############  Train Data  ##############");

            Debug.Log(trainSchedule);
            return trainSchedule;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Debug.Log("Place a .txt file at .exe path and enter the file name and press enter. Leave blank to exit");

                string fileName = Console.ReadLine();

                if (fileName == null || fileName.Length < 1)
                {
                    break;
                }

                TrainSimulation simulation = SetupSimulation(fileName);

                if(simulation != null)
                {
                    while(!simulation.Tick())
                    {
                        Console.ReadKey();

                    }
                }
            }

            Debug.Log("Press any key to exit...");
            Console.ReadKey();
        }
    }

    public static class Debug
    {
        public static void Log<T>(T message) { Console.WriteLine(message);}
        public static void LogWarning<T>(T message) { }// Console.WriteLine($"@->{message}");}

    }
}
