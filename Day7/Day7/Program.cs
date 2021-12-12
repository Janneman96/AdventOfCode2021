using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main()
        {
            List<int> allCrabSubmarines = File.ReadAllLines(@"C:\git\adventofcode2021Day7\Day7\input.txt")
                .First()
                .Split(',')
                .Select(n =>
                    int.Parse(n))
                .ToList();

            allCrabSubmarines.Sort();

            var fuelCost = GetTotalFuelToBestAlignPoint(allCrabSubmarines);

            Console.WriteLine(fuelCost);
        }

        static int GetTotalFuelToBestAlignPoint(List<int> allCrabSubmarines)
        {
            var middlePoint = allCrabSubmarines.Count() / 2;
            var middleFuelCost = CalculateFuelCost(allCrabSubmarines, middlePoint);
            var leftPoint = middlePoint - 1;
            var leftFuelCost = CalculateFuelCost(allCrabSubmarines, leftPoint);

            var lowestFuelCost = Math.Min(middleFuelCost, leftFuelCost);

            var leftIsCheaper = leftFuelCost < middleFuelCost;

            var i = leftIsCheaper ? leftPoint - 1 : middlePoint + 1;

            while (leftIsCheaper ? i >= allCrabSubmarines.First() : i <= allCrabSubmarines.Last())
            {
                var fuelCost = CalculateFuelCost(allCrabSubmarines, i);
                if (fuelCost > lowestFuelCost)
                {
                    break;
                }
                else
                {
                    lowestFuelCost = fuelCost;
                }

                _ = leftIsCheaper ? i-- : i++;
            }

            return lowestFuelCost;
        }

        static int CalculateFuelCost(List<int> allCrabSubmarines, int point)
        {
            // part 2
            Console.WriteLine($"Calculating for point {point}");
            var sum = 0;
            foreach (var crabSubmarine in allCrabSubmarines)
            {
                var subtotal = 0;
                for (var i = 0; i < Math.Abs(crabSubmarine - point) + 1; i++)
                {
                    sum += i;
                    subtotal += i;
                }
                //Console.WriteLine($"Move from {crabSubmarine} to {point}: {subtotal} fuel");
            }
            Console.WriteLine($"Total fuel for point {point}: {sum}");
            Console.WriteLine("####");
            return sum;

            // part 1
            //return allCrabSubmarines.Sum(crabSubmarine => Math.Abs(crabSubmarine - allCrabSubmarines[point]));
        }
    }
}
