namespace AdventOfCode2023.Solutions.Day1;

public static class SecondStar
{
    private static readonly string[] DigitsSpelled = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    public static int Naive(string[] input)
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

    public static int UsingSpans(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var lineAsSpan = line.AsSpan();
            var firstDigit = -1;
            var lastDigit = 0;
            for (int i = 0; i < lineAsSpan.Length; i++)
            {
                var character = lineAsSpan[i];
                var digit = character - '0';
                if (digit is < 0 or > 9)
                {
                    var substring = lineAsSpan[..(i + 1)];
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

    public static int UsingCustomComparison(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var lineAsSpan = line.AsSpan();
            var firstDigit = -1;
            var lastDigit = 0;
            for (int i = 0; i < lineAsSpan.Length; i++)
            {
                var character = lineAsSpan[i];
                var digit = character - '0';
                if (digit is < 0 or > 9)
                {
                    digit = GetDigitSpelledOut(lineAsSpan, i);
                    if (digit == -1)
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

    private static int GetDigitSpelledOut(ReadOnlySpan<char> line, int startIndex)
    {
        if (line.Length - startIndex < 3)
        {
            return -1;
        }

        var firstChar = line[startIndex];
        if (firstChar == 't')
        {
            var slice = line[(startIndex + 1)..];
            if (slice.Length >= 2 && slice[0] == 'w' && slice[1] == 'o')
            {
                return 2;
            }
            else if (slice.Length >= 4 && slice[0] == 'h' && slice[1] == 'r' && slice[2] == 'e' && slice[3] == 'e')
            {
                return 3;
            }

            return -1;
        }
        else if (firstChar == 'f')
        {
            var slice = line[(startIndex + 1)..];
            if (slice.Length < 3)
            {
                return -1;
            }

            if (slice[0] == 'o' && slice[1] == 'u' && slice[2] == 'r')
            {
                return 4;
            }
            else if (slice[0] == 'i' && slice[1] == 'v' && slice[2] == 'e')
            {
                return 5;
            }

            return -1;
        }
        else if (firstChar == 's')
        {
            var slice = line[(startIndex + 1)..];
            if (slice.Length >= 2 && slice[0] == 'i' && slice[1] == 'x')
            {
                return 6;
            }
            else if (slice.Length >= 4 && slice[0] == 'e' && slice[1] == 'v' && slice[2] == 'e' && slice[3] == 'n')
            {
                return 7;
            }

            return -1;
        }

        var fullSlice = line[startIndex..];
        if (fullSlice.StartsWith("one"))
        {
            return 1;
        }
        else if (fullSlice.StartsWith("nine"))
        {
            return 9;
        }
        else if (fullSlice.StartsWith("eight"))
        {
            return 8;
        }

        return -1;
    }
}