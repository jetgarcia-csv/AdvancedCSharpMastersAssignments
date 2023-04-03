using Pets;

namespace Animals
{
    public class Bird : IPet
    {
        [CustomAction]
        public void Talk()
            => Console.WriteLine("Tweet! ");

        [CustomAction]
        public void Fly()
            => Console.WriteLine("I can reach the heavens above! ");

        public string Name => "Bird";
        public int Tier => 3;
    }
}
