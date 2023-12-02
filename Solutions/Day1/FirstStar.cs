namespace AdventOfCode2023.Solutions.Day1;

public static class FirstStar
{
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

    public static int InitialUsingSpans(string[] input)
    {
        var sum = 0;
        var inputAsSpan = input.AsSpan();
        foreach (var line in inputAsSpan)
        {
            var firstDigit = -1;
            var lastDigit = 0;
            var lineAsSpan = line.AsSpan();
            foreach (var character in lineAsSpan)
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

    public static int BetterApproachUsingSpans(string[] input)
    {
        var sum = 0;
        var inputAsSpan = input.AsSpan();
        foreach (var lineAsString in inputAsSpan)
        {
            var line = lineAsString.AsSpan();
            var firstDigit = 0;
            for (int j = 0; j < line.Length; j++)
            {
                var character = line[j];
                if (character is < '0' or > '9')
                {
                    continue;
                }

                firstDigit = character - '0';
                break;
            }

            var lastDigit = 0;
            for (int j = line.Length - 1; j >= 0; j--)
            {
                var character = line[j];
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
}