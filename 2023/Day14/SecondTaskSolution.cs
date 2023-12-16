namespace AdventOfCode2023.Day14;

public static class SecondTaskSolution
{
    public static int Initial(string[] input)
    {
        var fast = input.Select(l => l.ToCharArray()).ToArray();
        var slow = input.Select(l => l.ToCharArray()).ToArray();
        var cycleLength = 0;
        while (true)
        {
            DoCycle(fast);
            DoCycle(fast);
            DoCycle(slow);
            cycleLength++;

            if (AreEqual(fast, slow))
            {
                break;
            }
        }

        fast = input.Select(l => l.ToCharArray()).ToArray();
        var statesBeforeCycleCount = 0;
        while (true)
        {
            DoCycle(fast);
            DoCycle(slow);
            statesBeforeCycleCount++;
            if (AreEqual(fast, slow))
            {
                break;
            }
        }

        var count = (1_000_000_000 - statesBeforeCycleCount) % cycleLength;
        for (int i = 0; i < count; i++)
        {
            DoCycle(fast);
        }

        return GetLoad(fast);
    }

    private static int GetLoad(char[][] map)
    {
        var load = 0;
        var rows = map.Length;
        var cols = map[0].Length;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i][j] == 'O')
                {
                    load += rows - i;
                }
            }
        }

        return load;
    }

    private static bool AreEqual(char[][] map, char[][] mapCopy)
    {
        var rows = map.Length;
        var cols = map[0].Length;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i][j] != mapCopy[i][j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static void DoCycle(char[][] map)
    {
        var rows = map.Length;
        var cols = map[0].Length;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i][j] == 'O')
                {
                    RollTheRockNorth(map, i, j);
                }
            }
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i][j] == 'O')
                {
                    RollTheRockWest(map, i, j);
                }
            }
        }

        for (int i = rows - 1; i >= 0; i--)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i][j] == 'O')
                {
                    RollTheRockSouth(map, i, j);
                }
            }
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = cols - 1; j >= 0; j--)
            {
                if (map[i][j] == 'O')
                {
                    RollTheRockEast(map, i, j);
                }
            }
        }
    }

    private static void RollTheRockNorth(char[][] map, int row, int col)
    {
        var destination = row;
        for (int i = row - 1; i >= 0; i--)
        {
            if (map[i][col] != '.')
            {
                break;
            }

            destination = i;
        }

        map[row][col] = '.';
        map[destination][col] = 'O';
    }

    private static void RollTheRockWest(char[][] map, int row, int col)
    {
        var destination = col;
        for (int i = col - 1; i >= 0; i--)
        {
            if (map[row][i] != '.')
            {
                break;
            }

            destination = i;
        }

        map[row][col] = '.';
        map[row][destination] = 'O';
    }

    private static void RollTheRockSouth(char[][] map, int row, int col)
    {
        var destination = row;
        var rows = map.Length;
        for (int i = row + 1; i < rows; i++)
        {
            if (map[i][col] != '.')
            {
                break;
            }

            destination = i;
        }

        map[row][col] = '.';
        map[destination][col] = 'O';
    }

    private static void RollTheRockEast(char[][] map, int row, int col)
    {
        var destination = col;
        var cols = map[0].Length;
        for (int i = col + 1; i < cols; i++)
        {
            if (map[row][i] != '.')
            {
                break;
            }

            destination = i;
        }

        map[row][col] = '.';
        map[row][destination] = 'O';
    }
}