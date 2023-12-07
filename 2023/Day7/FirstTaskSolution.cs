using System.Globalization;

namespace AdventOfCode2023.Day7;

public class HandComparer : IComparer<string>
{
    private static readonly Dictionary<char, int> CardStrengths = new()
    {
        ['2'] = 0,
        ['3'] = 1,
        ['4'] = 2,
        ['5'] = 3,
        ['6'] = 4,
        ['7'] = 5,
        ['8'] = 6,
        ['9'] = 7,
        ['T'] = 8,
        ['J'] = 9,
        ['Q'] = 10,
        ['K'] = 11,
        ['A'] = 12,
    };

    public int Compare(string? x, string? y)
    {
        var xType = GetHandType(x!);
        var yType = GetHandType(y!);
        if (xType != yType)
        {
            return xType.CompareTo(yType);
        }

        for (int i = 0; i < 5; i++)
        {
            if (x![i] != y![i])
            {
                return CompareCards(x[i], y[i]);
            }
        }

        return 0;
    }

    private static int CompareCards(char card1, char card2) => Math.Sign(CardStrengths[card1] - CardStrengths[card2]);

    private static int GetHandType(string hand)
    {
        var frequencies = new Dictionary<char, int>();
        foreach (var card in hand)
        {
            frequencies[card] = frequencies.GetValueOrDefault(card, 0) + 1;
        }

        return frequencies.Count switch
        {
            1 => 6,
            2 => frequencies.ContainsValue(4) ? 5 : 4,
            3 => frequencies.ContainsValue(3) ? 3 : 2,
            4 => 1,
            _ => 0,
        };
    }
}

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var bids = input
            .Select(l => l.Split(' '))
            .ToDictionary(p => p[0], p => int.Parse(p[1], CultureInfo.InvariantCulture));
        var hands = bids
            .Select(p => p.Key)
            .OrderDescending(new HandComparer())
            .ToList();
        for (int i = 0; i < hands.Count; i++)
        {
            var rank = hands.Count - i;
            bids[hands[i]] *= rank;
        }

        return bids.Sum(p => p.Value);
    }
}