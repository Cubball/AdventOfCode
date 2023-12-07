using System.Globalization;

namespace AdventOfCode2023.Day7;

public class JokerHandComparer : IComparer<string>
{
    private static readonly Dictionary<char, int> CardStrengths = new()
    {
        ['J'] = 0,
        ['2'] = 1,
        ['3'] = 2,
        ['4'] = 3,
        ['5'] = 4,
        ['6'] = 5,
        ['7'] = 6,
        ['8'] = 7,
        ['9'] = 8,
        ['T'] = 9,
        ['Q'] = 10,
        ['K'] = 11,
        ['A'] = 12,
    };

    public int Compare(string? x, string? y)
    {
        var modifiedFirstHand = GetStrogestHand(x!);
        var modifiedSecondHand = GetStrogestHand(y!);
        var xType = GetHandType(modifiedFirstHand);
        var yType = GetHandType(modifiedSecondHand);
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

    private static string GetStrogestHand(string hand)
    {
        var jokerCount = hand.Count(c => c == 'J');
        if (jokerCount == 0)
        {
            return hand;
        }

        if (jokerCount == 5)
        {
            return "AAAAA";
        }

        var frequencies = new Dictionary<char, int>();
        foreach (var card in hand)
        {
            if (card != 'J')
            {
                frequencies[card] = frequencies.GetValueOrDefault(card, 0) + 1;
            }
        }

        var maxCount = 0;
        var mostFrequentCard = ' ';
        foreach (var (card, count) in frequencies)
        {
            if (count > maxCount)
            {
                mostFrequentCard = card;
                maxCount = count;
            }
            else if (count == maxCount && CardStrengths[card] > CardStrengths[mostFrequentCard])
            {
                mostFrequentCard = card;
            }
        }

        return hand.Replace('J', mostFrequentCard);
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

public static class SecondTaskSolution
{
    public static int Initial(string[] input)
    {
        var bids = input
            .Select(l => l.Split(' '))
            .ToDictionary(p => p[0], p => int.Parse(p[1], CultureInfo.InvariantCulture));
        var hands = bids
            .Select(p => p.Key)
            .OrderDescending(new JokerHandComparer())
            .ToList();
        for (int i = 0; i < hands.Count; i++)
        {
            var rank = hands.Count - i;
            bids[hands[i]] *= rank;
        }

        return bids.Sum(p => p.Value);
    }
}