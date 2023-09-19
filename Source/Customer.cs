namespace TakeHome.Source
{
    public struct Customer
    {
        public enum CustomerType
        {
            BoardAnyTrain,
            BoardLessThanHalfFull
        }

        public CustomerType customerType;
        public int timeArrived;
        public int destinationStation;
        public int startingStation;

        public override string ToString()
        {
            return $"Customer Type: {customerType}\n" +
                $"Time Arrived: {timeArrived}\n" +
                $"Destination: {destinationStation}\n" +
                $"Starting Station: {startingStation}\n";
        }

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
    }
}
