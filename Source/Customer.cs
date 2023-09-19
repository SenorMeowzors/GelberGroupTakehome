using System.Reflection.Metadata.Ecma335;

namespace TakeHome.Source
{
    public class Customer
    {
        public enum CustomerType
        {
            BoardAnyTrain,
            BoardLessThanHalfFull
        }

        public virtual CustomerType customerType
        {
            get
            {
                return CustomerType.BoardAnyTrain;
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
