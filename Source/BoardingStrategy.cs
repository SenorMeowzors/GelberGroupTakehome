namespace TakeHome.Source
{
    public abstract class BoardingStrategy
    {
        public Customer Customer { get; set; }
        public abstract void Board(Train train);
    }

}
