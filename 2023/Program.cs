using AdventOfCode2023.Day8;

using BenchmarkDotNet.Running;

var input = File.ReadAllLines(@"Day8\Input.txt");
Console.WriteLine(SecondTaskSolution.Initial(input));
// Console.WriteLine(FirstTaskSolution.LINQ(input));
// Console.WriteLine(FirstTaskSolution.UsingSpans(input));
// Console.WriteLine(SecondTaskSolution.Initial(input));
// Console.WriteLine(SecondTaskSolution.ReplaceInsteadOfSplit(input));
// Console.WriteLine(SecondTaskSolution.UsingSpans(input));

// BenchmarkRunner.Run<SecondTaskBenchmark>();