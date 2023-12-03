using AdventOfCode2023.Solutions.Day3;

using BenchmarkDotNet.Running;

var input = File.ReadAllLines(@"Inputs\Day3.txt");
Console.WriteLine(FirstStar.UsingSpans(input));
Console.WriteLine(FirstStar.UsingRegex(input));

// BenchmarkRunner.Run<FirstStar>();