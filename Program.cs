using AdventOfCode2023.Benchmarks.Day1;

using BenchmarkDotNet.Running;

// var input = File.ReadAllLines(@"Inputs\Day1.txt");
// Console.WriteLine(SecondStar.Initial(input));
// Console.WriteLine(SecondStar.UsingSpans(input));
// Console.WriteLine(SecondStar.UsingCustomComparison(input));

BenchmarkRunner.Run<SecondStar>();