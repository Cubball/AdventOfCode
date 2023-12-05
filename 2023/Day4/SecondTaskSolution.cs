namespace AdventOfCode2023.Day4;

public static class SecondTaskSolution
{
    private static readonly char[] Separators = new[] { ':', '|' };

    public static int Initial(string[] input)
    {
        var numberOfInstances = input.Select(_ => 1).ToArray();
        var cardNumber = 1;
        foreach (var line in input)
        {
            var parts = line.Split(Separators, StringSplitOptions.TrimEntries);
            var winningNumbersString = parts[1];
            var myNumbersString = parts[2];
            var matchingNumbersCount = winningNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                           .Intersect(myNumbersString.Split(' ', StringSplitOptions.RemoveEmptyEntries))
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