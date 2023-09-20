using static TakeHome.Source.Customer;

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
                Console.WriteLine($"File {fileName} does not exist!");
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
            Console.WriteLine($"##############  Customers  ##############");

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
                Console.WriteLine($"{customer}");
            }

            return customerList;
        }

        private static TrainSchedule ReadTrainSchedule(string trainHeader)
        {
            string[] trainData = trainHeader.Split(' ');

            if (trainData.Length != 4)
            {
                Console.WriteLine("Train data length unexpected: " + trainData.Length);
            }

            TrainSchedule trainSchedule = new TrainSchedule
            {
                numberofStations = int.Parse(trainData[0]),
                stationDistance = int.Parse(trainData[1]),
                departFrequency = int.Parse(trainData[2]),
                capacity = int.Parse(trainData[3])
            };
            Console.WriteLine($"##############  Train Data  ##############");

            Console.WriteLine(trainSchedule);
            return trainSchedule;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Place a .txt file at .exe path and enter the file name and press enter. Leave blank to exit");

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

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
