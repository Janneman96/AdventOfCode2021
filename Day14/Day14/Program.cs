using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    class Program
    {
        static void Main()
        {
            var datetimeStart = DateTime.Now;
            var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021\Day14\Day14\input.txt");
            const int steps = 40;

            var polymerTemplate = inputLines.First();
            Console.WriteLine($"Template: {polymerTemplate}");

            var pairInsertionRules = inputLines
                .Skip(2)
                .Select(inputLine =>
                    new PairInsertionCalculation
                    {
                        From = inputLine.Split(" -> ")[0],
                        To = inputLine.Split(" -> ")[1][0]
                    }).ToList();

            var pairInsertionCalculations = new List<PairInsertionCalculation>();

            foreach (var rule in pairInsertionRules)
            {
                pairInsertionCalculations.Add(new PairInsertionCalculation { From = rule.From, To = rule.To });
            }

            foreach (var calculation in pairInsertionCalculations)
            {
                calculation.NextLeft = pairInsertionCalculations.Single(c => c.From == string.Join(string.Empty, calculation.From[0], calculation.To));
                calculation.NextRight = pairInsertionCalculations.Single(c => c.From == string.Join(string.Empty, calculation.To, calculation.From[1]));
            }

            var calculationResults = new List<Dictionary<char, ulong>>();
            for (var i = 0; i < polymerTemplate.Length - 1; i++)
            {
                var insertionCalculation = pairInsertionCalculations.SingleOrDefault(calculation => calculation.From == polymerTemplate.Substring(i, 2));
                calculationResults.Add(insertionCalculation.GetCharacterCountsForSteps(steps));
            }

            var characterCounts = new Dictionary<char, ulong>();

            foreach (var result in calculationResults)
            {
                foreach (var charResult in result)
                {
                    if (!characterCounts.ContainsKey(charResult.Key))
                    {
                        characterCounts.Add(charResult.Key, charResult.Value);
                    }
                    else
                    {
                        characterCounts[charResult.Key] += charResult.Value;
                    }
                }
            }
            characterCounts[polymerTemplate.First()]++;

            Console.WriteLine(string.Join(",", characterCounts));
            Console.WriteLine($"{steps} steps completed in {(DateTime.Now - datetimeStart).Milliseconds} milliseconds.");

            Console.WriteLine(characterCounts.Max(characterCount => characterCount.Value) - characterCounts.Min(characterCount => characterCount.Value));
        }

        class PairInsertionCalculation
        {
            public string From { get; set; }
            public char To { get; set; }
            public ulong CalculateTwentySteps { get; set; }
            public PairInsertionCalculation NextLeft { get; set; }
            public PairInsertionCalculation NextRight { get; set; }
            private Dictionary<int, Dictionary<char, ulong>> Solutions { get; set; } = new Dictionary<int, Dictionary<char, ulong>>();
            public Dictionary<char, ulong> GetCharacterCountsForSteps(int steps)
            {
                if (Solutions.ContainsKey(steps))
                {
                    return Solutions[steps];
                }
                var solution = new Dictionary<char, ulong>();
                if (steps == 1)
                {
                    solution.Add(To, 1);
                    if (!solution.ContainsKey(From[1]))
                    {
                        solution.Add(From[1], 1);
                    }
                    else
                    {
                        solution[From[1]]++;
                    }
                }
                else
                {
                    var leftSolutions = NextLeft.GetCharacterCountsForSteps(steps - 1);
                    var rightSolutions = NextRight.GetCharacterCountsForSteps(steps - 1);
                    foreach (var leftSolution in leftSolutions)
                    {
                        solution.Add(leftSolution.Key, leftSolution.Value);
                    }
                    foreach (var rightSolution in rightSolutions)
                    {
                        if (!solution.ContainsKey(rightSolution.Key))
                        {
                            solution.Add(rightSolution.Key, rightSolution.Value);
                        }
                        else
                        {
                            solution[rightSolution.Key] += rightSolution.Value;
                        }
                    }
                }
                Solutions.Add(steps, solution);
                return solution;
            }
        }
    }
}