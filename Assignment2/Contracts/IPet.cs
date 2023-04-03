namespace Pets
{
    public interface IPet
    {
        int Tier { get; }
        string Name { get; }

        void Talk();
    }
}
