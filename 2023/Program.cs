using AdventOfCode2023.Day6;

using BenchmarkDotNet.Running;

var input = File.ReadAllLines(@"Day6\Input.txt");
Console.WriteLine(FirstTaskSolution.Initial(input));
// Console.WriteLine(FirstTaskSolution.UsingSpans(input));
// Console.WriteLine(SecondTaskSolution.Initial(input));

// BenchmarkRunner.Run<FirstTaskBenchmark>();