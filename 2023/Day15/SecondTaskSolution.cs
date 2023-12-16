using System.Globalization;

namespace AdventOfCode2023.Day15;

public static class SecondTaskSolution
{
    public static int Initial(string[] input)
    {
        var steps = input[0].Split(',');
        var boxes = new List<(string, int)>[256];
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i] = new List<(string, int)>();
        }

        foreach (var step in steps)
        {
            ProcessStep(step, boxes);
        }

        var sum = 0;
        for (int i = 0; i < boxes.Length; i++)
        {
            for (int j = 0; j < boxes[i].Count; j++)
            {
                var (_, focalLength) = boxes[i][j];
                sum += (i + 1) * (j + 1) * focalLength;
            }
        }

        return sum;
    }

    private static void ProcessStep(string step, List<(string, int)>[] boxes)
    {
        var equalsIndex = step.IndexOf('=');
        if (equalsIndex != -1)
        {
            var label = step[..equalsIndex];
            var number = int.Parse(step[(equalsIndex + 1)..], CultureInfo.InvariantCulture);
            var boxNumber = Hash(label);
            var indexOfLabel = boxes[boxNumber].FindIndex(x => x.Item1 == label);
            if (indexOfLabel == -1)
            {
                boxes[boxNumber].Add((label, number));
            }
            else
            {
                boxes[boxNumber][indexOfLabel] = (label, number);
            }
        }
        else
        {
            var label = step[..^1];
            var boxNumber = Hash(label);
            var indexOfLabel = boxes[boxNumber].FindIndex(x => x.Item1 == label);
            if (indexOfLabel != -1)
            {
                boxes[boxNumber].RemoveAt(indexOfLabel);
            }
        }
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