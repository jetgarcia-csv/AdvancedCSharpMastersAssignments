using Pets;

namespace Animals
{
    public class Fish : IPet
    {
        [CustomAction]
        public void Talk()
            => Console.WriteLine("*blob* ");

        [CustomAction]
        public void Swim()
            => Console.WriteLine("I can swim just any song. ");

        public string Name => "Fish";
        public int Tier => 4;
    }
}
