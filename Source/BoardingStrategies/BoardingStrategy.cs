using TakeHome.Source.Entities;

namespace TakeHome.Source.BoardingStrategies
{
    public abstract class BoardingStrategy
    {
        public Customer Customer { get; set; }
        public abstract void Board(Train train);

        public abstract int Priority { get; }
    }

}
