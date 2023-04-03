using System.Collections;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter objects. Enter empty string to terminate input. ");

        var inputs = new ArrayList();
        var isAcceptingInput = true;

        do
        {
            var input = Console.ReadLine();

            if (input == string.Empty)
                break;

            inputs.Add(input);
        } while (isAcceptingInput);

        var (stringList, intList, boolList, doubleList) = CollateByTypes(inputs);

        var options = new Dictionary<int, string>
        {
            { 0, "All inputs"},
            { 1, "Boolean values" },
            { 2, "Integer values"},
            { 3, "Double values"},
            { 4, "String values"},
        };

        do
        {
            Console.Clear();

            foreach (var option in options)
                Console.WriteLine($"[{option.Key}] {option.Value}");

            Console.WriteLine("Enter option: ");

            var input = Console.ReadLine();
            var isValidOption = int.TryParse(input, out int selectedOption);

            if (!isValidOption)
                continue;

            Console.Clear();
            Console.WriteLine(options[selectedOption]);

            var _ = selectedOption switch
            {
                0 => PrintList(inputs.Cast<string>().ToList()),
                1 => PrintList(boolList),
                2 => PrintList(intList),
                3 => PrintList(doubleList),
                4 => PrintList(stringList),
                _ => throw new Exception("Invalid input. "),
            };

            Console.ReadLine();
        } while (true);
    }

    private static (IList<string>, IList<int>, IList<bool>, IList<double>) CollateByTypes(ArrayList arrayList)
    {
        var stringList = new List<string>();
        var intList = new List<int>();
        var boolList = new List<bool>();
        var doubleList = new List<double>();

        foreach (var item in arrayList)
        {
            object? converted = item.CastOrDefault<int>()
                ?? item.CastOrDefault<double>()
                ?? item.CastOrDefault<bool>()
                ?? (object?)item?.ToString();

            if (converted == null)
                continue;

            switch (converted)
            {
                case bool val:
                    boolList.Add(val);
                    break;
                case int val:
                    intList.Add(val);
                    break;
                case double val:
                    doubleList.Add(val);
                    break;
                case string val:
                    stringList.Add(val);
                    break;
                default:
                    break;
            }
        }

        return (stringList, intList, boolList, doubleList);
    }

    private static bool PrintList<T>(IEnumerable<T> list)
    {
        foreach (var item in list)
            Console.WriteLine(item);

        return true;
    }
}
