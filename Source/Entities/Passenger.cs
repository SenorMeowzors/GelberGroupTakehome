using TakeHome.Source.Entities.Strategies.BoardingStrategies;

namespace TakeHome.Source.Entities
{
    public class Passenger
    {
        public enum PassengerType
        {
            BoardAnyTrain,
            BoardLessThanHalfFull
        }

        public PassengerType Type
        {
            get
            {
                if (BoardingStrategy is BoardWhenPossible)
                {
                    return PassengerType.BoardAnyTrain;
                }
                else
                {
                    return PassengerType.BoardLessThanHalfFull;
                }
            }
        }
        public int TimeArrived { get; private set; }
        public int DestinationStation { get; private set; }
        public int StartingStation { get; private set; }
        public int ID { get; private set; }

        public BoardingStrategy BoardingStrategy { get; private set; }


        public Passenger(int customerID, BoardingStrategy strategy, int timeArrived, int destinationStation, int startingStation)
        {
            ID = customerID;
            BoardingStrategy = strategy;
            TimeArrived = timeArrived;
            DestinationStation = destinationStation;
            StartingStation = startingStation;
            BoardingStrategy.Passenger = this;
        }

        public override string ToString()
        {
            return
                $"ID: {ID}\n" +
                $"Customer Type: {Type}\n" +
                $"Time Arrived: {TimeArrived}\n" +
                $"Destination: {DestinationStation}\n" +
                $"Starting Station: {StartingStation}\n";
        }

        public void BoardTrain(Train train)
        {
            BoardingStrategy.Board(train);
        }
    }

}
