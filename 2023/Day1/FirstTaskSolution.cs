using System.Buffers;

namespace AdventOfCode2023.Day1;

public static class FirstTaskSolution
{
    private static readonly SearchValues<char> DigitSearchValues = SearchValues.Create("123456789");

    public static int Initial(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var firstDigit = -1;
            var lastDigit = 0;
            foreach (var character in line)
            {
                if (character is < '0' or > '9')
                {
                    continue;
                }

                var digit = character - '0';
                if (firstDigit == -1)
                {
                    firstDigit = digit;
                }

                lastDigit = digit;
            }

            sum += (firstDigit * 10) + lastDigit;
        }

        return sum;
    }

    public static int BetterApproach(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var firstDigit = 0;
            for (int i = 0; i < line.Length; i++)
            {
                var character = line[i];
                if (character is < '0' or > '9')
                {
                    continue;
                }

                firstDigit = character - '0';
                break;
            }

            var lastDigit = 0;
            for (int i = line.Length - 1; i >= 0; i--)
            {
                var character = line[i];
                if (character is < '0' or > '9')
                {
                    continue;
                }

                lastDigit = character - '0';
                break;
            }

            sum += (firstDigit * 10) + lastDigit;
        }

        return sum;
    }

    public static int UsingSearchValues(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var lineAsSpan = line.AsSpan();
            var firstDigitIndex = lineAsSpan.IndexOfAny(DigitSearchValues);
            var firstDigit = lineAsSpan[firstDigitIndex] - '0';
            var lastDigitIndex = lineAsSpan.LastIndexOfAny(DigitSearchValues);
            var lastDigit = lineAsSpan[lastDigitIndex] - '0';
            sum += (firstDigit * 10) + lastDigit;
        }

        return sum;
    }
}