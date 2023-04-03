using Pets;

namespace Animals
{
    public class Cat : IPet
    {
        [CustomAction]
        public void Talk()
            => Console.WriteLine("Meow! ");

        [CustomAction]
        public void Purr()
            => Console.WriteLine("I can love you until the end! ");

        public string Name => "Cat";
        public int Tier => 6;
    }
}
