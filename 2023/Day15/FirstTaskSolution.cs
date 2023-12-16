namespace AdventOfCode2023.Day15;

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var steps = input[0].Split(',');
        var sum = 0;
        foreach (var step in steps)
        {
            sum += Hash(step);
        }

        return sum;
    }

    private static int Hash(string str)
    {
        var sum = 0;
        foreach (var c in str)
        {
            sum += (byte)c;
            sum *= 17;
            sum %= 256;
        }

        return sum;
    }
}