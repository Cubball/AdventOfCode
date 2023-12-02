using AdventOfCode2023.Benchmarks.Day2;

using BenchmarkDotNet.Running;

// var input = File.ReadAllLines(@"Inputs\Day2.txt");
// Console.WriteLine(SecondStar.Naive(input));
// Console.WriteLine(SecondStar.UsingSpans(input));

BenchmarkRunner.Run<FirstStar>();