using System.Globalization;

using Microsoft.Z3;

namespace AdventOfCode2023.Day24;

public static class SecondTaskSolution
{
    private static readonly char[] Separators = new char[] { ' ', ',' };

    public static long Initial(string[] input)
    {
        var context = new Context();
        var posX = context.MkIntConst("x");
        var posY = context.MkIntConst("y");
        var posZ = context.MkIntConst("z");
        var velX = context.MkIntConst("m");
        var velY = context.MkIntConst("n");
        var velZ = context.MkIntConst("l");
        var solver = context.MkSolver();
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var parts = line.Split('@');
            var positionParts = parts[0].Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            var xVal = long.Parse(positionParts[0], CultureInfo.InvariantCulture);
            var yVal = long.Parse(positionParts[1], CultureInfo.InvariantCulture);
            var zVal = long.Parse(positionParts[2], CultureInfo.InvariantCulture);
            var velocityParts = parts[1].Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            var mVal = long.Parse(velocityParts[0], CultureInfo.InvariantCulture);
            var nVal = long.Parse(velocityParts[1], CultureInfo.InvariantCulture);
            var lVal = long.Parse(velocityParts[2], CultureInfo.InvariantCulture);
            var t = context.MkIntConst($"t{i}");
            solver.Assert(context.MkEq(context.MkAdd(posX, context.MkMul(velX, t)), context.MkAdd(context.MkInt(xVal), context.MkMul(context.MkInt(mVal), t))));
            solver.Assert(context.MkEq(context.MkAdd(posY, context.MkMul(velY, t)), context.MkAdd(context.MkInt(yVal), context.MkMul(context.MkInt(nVal), t))));
            solver.Assert(context.MkEq(context.MkAdd(posZ, context.MkMul(velZ, t)), context.MkAdd(context.MkInt(zVal), context.MkMul(context.MkInt(lVal), t))));
        }

        solver.Check();
        var result = solver.Model.Evaluate(posX + posY + posZ);
        return long.Parse(result.ToString(), CultureInfo.InvariantCulture);
    }
}