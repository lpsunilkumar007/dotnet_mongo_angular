// See https://aka.ms/new-console-template for more information
using BinaryStringAnalysis;

Console.WriteLine(@"Requirements:
1. The function accepts a binary string as input.
2. Check if the binary string is 'good' based on these conditions:
   i) Equal number of 0's and 1's.
   ii) For every prefix, the number of 1's is not less than the number of 0's.
");


BinaryStringEvaluator.RunTests();


Console.ReadKey();