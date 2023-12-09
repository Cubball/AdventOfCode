namespace AdventOfCode2023.Day9;

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var sequences = input
            .Select(l => l
                        .Split(' ')
                        .Select(int.Parse)
                        .ToArray());
        return sequences.Sum(GetPredictedValue);
    }

    private static int GetPredictedValue(int[] sequence)
    {
        var currentIndex = sequence.Length - 1;
        while (true)
        {
            var allZeros = true;
            for (int i = 0; i < currentIndex; i++)
            {
                if (sequence[i] != 0)
                {
                    allZeros = false;
                }

                sequence[i] = sequence[i + 1] - sequence[i];
            }

            currentIndex--;
            if (allZeros)
            {
                break;
            }
        }

        return sequence.Sum();
    }
}