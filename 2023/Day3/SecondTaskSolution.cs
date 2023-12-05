using System.Globalization;

namespace AdventOfCode2023.Day3;

public static class SecondTaskSolution
{
    public static int Initial(string[] input)
    {
        var sum = 0;
        var numbers = new List<int>();
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i].AsSpan();
            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] != '*')
                {
                    continue;
                }

                numbers.Clear();
                numbers.AddRange(GetAdjacentNumbers(line, j));
                if (i > 0)
                {
                    numbers.AddRange(GetAdjacentNumbers(input[i - 1], j));
                }

                if (i < input.Length - 1)
                {
                    numbers.AddRange(GetAdjacentNumbers(input[i + 1], j));
                }

                if (numbers.Count == 2)
                {
                    sum += numbers[0] * numbers[1];
                }
            }
        }

        return sum;
    }

    private static List<int> GetAdjacentNumbers(ReadOnlySpan<char> line, int gearIndex)
    {
        if (char.IsDigit(line[gearIndex]))
        {
            var startIndex = line[..gearIndex].LastIndexOfAnyExceptInRange('0', '9') + 1;
            var endIndex = line[(gearIndex + 1)..].IndexOfAnyExceptInRange('0', '9');
            if (endIndex < 0)
            {
                endIndex = line.Length;
            }
            else
            {
                endIndex += gearIndex + 1;
            }

            return new List<int> { int.Parse(line[startIndex..endIndex], CultureInfo.InvariantCulture) };
        }

        var numbers = new List<int>();
        if (gearIndex > 0 && char.IsDigit(line[gearIndex - 1]))
        {
            var startIndex = line[..gearIndex].LastIndexOfAnyExceptInRange('0', '9') + 1;
            numbers.Add(int.Parse(line[startIndex..gearIndex], CultureInfo.InvariantCulture));
        }

        if (gearIndex < line.Length - 1 && char.IsDigit(line[gearIndex + 1]))
        {
            var endIndex = line[(gearIndex + 1)..].IndexOfAnyExceptInRange('0', '9');
            if (endIndex < 0)
            {
                endIndex = line.Length;
            }
            else
            {
                endIndex += gearIndex + 1;
            }

            numbers.Add(int.Parse(line[(gearIndex + 1)..endIndex], CultureInfo.InvariantCulture));
        }

        return numbers;
    }
}