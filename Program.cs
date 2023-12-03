using AdventOfCode2023.Solutions.Day3;

using BenchmarkDotNet.Running;

var input = File.ReadAllLines(@"Inputs\Day3.txt");
Console.WriteLine(SecondStar.UsingSpans(input));

// BenchmarkRunner.Run<SecondStar>();