namespace AdventOfCode2023.Day8;

public static class SecondTaskSolution
{
    public static long Initial(string[] input)
    {
        var turns = input[0];
        var nodes = input
            .Skip(2)
            .ToDictionary(l => l[..3], l => (l[7..10], l[12..15]));
        var pathLengths = nodes.Keys
            .Where(n => n[2] == 'A')
            .Select(n => GetStepsCount(n, nodes, turns))
            .ToList();
        return LCM(pathLengths);
    }

    private static long GetStepsCount(string startNode, Dictionary<string, (string, string)> nodes, string turns)
    {
        var stepsCount = 0L;
        var turnIndex = 0;
        while (startNode[2] != 'Z')
        {
            var turn = turns[turnIndex];
            startNode = turn == 'L'
                ? nodes[startNode].Item1
                : nodes[startNode].Item2;
            turnIndex = (turnIndex + 1) % turns.Length;
            stepsCount++;
        }

        return stepsCount;
    }

    private static long LCM(List<long> numbers, int index = 0)
    {
        if (index == numbers.Count - 1)
        {
            return numbers[^1];
        }

        var lcm = numbers[index] * numbers[index + 1] / GCD(numbers[index], numbers[index + 1]);
        numbers[index + 1] = lcm;
        return LCM(numbers, index + 1);
    }

    private static long GCD(long a, long b) => a == 0 ? b : GCD(b % a, a);
}