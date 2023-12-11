namespace AdventOfCode2023.Day11;

public static class FirstTaskSolution
{
    private static readonly (int, int)[] Offsets = new[]
    {
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
    };

    public static int Initial(string[] input)
    {
        var image = GetImage(input);
        var visited = new bool[image.Count][];
        for (int i = 0; i < visited.Length; i++)
        {
            visited[i] = new bool[image[0].Length];
        }

        var sum = 0;
        var (galaxyRow, galaxyCol) = FindGalaxy(image);
        var queue = new Queue<(int Row, int Col, int Length)>();
        while (galaxyRow != -1)
        {
            queue.Clear();
            ClearVisited(visited);
            queue.Enqueue((galaxyRow, galaxyCol, 0));
            visited[galaxyRow][galaxyCol] = true;
            while (queue.Count > 0)
            {
                var (row, col, length) = queue.Dequeue();
                if (image[row][col] == '#' && (galaxyRow != row || galaxyCol != col))
                {
                    sum += length;
                }

                foreach (var (dx, dy) in Offsets)
                {
                    var nextRow = row + dy;
                    var nextCol = col + dx;
                    if (InBounds(nextRow, nextCol, image) && !visited[nextRow][nextCol])
                    {
                        queue.Enqueue((nextRow, nextCol, length + 1));
                        visited[nextRow][nextCol] = true;
                    }
                }
            }

            image[galaxyRow][galaxyCol] = '.';
            (galaxyRow, galaxyCol) = FindGalaxy(image);
        }

        return sum;
    }

    public static int WithoutBFS(string[] input)
    {
        var image = GetImage(input);
        var galaxies = GetGalaxies(image);
        var sum = 0;
        while (galaxies.Count > 1)
        {
            var (row, col) = galaxies[^1];
            for (int i = 0; i < galaxies.Count - 1; i++)
            {
                var (nextRow, nextCol) = galaxies[i];
                sum += Math.Abs(row - nextRow) + Math.Abs(col - nextCol);
            }

            galaxies.RemoveAt(galaxies.Count - 1);
        }

        return sum;
    }

    public static int WithoutExpanding(string[] input)
    {
        var emptyRows = GetEmptyRows(input);
        var emptyCols = GetEmptyColumns(input);
        var galaxies = GetGalaxies(input);
        var sum = 0;
        while (galaxies.Count > 1)
        {
            var (row, col) = galaxies[^1];
            for (int i = 0; i < galaxies.Count - 1; i++)
            {
                var (nextRow, nextCol) = galaxies[i];
                var row1 = Math.Min(row, nextRow);
                var row2 = Math.Max(row, nextRow);
                var col1 = Math.Min(col, nextCol);
                var col2 = Math.Max(col, nextCol);
                var numberOfEmptyRows = GetNumberOfEmptyLinesBetween(row1, row2, emptyRows);
                var numberOfEmptyCols = GetNumberOfEmptyLinesBetween(col1, col2, emptyCols);
                sum += row2 - row1 + numberOfEmptyRows + col2 - col1 + numberOfEmptyCols;
            }

            galaxies.RemoveAt(galaxies.Count - 1);
        }

        return sum;
    }

    private static int GetNumberOfEmptyLinesBetween(int line1, int line2, HashSet<int> emptyLines)
    {
        var count = 0;
        for (int i = line1 + 1; i < line2; i++)
        {
            if (emptyLines.Contains(i))
            {
                count++;
            }
        }

        return count;
    }

    private static HashSet<int> GetEmptyRows(string[] input)
    {
        var set = new HashSet<int>();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i].AsSpan().IndexOf('#') == -1)
            {
                set.Add(i);
            }
        }

        return set;
    }

    private static List<(int Row, int Col)> GetGalaxies(string[] image)
    {
        var set = new List<(int, int)>();
        for (int i = 0; i < image.Length; i++)
        {
            for (int j = 0; j < image[0].Length; j++)
            {
                if (image[i][j] == '#')
                {
                    set.Add((i, j));
                }
            }
        }

        return set;
    }

    private static List<(int Row, int Col)> GetGalaxies(List<char[]> image)
    {
        var set = new List<(int, int)>();
        for (int i = 0; i < image.Count; i++)
        {
            for (int j = 0; j < image[0].Length; j++)
            {
                if (image[i][j] == '#')
                {
                    set.Add((i, j));
                }
            }
        }

        return set;
    }

    private static void ClearVisited(bool[][] visited)
    {
        for (int i = 0; i < visited.Length; i++)
        {
            for (int j = 0; j < visited[0].Length; j++)
            {
                visited[i][j] = false;
            }
        }
    }

    private static bool InBounds(int row, int col, List<char[]> image)
    {
        return row >= 0 && col >= 0 && row < image.Count && col < image[0].Length;
    }

    private static (int Row, int Col) FindGalaxy(List<char[]> image)
    {
        for (int i = 0; i < image.Count; i++)
        {
            for (int j = 0; j < image[0].Length; j++)
            {
                if (image[i][j] == '#')
                {
                    return (i, j);
                }
            }
        }

        return (-1, -1);
    }

    private static List<char[]> GetImage(string[] input)
    {
        var image = new List<char[]>();
        var emptyColumns = GetEmptyColumns(input);
        var emptyRow = new char[input[0].Length + emptyColumns.Count];
        Array.Fill(emptyRow, '.');
        for (int i = 0; i < input.Length; i++)
        {
            var inputIndex = 0;
            var rowIsEmpty = true;
            var charArray = new char[input[0].Length + emptyColumns.Count];
            image.Add(charArray);
            for (int j = 0; j < charArray.Length; j++)
            {
                if (input[i][inputIndex] == '#')
                {
                    rowIsEmpty = false;
                }

                if (emptyColumns.Contains(inputIndex))
                {
                    charArray[j++] = input[i][inputIndex++];
                    charArray[j] = '.';
                }
                else
                {
                    charArray[j] = input[i][inputIndex++];
                }
            }

            if (rowIsEmpty)
            {
                image.Add(emptyRow.ToArray());
            }
        }

        return image;
    }

    private static HashSet<int> GetEmptyColumns(string[] input)
    {
        var set = new HashSet<int>();
        for (int i = 0; i < input[0].Length; i++)
        {
            var isEmpty = true;
            for (int j = 0; j < input.Length; j++)
            {
                if (input[j][i] == '#')
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                set.Add(i);
            }
        }

        return set;
    }
}