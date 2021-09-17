namespace Worms
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Simulator simulator = new Simulator(10, 10);
            simulator.Run();
        }
    }
}