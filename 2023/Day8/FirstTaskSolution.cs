namespace AdventOfCode2023.Day8;

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var turns = input[0];
        var nodes = input
            .Skip(2)
            .ToDictionary(l => l[..3], l => (l[7..10], l[12..15]));
        var currentNode = "AAA";
        var turnIndex = 0;
        var stepsCount = 0;
        while (currentNode != "ZZZ")
        {
            var turn = turns[turnIndex];
            currentNode = turn == 'L'
                ? nodes[currentNode].Item1
                : nodes[currentNode].Item2;
            turnIndex = (turnIndex + 1) % turns.Length;
            stepsCount++;
        }

        return stepsCount;
    }
}