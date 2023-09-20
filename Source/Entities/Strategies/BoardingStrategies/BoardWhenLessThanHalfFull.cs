using TakeHome.Source.Entities;
using TakeHome.Source.Helper;

namespace TakeHome.Source.Entities.Strategies.BoardingStrategies
{
    public class BoardWhenLessThanHalfFull : BoardingStrategy
    {
        public override int Priority => 0;

        public override void Board(Train train)
        {
            var half = train.Capacity / 2.0f;
            if (train.CurrentPassengers.Count >= half)
            {
                Debug.Log($"{Passenger.ID} refused to board the {train.TrainName}. Capacity was {train.CurrentPassengers.Count}/{train.Capacity}");

                return;
            }
            Debug.Log($"{Passenger.ID} boards the {train.TrainName}");
            train.Board(Passenger);
        }
    }

}
