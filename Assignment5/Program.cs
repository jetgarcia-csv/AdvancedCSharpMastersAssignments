using System.Net.Http.Json;

public class Program
{
    private const string _catFactsApiEndpoint = "https://cat-fact.herokuapp.com/facts";

    private static int _sumResult;
    private static IList<int> _intList = new List<int>();
    private static object _intListLock = new object();

    private static async Task Main(string[] args)
    {
        var cancellationTokenSource = new CancellationTokenSource();

        Console.WriteLine("Running tasks. Press Ctrl+C to cancel. ");

        Console.CancelKeyPress +=
            (sender, eventArgs) =>
            {
                Console.WriteLine("Cancelling tasks. ");

                cancellationTokenSource.Cancel();

                eventArgs.Cancel = true;
            };

        try
        {
            Task.WaitAll(
                new[]
                {
                    GetCatFacts(cancellationTokenSource.Token),
                    LoopAndPrint(cancellationTokenSource.Token),
                    Sum(1, 2, cancellationTokenSource.Token),
                    AddToCollection(100, cancellationTokenSource.Token)
                },
                cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
        }
    }

    private static async Task<IList<CatFact>> GetCatFacts(CancellationToken cancellationToken)
    {
        Console.WriteLine("Getting cat facts. ");

        var httpClient = new HttpClient();

        var response = await httpClient.GetAsync(_catFactsApiEndpoint, cancellationToken);

        var result = response?.IsSuccessStatusCode ?? false
            ? await response.Content
                .ReadFromJsonAsync<IList<CatFact>>(cancellationToken: cancellationToken)
                ?? Enumerable.Empty<CatFact>().ToList()
            : Enumerable.Empty<CatFact>().ToList();

        foreach (var item in result)
        {
            Console.WriteLine(item.Text);
        }

        return result;
    }

    private static async Task LoopAndPrint(CancellationToken cancellationToken)
    {
        Console.WriteLine("LoopAndPrint is running. ");

        await Task.Run(() =>
        {
            for (int x = 0; x < 20 && !cancellationToken.IsCancellationRequested; x++)
            {
                Console.WriteLine($"LoopAndPrint: {x}. ");

                Thread.Sleep(1000);
            }
        },
        cancellationToken);
    }

    private static async Task Sum(int operand1, int operand2, CancellationToken cancellationToken)
    {
        Console.WriteLine("Sum is running. ");

        await Task.Run(() => operand1 + operand2)
            .ContinueWith(x =>
            {
                _sumResult = x.Result;
            },
            cancellationToken);
    }

    private static async Task AddToCollection(int value, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Adding to collection: '{value}'. ");

        await Task.Run(() =>
        {
            lock (_intListLock)
                _intList.Add(value);
        },
        cancellationToken);
    }

    internal class CatFact
    {
        public string? Text { get; set; }
    }
}
