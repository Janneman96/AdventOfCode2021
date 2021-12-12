using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    class Program
    {
        static ulong flashes = 0;
        static int steps = 0;
        static void Main()
        {
            var octopusses = System.IO.File.ReadAllLines(@"C:\git\adventofcode2021Day11\Day11\input.txt").Select(inputLine => inputLine.Select(character => new Octopus { FlashedInThisStep = false, Value = int.Parse(character.ToString()) }).ToList()).ToList();
            //var octopusses = System.IO.File.ReadAllLines(@"C:\git\adventofcode2021Day11\Day11\exampleInput.txt").Select(inputLine => inputLine.Select(character => new Octopus { FlashedInThisStep = false, Value = int.Parse(character.ToString()) }).ToList()).ToList();
            //var octopusses = System.IO.File.ReadAllLines(@"C:\git\adventofcode2021Day11\Day11\easyExample.txt").Select(inputLine => inputLine.Select(character => new Octopus { FlashedInThisStep = false, Value = int.Parse(character.ToString()) }).ToList()).ToList();

            Print(octopusses, 0);
            while (true)
            {
                steps++;
                for (var x = 0; x < octopusses.First().Count; x++)
                {
                    for (var y = 0; y < octopusses.Count; y++)
                    {
                        Age(octopusses, x, y);
                    }
                }
                Print(octopusses, steps + 1);

                if (octopusses.All(row => row.All(o => o.FlashedInThisStep)))
                {
                    goto label1;
                }
                foreach (var row in octopusses)
                {
                    foreach (var octopus in row.Where(o => o.FlashedInThisStep))
                    {
                        octopus.FlashedInThisStep = false;
                    }
                }
            }
        label1:

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Flashes: {flashes}");
            Console.WriteLine($"Steps: {steps}");
        }

        private static void Print(List<List<Octopus>> octopusses, int step)
        {
            Console.WriteLine();
            Console.WriteLine($"After step {step}:");
            foreach (var row in octopusses)
            {
                foreach (var octopus in row)
                {
                    Console.Write(octopus.Value);
                }
                Console.WriteLine();
            }
        }

        private static void Age(List<List<Octopus>> octopusses, int octopusX, int octopusY)
        {
            if (octopusX < octopusses.First().Count && octopusY < octopusses.Count && octopusX >= 0 && octopusY >= 0)
            {
                var octopus = octopusses[octopusY][octopusX];

                if (!octopus.FlashedInThisStep)
                {
                    octopus.Value += 1;

                    if (octopus.Value >= 10)
                    {
                        flashes++;
                        octopus.FlashedInThisStep = true;

                        for (var xModifier = -1; xModifier <= 1; xModifier++)
                        {
                            for (var yModifier = -1; yModifier <= 1; yModifier++)
                            {
                                if (xModifier != 0 || yModifier != 0)
                                {
                                    Age(octopusses, octopusX + xModifier, octopusY + yModifier);
                                }
                            }
                        }

                        octopus.Value = 0;
                    }
                }
            }
        }

        class Octopus
        {
            public bool FlashedInThisStep { get; set; }
            public int Value { get; set; }
        }
    }
}
