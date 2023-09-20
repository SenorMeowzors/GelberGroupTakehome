
namespace TakeHome.Source.Entities.Strategies.BoardingStrategies
{
    public class BoardWhenPossible : BoardingStrategy
    {
        public override int Priority => 1;

        public override void Board(Train train)
        {
            train.Board(Passenger);
        }
    }

}
