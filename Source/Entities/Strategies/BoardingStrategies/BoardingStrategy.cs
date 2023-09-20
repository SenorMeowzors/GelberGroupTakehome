using TakeHome.Source.Entities;

namespace TakeHome.Source.Entities.Strategies.BoardingStrategies
{
    public abstract class BoardingStrategy
    {
        public Passenger? Passenger { get; set; }
        public abstract void Board(Train train);
        public abstract int Priority { get; }
    }

}
