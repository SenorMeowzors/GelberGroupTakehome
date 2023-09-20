using TakeHome.Source.Entities;
using TakeHome.Source.Helper;

namespace TakeHome.Source
{
    class Program
    {
        private static bool _stepped = false;
        private static string _fileName;
        private static List<string> _filesToRun = new List<string>();

        static void Main(string[] args)
        {
            while (true)
            {
                InitializeParameters(args);

                if (_filesToRun.Count > 0)
                {
                    foreach (string file in _filesToRun)
                    {
                        Simulate(Path.GetRelativePath(Environment.CurrentDirectory, file), _stepped);
                    }

                }
                else
                {
                    Debug.Log("Input the filepath of a file (with extension) you wish to simulate and press enter. Type quit to exit.", true);

                    _fileName = Console.ReadLine();
                    if (_fileName == null || _fileName.Length < 1)
                    {
                        continue;
                    }

                    if (_fileName.ToLower() == "quit")
                    {
                        break;
                    }

                    Simulate(_fileName);
                }

                if (args.Length > 0)
                {
                    break;
                }
            }

            if (_stepped)
            {
                Debug.Log("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void InitializeParameters(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (string arg in args)
                {
                    if (arg == "-simple")
                    {
                        Debug.Simple = true;
                        continue;
                    }

                    if (arg == "-steps")
                    {
                        _stepped = true;
                    }

                    if (Path.Exists(arg))
                    {
                        _filesToRun.Add(arg);
                        continue;
                    }
                }
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

        private static TrainSimulation SetupSimulation(string fileName)
        {
            var path = fileName;
            if (!File.Exists(path))
            {
                Debug.Log($"{path} does not exist!", true);
                return null;
            }

            return Parser.TrainSimulationFromFile(path);

        }
    }
}
