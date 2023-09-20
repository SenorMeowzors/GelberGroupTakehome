namespace TakeHome.Source
{
    public class BoardWhenLessThanHalfFull : BoardingStrategy
    {
        public override void Board(Train train)
        {
            if(train.capacity >= train.capacity/2)
            {
                return;
            }
            Debug.Log($"{Customer.customerID} + boards the {train.TrainName}");
            train.BoardCustomer(this.Customer);
        }
    }

}
