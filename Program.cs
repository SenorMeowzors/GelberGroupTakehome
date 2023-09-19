namespace TakeHome
{

    //  You are to write a program to implement a railway travel simulation.This program should read input
    //  from a file and print the resulting score to the console. (The program should be a console-only program.)
    //  The program should take a single command line parameter, the name of the input file.
    //  Passengers arrive at a set of stations with a destination station in mind.Trains travel on a regular
    //  schedule and have a fixed maximum capacity. The goal of the problem is to determine when all
    //  passengers arrive at their desired destination.

    //  Problem
    //  1) The number of stations is specified by the problem inputs; stations are numbered 1, 2, 3...n.
    //  2) Time is measured in minutes.
    //  3) The time trains take to travel between stations and the frequency new trains appear at the
    //  termini are specified by problem inputs.
    //  4) The simulation starts at t=0. At that time two empty trains arrive on each end of the line,
    //  travelling to the opposite end.Trains disappear after reaching the far terminus.
    //  5) Two types of passengers arrive at the stations.
    //  a.Passenger type A is willing to board any non-full train heading to its destination.
    //  b.Passenger type B will only board a train if it is no more than half-full.
    //  6) A passenger is considered to have arrived the moment the train it is on arrives at the station.
    //  7) Passengers depart the train before passengers waiting at the station board it.
    //  8) A train is present at a station during the minute it arrives at that station.Passengers may board
    //  and depart the train. A passenger may arrive at a station and then immediately board a train
    //  that arrived in the same minute.
    //  9) If two or more passengers attempt to board a train at the same time, the passenger whose
    //  destination is more distant boards first.If there is a tie, type A passengers board first.
    //  
    //  Input
    //  The first line of input specifies the form of the railway in four integers. The first is the number of
    //  stations, the second is the time taken to travel from one station to the next, the third is the frequency
    //  that trains depart from the termini, and the fourth is the capacity of trains.
    //  Subsequent lines define passengers, in the form of a letter that gives the type of the passenger followed
    //  by three integers. The first is the time the passenger arrived, the second is their destination station, the
    //  third is the station they appear at.

    // Example Format
    // 
    // 2 2 2 2
    // A 1 1 2
    

    internal class Program
    {
        public struct TrainSchedule
        {
            public int numberofStations;
            public int stationDistance;
            public int departFrequency;
            public int capacity; 
        }

        public enum CustomerType
        {
            BoardAnyTrain,
            BoardLessThanHalfFull
        }

        public struct Customer
        {
            public CustomerType customerType;
            public int timeArrived;
            public int destinationStation;
            public int startingStation;
        }

        public static void ReadInput(string fileName)
        {
            //TODO
        }


        static void Main(string[] args)
        {
            while(true)
            {
                string fileName = Console.ReadLine();

                if(fileName == null || fileName.Length < 1)
                {
                    return;
                }

                ReadInput(fileName);
            }

        }
    }
}