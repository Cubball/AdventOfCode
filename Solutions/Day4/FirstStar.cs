namespace AdventOfCode2023.Solutions.Day4;

public static class FirstStar
{
    private static readonly char[] Separators = new[] { ':', '|' };

    public static int Simple(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var parts = line.Split(Separators, StringSplitOptions.TrimEntries);
            var winningNumbersString = parts[1];
            var myNumbersString = parts[2];
            var matchingNumbersCount = winningNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                           .Select(int.Parse)
                                                           .Intersect(myNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse))
                                                           .Count();
            if (matchingNumbersCount > 0)
            {
                sum += (int)Math.Pow(2, matchingNumbersCount - 1);
            }
        }

        return sum;
    }
}