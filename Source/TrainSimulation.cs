namespace TakeHome.Source
{
    public class TrainSimulation
    {
        List<Customer> _customers;
        public TrainSchedule trainSchedule;

        Train train1;
        Train train2;

        public int time = 0;

        public TrainSimulation(List<Customer> customersList, TrainSchedule schedule)
        {
            _customers = customersList;
            trainSchedule = schedule;

            train1 = new Train(this, true);
            train2 = new Train(this, false);
        }

        public bool Tick()
        {

            train1.Tick(_customers);
            train2.Tick(_customers);

            time++;
            if (train1.Finished && train2.Finished)
            {
                Console.WriteLine($"Finished in t = " + time);
                return true;
            }
            return false;
        }
    }
}
