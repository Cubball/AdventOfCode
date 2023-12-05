using AdventOfCode2023.Day1;

using BenchmarkDotNet.Running;

var input = File.ReadAllLines(@"Day1\Input.txt");
Console.WriteLine(FirstTaskSolution.Naive(input));

BenchmarkRunner.Run<FirstTaskBenchmark>();