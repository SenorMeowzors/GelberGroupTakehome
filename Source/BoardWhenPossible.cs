namespace TakeHome.Source
{
    public class BoardWhenPossible : BoardingStrategy
    {
        public override void Board(Train train)
        {
            train.BoardCustomer(this.Customer);
        }
    }

}
