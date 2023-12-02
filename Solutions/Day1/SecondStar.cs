namespace AdventOfCode.Solutions.Day1;

public static class SecondStar
{
    private static readonly string[] DigitsSpelled = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    public static int Initial(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var firstDigit = -1;
            var lastDigit = 0;
            for (int i = 0; i < line.Length; i++)
            {
                var character = line[i];
                var digit = character - '0';
                if (digit is < 0 or > 9)
                {
                    var substring = line[..(i + 1)];
                    for (int j = 0; j < DigitsSpelled.Length; j++)
                    {
                        if (substring.EndsWith(DigitsSpelled[j], StringComparison.InvariantCulture))
                        {
                            digit = j + 1;
                            break;
                        }
                    }

                    if (digit is < 0 or > 9)
                    {
                        continue;
                    }
                }

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
}