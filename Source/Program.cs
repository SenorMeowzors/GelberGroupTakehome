using TakeHome.Source.Entities;
using TakeHome.Source.Helper;

namespace TakeHome.Source
{
    class Program
    {
        public static TrainSimulation SetupSimulation(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Debug.Log($"File {fileName} does not exist!");
                return null;
            }

            return Parser.TrainSimulationFromFile(fileName);

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

                Debug.LogBlank();
                Debug.LogHeader("Simulation Begin");
                Debug.LogBlank();

                TrainSimulation simulation = SetupSimulation(fileName);

                if(simulation != null)
                {
                    while(!simulation.Tick())
                    {
                        Console.ReadKey();
                    }
                }

                Debug.LogBlank();
                Debug.LogHeader("Simulation Ended");
                Debug.LogBlank();

            }

            Debug.Log("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
