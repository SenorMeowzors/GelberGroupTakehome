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
                Debug.Log($"{path} does not exist!", true);
                return null;
            }

            return Parser.TrainSimulationFromFile(path);

        }



        static void Main(string[] args)
        {
            bool stepped = false;
            string fileName;

            while (true)
            {          
                List<string> filesToRun  = new List<string>();

                if (args.Length > 0)
                {
                    foreach (string arg in args)
                    {
                        if (arg == "-simple")
                        {
                            Debug.Simple = true;
                            continue;
                        }

                        if(arg == "-steps")
                        {
                            stepped = true;
                        }

                        if (Path.Exists(arg))
                        {
                            filesToRun.Add(arg);
                            continue;
                        }
                    }
                }

                if (filesToRun.Count > 0)
                {
                    foreach(string file in filesToRun)
                    {
                        Simulate(Path.GetRelativePath(Environment.CurrentDirectory, file), stepped);
                    }

                }
                else
                {
                    Debug.Log("Input the filepath of a file (with extension) you wish to simulate and press enter. Type quit to exit.", true);

                    fileName = Console.ReadLine();
                    if (fileName == null || fileName.Length < 1)
                    {
                        continue;
                    }

                    if (fileName.ToLower() == "quit")
                    {
                        break;
                    }

                    Simulate(fileName);
                }

                if(args.Length > 0)
                {
                    break;
                }
            }

            if(stepped)
            {
                Debug.Log("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void Simulate(string fileName, bool steppedSimulation = false)
        {
            Debug.LogBlank();
            Debug.LogHeader("Simulation Begin");
            Debug.LogBlank();

            TrainSimulation simulation = SetupSimulation(fileName);

            if (simulation != null)
            {
                while (!simulation.Tick())
                {
                    if (steppedSimulation && !Debug.Simple)
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
