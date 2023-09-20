using TakeHome.Source.Entities;
using TakeHome.Source.Helper;

namespace TakeHome.Source
{
    class Program
    {
        public static TrainSimulation SetupSimulation(string fileName)
        {
            var path = fileName;
            if (!File.Exists(path))
            {
                Debug.Log($"{path} does not exist!");
                return null;
            }

            return Parser.TrainSimulationFromFile(path);

        }

        static void Main(string[] args)
        {
            while (true)
            {
                string fileName;

                if (args.Length > 0)
                {
                    foreach(string arg in args)
                    {
                        Simulate(false, Path.GetRelativePath(Environment.CurrentDirectory, arg));
                    }

                }
                else
                {
                    Debug.Log("Input the filepath of a file you wish to simulate and press enter. Type quit to exit.");

                    fileName = Console.ReadLine();
                    if (fileName == null || fileName.Length < 1)
                    {
                        continue;
                    }

                    if (fileName.ToLower() == "quit")
                    {
                        break;
                    }

                    Simulate(true, fileName);
                }

                if(args.Length > 0)
                {
                    break;
                }
            }

                Debug.Log("Press any key to continue...");
                Console.ReadKey();

        }

        private static void Simulate(bool steppedSimulation, string fileName)
        {
            Debug.LogBlank();
            Debug.LogHeader("Simulation Begin");
            Debug.LogBlank();

            TrainSimulation simulation = SetupSimulation(fileName);

            if (simulation != null)
            {
                while (!simulation.Tick())
                {
                    if (steppedSimulation)
                    {
                        Console.ReadKey();
                    }
                }
            }

            Debug.LogBlank();
            Debug.LogHeader("Simulation Ended");
            Debug.LogBlank();
        }
    }
}
