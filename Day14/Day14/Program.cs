using System;
using System.Linq;

namespace Day14
{
    class Program
    {
        static void Main()
        {
            var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021\Day14\Day14\example.txt");

            var polymerTemplate = inputLines.First();
            Console.WriteLine($"Template: {polymerTemplate}");

            var pairInsertionRules = inputLines
                .Skip(2)
                .Select(inputLine =>
                    new PairInsertionRule
                    {
                        From = inputLine.Split(" -> ")[0],
                        To = inputLine.Split(" -> ")[1]
                    }).ToList();



            var temporaryPolymerTemplate = string.Empty;
            for (var step = 1; step <= 100; step++)
            {
                for (var i = 0; i < polymerTemplate.Length - 1; i++)
                {
                    var insertionRule = pairInsertionRules.SingleOrDefault(rule => rule.From == polymerTemplate.Substring(i, 2));
                    if (insertionRule != null)
                    {
                        temporaryPolymerTemplate += insertionRule.To;
                    }
                }
                polymerTemplate = polymerTemplate[0] + temporaryPolymerTemplate;
                temporaryPolymerTemplate = string.Empty;

                //Console.WriteLine($"After step {step}: {polymerTemplate}");
                //Console.WriteLine($"After step {step}: {polymerTemplate.Length}");
                //Console.WriteLine($"Processed step {step}");
                var ruleUsedCount = pairInsertionRules.Count(rule => polymerTemplate.Contains(rule.From));
                Console.WriteLine($"contains {ruleUsedCount} rules. Rules unused: {string.Join(',', pairInsertionRules.Where(rule => !polymerTemplate.Contains(rule.From)).Select(rule => rule.From))}");
            }
            var maxChar = polymerTemplate.GroupBy(character => character).Max(group => group.Count());
            var minChar = polymerTemplate.GroupBy(character => character).Min(group => group.Count());

            Console.WriteLine("max " + maxChar);
            Console.WriteLine("min " + minChar);
            Console.WriteLine(maxChar - minChar);
        }

        class PairInsertionRule
        {
            private char to { get; set; }
            public string From { get; set; }
            public string To
            {
                get
                {
                    return string.Join(
                        string.Empty,
                        new char[] {
                            //From[0],
                            to,
                            From[1] });
                }
                set
                {
                    to = value[0];
                }
            }
        }
    }
}