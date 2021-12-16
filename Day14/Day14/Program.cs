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

            const int steps = 40;

            var completed20StepCalculations = new Dictionary<string, Dictionary<char, ulong>>();

            foreach (var calculation in pairInsertionCalculations)
            {
                calculation.NextLeft = pairInsertionCalculations.Single(c => c.From == string.Join(string.Empty, calculation.From[0], calculation.To));
                calculation.NextRight = pairInsertionCalculations.Single(c => c.From == string.Join(string.Empty, calculation.To, calculation.From[1]));
                calculation.GetCharacterCountsForSteps = (stepsRemaining, originalFrom) =>
                {
                    if (stepsRemaining == steps)
                    {
                        Console.WriteLine($"Calculating {calculation.From}");
                        if (!completed20StepCalculations.ContainsKey(originalFrom))
                        {
                            completed20StepCalculations.Add(originalFrom, new Dictionary<char, ulong>());
                        }
                    }

                    if (stepsRemaining > 20)
                    {
                        if (completed20StepCalculations.ContainsKey(originalFrom))
                        {
                            if (!completed20StepCalculations[originalFrom].ContainsKey(calculation.To))
                            {
                                completed20StepCalculations[originalFrom].Add(calculation.To, 0);
                            }
                            if (!completed20StepCalculations[originalFrom].ContainsKey(calculation.From[1]))
                            {
                                completed20StepCalculations[originalFrom].Add(calculation.From[1], 0);
                            }
                            completed20StepCalculations[originalFrom][calculation.To]++;
                            completed20StepCalculations[originalFrom][calculation.From[1]]++;
                        }
                        
                        calculation.NextLeft.GetCharacterCountsForSteps(stepsRemaining - 1, originalFrom);
                        calculation.NextRight.GetCharacterCountsForSteps(stepsRemaining - 1, originalFrom);
                    }
                    else
                    {
                        if (stepsRemaining == 1)
                        {
                            characterCounts[calculation.To]++;
                            characterCounts[calculation.From[1]]++;
                        }
                        else
                        {
                            if (stepsRemaining == 20 && completed20StepCalculations.ContainsKey(calculation.From))
                            {
                                var completed20StepCalculation = completed20StepCalculations[calculation.From];
                                foreach (var characterWithCount in completed20StepCalculation)
                                {
                                    characterCounts[characterWithCount.Key] += characterWithCount.Value;
                                }
                            }
                            else
                            {
                                calculation.NextLeft.GetCharacterCountsForSteps(stepsRemaining - 1, originalFrom);
                                calculation.NextRight.GetCharacterCountsForSteps(stepsRemaining - 1, originalFrom);
                            }
                        }
                    }
                };
            }

            for (var i = 0; i < polymerTemplate.Length - 1; i++)
            {
                var insertionRule = pairInsertionCalculations.SingleOrDefault(calculation => calculation.From == polymerTemplate.Substring(i, 2));
                insertionRule.GetCharacterCountsForSteps(steps, insertionRule.From);//todo: problem; looping through the bottom 20 branches before going through the top 20 branches.
                // possible solution: execute with 20 step depth, then save all results into completed20StepCalculations. Then run again while getting the values from completed20StepCalculations instead of calculating again.
            }

            characterCounts[polymerTemplate.First()]++;

            Console.WriteLine(string.Join(",", characterCounts));
        }

        class PairInsertionCalculation
        {
            public string From { get; set; }
            public char To { get; set; }
            public Action<int, string> GetCharacterCountsForSteps { get; set; }
            public ulong CalculateTwentySteps { get; set; }
            public PairInsertionCalculation NextLeft { get; set; }
            public PairInsertionCalculation NextRight { get; set; }
        }
    }
}