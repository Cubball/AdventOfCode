namespace AdventOfCode2023.Solutions.Day4;

public static class SecondStar
{
    private static readonly char[] Separators = new[] { ':', '|' };

    public static int Simple(string[] input)
    {
        var numberOfInstances = input.Select(_ => 1).ToArray();
        var cardNumber = 1;
        foreach (var line in input)
        {
            var parts = line.Split(Separators, StringSplitOptions.TrimEntries);
            var winningNumbersString = parts[1];
            var myNumbersString = parts[2];
            var matchingNumbersCount = winningNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                           .Select(int.Parse)
                                                           .Intersect(myNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse))
                                                           .Count();
            for (int i = 0; i < matchingNumbersCount; i++)
            {
                numberOfInstances[cardNumber + i] += numberOfInstances[cardNumber - 1];
            }

            cardNumber++;
        }

        return numberOfInstances.Sum();
    }
}