using TakeHome.Source.Entities;

namespace TakeHome.Source.BoardingStrategies
{
    public class BoardWhenPossible : BoardingStrategy
    {
        public override int Priority => 1;

        public override void Board(Train train)
        {
            train.BoardCustomer(Customer);
        }
    }

}
