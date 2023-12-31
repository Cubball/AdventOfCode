namespace AdventOfCode2023.Day4;

public static class FirstTaskSolution
{
    private static readonly char[] Separators = new[] { ':', '|' };

    public static int Initial(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var parts = line.Split(Separators, StringSplitOptions.TrimEntries);
            var winningNumbersString = parts[1];
            var myNumbersString = parts[2];
            var matchingNumbersCount = winningNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                           .Intersect(myNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                                                           .Count();
            if (matchingNumbersCount > 0)
            {
                sum += (int)Math.Pow(2, matchingNumbersCount - 1);
            }
        }

        return sum;
    }

    public static int LINQ(string[] input) =>
        input.Select(l => l.Split(Separators, StringSplitOptions.TrimEntries))
             .Select(p => p[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                              .Intersect(p[2].Split(' ', StringSplitOptions.RemoveEmptyEntries))
                              .Count())
             .Where(n => n > 0)
             .Sum(n => (int)Math.Pow(2, n - 1));
}