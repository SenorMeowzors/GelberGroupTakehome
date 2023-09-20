using TakeHome.Source.Entities;

namespace TakeHome.Source.BoardingStrategies
{
    public class BoardWhenLessThanHalfFull : BoardingStrategy
    {
        public override int Priority => 0;

        public override void Board(Train train)
        {
            var half = train.capacity / 2.0f;
            if (train.currentCustomers.Count >= half)
            {
                Debug.Log($"{Customer.customerID} refused to board the {train.TrainName}. Capacity was {train.currentCustomers.Count}/{train.capacity}");

                return;
            }
            Debug.Log($"{Customer.customerID} + boards the {train.TrainName}");
            train.BoardCustomer(Customer);
        }
    }

}
