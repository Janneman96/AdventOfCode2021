using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    class Program
    {
                static void Main()
        {
            var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021\Day14\Day14\input.txt");

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
            var characterCounts = new Dictionary<char, ulong>();

            foreach (var rule in pairInsertionRules)
            {
                pairInsertionCalculations.Add(new PairInsertionCalculation { From = rule.From, To = rule.To });
                if (!characterCounts.ContainsKey(rule.To))
                {
                    characterCounts.Add(rule.To, 0);
                }
            }

            const int steps = 20;

            var completed20StepCalculations = new Dictionary<string, ulong>();//todo use

            foreach (var calculation in pairInsertionCalculations)
            {
                calculation.NextLeft = pairInsertionCalculations.Single(c => c.From == string.Join(string.Empty, calculation.From[0], calculation.To));
                calculation.NextRight = pairInsertionCalculations.Single(c => c.From == string.Join(string.Empty, calculation.To, calculation.From[1]));
                calculation.GetCharacterCountsForSteps = (stepsRemaining) =>
                {
                    if (stepsRemaining == steps)
                    {
                        Console.WriteLine($"Calculating {calculation.From}");
                    }
                    if (stepsRemaining == 1)
                    {
                        characterCounts[calculation.To]++;//todo use reference to completed20StepCalculations, which gets passed through the 
                        characterCounts[calculation.From[1]]++;
                    }
                    else
                    {
                        calculation.NextLeft.GetCharacterCountsForSteps(stepsRemaining - 1);
                        calculation.NextRight.GetCharacterCountsForSteps(stepsRemaining - 1);
                    }
                };
            }

            for (var i = 0; i < polymerTemplate.Length - 1; i++)
            {
                var insertionRule = pairInsertionCalculations.SingleOrDefault(calculation => calculation.From == polymerTemplate.Substring(i, 2));
                insertionRule.GetCharacterCountsForSteps(steps);
            }

            characterCounts[polymerTemplate.First()]++;

            Console.WriteLine(string.Join(",", characterCounts));
        }

        class PairInsertionCalculation
        {
            public string From { get; set; }
            public char To { get; set; }
            public Action<int> GetCharacterCountsForSteps { get; set; }
            public ulong CalculateTwentySteps { get; set; }
            public PairInsertionCalculation NextLeft { get; set; }
            public PairInsertionCalculation NextRight { get; set; }
        }
    }
}