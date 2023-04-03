using Pets;

namespace Animals
{
    public class Fox : IPet
    {
        [CustomAction]
        public void Talk()
            => Console.WriteLine("(What does the fox say?) ");

        [CustomAction]
        public void Hunt()
            => Console.WriteLine("I can live. ");

        public string Name => "Fox";
        public int Tier => 1;
    }
}
