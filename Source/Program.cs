namespace TakeHome.Source
{
    internal class Program
    {
        public static void ReadInput(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"File {fileName} does not exist!");
                return;
            }

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;

                string[] trainData = reader.ReadLine().Split(' ');

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

                Console.WriteLine($"Train Data: {trainSchedule.ToString()}");

                List<Customer> customerList = new List<Customer>();

                while ((line = reader.ReadLine()) != null)
                {

                    string[]? data = line.Split(' ');

                    if (data.Length < 1)
                    {
                        return;
                    }

                    Customer.CustomerType customerType = Customer.ParseCustomer(Convert.ToChar(data[0]));

                    Customer customer = new Customer
                    {
                        customerType = customerType,
                        timeArrived = int.Parse(data[1]),
                        destinationStation = int.Parse(data[2]),
                        startingStation = int.Parse(data[3])
                    };

                    customerList.Add(customer);
                    Console.WriteLine($"Customer Data: {customer}");
                }
            }
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

                ReadInput(fileName);
            }


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
