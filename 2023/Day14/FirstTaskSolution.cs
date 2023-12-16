namespace AdventOfCode2023.Day14;

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var map = input.Select(l => l.ToCharArray()).ToArray();
        var rows = map.Length;
        var cols = map[0].Length;
        var load = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i][j] == '.')
                {
                    RollTheRock(map, i, j);
                }

                if (map[i][j] == 'O')
                {
                    load += rows - i;
                }
            }
        }

        return load;
    }

    private static void RollTheRock(char[][] map, int row, int col)
    {
        var rows = map.Length;
        for (int i = row + 1; i < rows; i++)
        {
            if (map[i][col] == '#')
            {
                return;
            }

            if (map[i][col] == 'O')
            {
                map[i][col] = '.';
                map[row][col] = 'O';
                return;
            }
        }
    }
}