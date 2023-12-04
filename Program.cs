using AdventOfCode2023.Benchmarks.Day4;

using BenchmarkDotNet.Running;

// var input = File.ReadAllLines(@"Inputs\Day4.txt");
// Console.WriteLine(FirstStar.Simple(input));
// Console.WriteLine(FirstStar.LINQ(input));
// Console.WriteLine(SecondStar.Simple(input));

BenchmarkRunner.Run<SecondStar>();