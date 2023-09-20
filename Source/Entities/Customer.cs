using System.Reflection.Metadata.Ecma335;
using TakeHome.Source.BoardingStrategies;

namespace TakeHome.Source.Entities
{
    public class Customer
    {
        public enum CustomerType
        {
            BoardAnyTrain,
            BoardLessThanHalfFull
        }

        public CustomerType customerType
        {
            get
            {
                if (boardingStrategy is BoardWhenPossible)
                {
                    return CustomerType.BoardAnyTrain;
                }
                else
                {
                    return CustomerType.BoardLessThanHalfFull;
                }
            }
        }
        public int timeArrived;
        public int destinationStation;
        public int startingStation;
        public int customerID;

        public BoardingStrategy boardingStrategy;

        public override string ToString()
        {
            return
                $"ID: {customerID}\n" +
                $"Customer Type: {customerType}\n" +
                $"Time Arrived: {timeArrived}\n" +
                $"Destination: {destinationStation}\n" +
                $"Starting Station: {startingStation}\n";
        }

        public void BoardTrain(Train train)
        {
            boardingStrategy.Board(train);
        }
    }

}
