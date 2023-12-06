using AdventOfCode2023.Day5;

using BenchmarkDotNet.Running;

// var input = File.ReadAllLines(@"Day5\Input.txt");
// Console.WriteLine(FirstTaskSolution.Initial(input));
// Console.WriteLine(FirstTaskSolution.UsingSpans(input));
// Console.WriteLine(SecondTaskSolution.Initial(input));

BenchmarkRunner.Run<FirstTaskBenchmark>();