using Pets;

namespace Animals
{
    public class Aardvark : IPet
    {
        [CustomAction]
        public void Talk()
            => Console.WriteLine("Krkrkr! ");

        [CustomAction]
        public void DoSomething()
            => Console.WriteLine("I can love. ");

        public string Name => "Aardvark";
        public int Tier => 2;
    }
}
