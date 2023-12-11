using AdventOfCode2023.Day11;

var input = File.ReadAllLines(@"Day11\Input.txt");
Console.WriteLine(FirstTaskSolution.Initial(input));
Console.WriteLine(FirstTaskSolution.WithoutBFS(input));
Console.WriteLine(FirstTaskSolution.WithoutExpanding(input));