using AdventOfCode2023.Solutions.Day5;

using BenchmarkDotNet.Running;

var input = File.ReadAllLines(@"Inputs\Day5.txt");
Console.WriteLine(SecondTaskSolution.Initial(input));
// Console.WriteLine(FirstStar.LINQ(input));
// Console.WriteLine(SecondStar.Simple(input));

// BenchmarkRunner.Run<SecondStar>();