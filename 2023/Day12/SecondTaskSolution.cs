namespace AdventOfCode2023.Day12;

public static class SecondTaskSolution
{
    public static long Initial(string[] input)
    {
        var sum = 0L;
        foreach (var (line, groups) in Parse(input))
        {
            var memo = new long?[line.Length, groups.Count];
            sum += Solve(line, 0, groups, 0, memo);
        }

        return sum;
    }

    private static List<(string, List<int>)> Parse(string[] input)
    {
        var list = new List<(string, List<int>)>();
        foreach (var line in input)
        {
            var parts = line.Split(' ');
            var springs = string.Join('?', Enumerable.Repeat(parts[0], 5));
            var numbersAsString = string.Join(',', Enumerable.Repeat(parts[1], 5));
            var numbers = numbersAsString.Split(',').Select(int.Parse).ToList();
            list.Add((springs, numbers));
        }

        return list;
    }

    private static long Solve(string line, int index, List<int> groups, int groupIndex, long?[,] memo)
    {
        if (groupIndex >= groups.Count)
        {
            var foundSpring = false;
            for (int i = index; i < line.Length; i++)
            {
                if (line[i] == '#')
                {
                    foundSpring = true;
                    break;
                }
            }

            return foundSpring ? 0 : 1;
        }

        if (index >= line.Length)
        {
            return 0;
        }

        if (memo[index, groupIndex].HasValue)
        {
            return memo[index, groupIndex]!.Value;
        }

        if (line[index] == '.')
        {
            memo[index, groupIndex] = Solve(line, index + 1, groups, groupIndex, memo);
            return memo[index, groupIndex]!.Value;
        }

        var isGroup = IsGroup(line, index, groups[groupIndex]);
        if (isGroup)
        {
            memo[index, groupIndex] = Solve(line, index + groups[groupIndex] + 1, groups, groupIndex + 1, memo);
            if (line[index] == '?')
            {
                memo[index, groupIndex] += Solve(line, index + 1, groups, groupIndex, memo);
            }
        }
        else
        {
            memo[index, groupIndex] = line[index] == '?' ? Solve(line, index + 1, groups, groupIndex, memo) : 0;
        }

        return memo[index, groupIndex]!.Value;
    }

    private static bool IsGroup(string line, int index, int groupCount)
    {
        if (line.Length - index < groupCount)
        {
            return false;
        }

        for (int i = 0; i < groupCount; i++)
        {
            if (line[index + i] == '.')
            {
                return false;
            }
        }

        return index + groupCount >= line.Length || line[index + groupCount] != '#';
    }
}