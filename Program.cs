using AdventOfCode2023.Benchmarks.Day2;

using BenchmarkDotNet.Running;

// var input = File.ReadAllLines(@"Inputs\Day2.txt");
// Console.WriteLine(FirstStar.Naive(input));
// Console.WriteLine(FirstStar.UsingRegex(input));
// Console.WriteLine(SecondStar.Naive(input));
// Console.WriteLine(SecondStar.UsingRegex(input));

BenchmarkRunner.Run<SecondStar>();