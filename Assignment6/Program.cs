internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter the horse you would like to bet (1-10): ");

        var input = Console.ReadLine();

        var isValidHorseId = int.TryParse(input, out int betHorseId)
            && betHorseId > 0
            && betHorseId <= 10;

        if (!isValidHorseId)
            throw new Exception("Invalid input. ");

        var randomizer = new Random();

        var horses = Enumerable.Range(1, 10)
            .Select((x) => new
            {
                Id = x,
                Name = $"Horse {x:00}",
                Duration = randomizer.Next(1000, 10000),
            });

        var isRaceFinished = false;
        int? winnerHorseId = null;
        string? winnerHorseName = null;

        var horseThreads = horses
            .Select(x =>
            {
                var thread = new Thread(() =>
                {
                    Run(x.Name, x.Duration);

                    if (!isRaceFinished)
                    {
                        isRaceFinished = true;
                        winnerHorseId = x.Id;
                        winnerHorseName = x.Name;
                    }
                })
                {
                    Name = x.Name,
                    Priority = (ThreadPriority)randomizer.Next(0, 4),
                };

                return thread;
            })
            .ToList();

        horseThreads.ForEach(x => x.Start());
        horseThreads.ForEach(x => x.Join());

        if (betHorseId == winnerHorseId)
            Console.WriteLine($"Congrats, {winnerHorseName} won the race! ");
        else
            Console.WriteLine($"Sorry, {winnerHorseName} did not win the race. ");
    }

    public static void Run(string threadName, int milliseconds)
    {
        Thread.Sleep(milliseconds);

        Console.WriteLine($"Thread {threadName} completed in {milliseconds} ms. ");
    }
}
