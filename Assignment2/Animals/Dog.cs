using Pets;

namespace Animals
{
    public class Dog : IPet
    {
        [CustomAction]
        public void Talk()
            => Console.WriteLine("Woof! ");

        [CustomAction]
        public void PuppyEyes()
            => Console.WriteLine("I can be your good friend! ");

        public string Name => "Dog";
        public int Tier => 5;
    }
}
