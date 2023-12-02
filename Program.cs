using AdventOfCode2023.Benchmarks.Day1;

using BenchmarkDotNet.Running;

// var input = File.ReadAllLines(@"Inputs\Day1.txt");
// Console.WriteLine(FirstStar.Naive(input));
// Console.WriteLine(FirstStar.BetterApproach(input));
// Console.WriteLine(FirstStar.UsingSearchValues(input));

BenchmarkRunner.Run<FirstStar>();