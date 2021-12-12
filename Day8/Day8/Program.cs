using System;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputLines = System.IO.File.ReadAllLines(@"C:\git\adventofcode2021Day8\Day8\input-Mel.txt");

            Part2(inputLines);
        }

        static void Part1(string[] inputLines)
        {
            var outputValues = inputLines.SelectMany(inputLines => inputLines.Split(" | ")[1].Split(' '));

            var searchFor = new int[] { 2, 3, 4, 7 };
            var count = outputValues.Count(outputValue => searchFor.Contains(outputValue.Length));

            Console.WriteLine(count);
        }

        static void Part2(string[] inputLines)
        {
            var total = 0;
            foreach (var inputLine in inputLines)
            {
                var numberHint = inputLine.Split(" | ")[0].Split(' ');
                var encryptedNumbers = GetEncryptedNumbers(numberHint);

                var encryptedOutputs = inputLine.Split(" | ")[1].Split(' ');
                var subtotal = GetSubtotal(encryptedOutputs, encryptedNumbers);
                total += subtotal;
            }
            Console.WriteLine(total);
        }

        private static int GetSubtotal(string[] encryptedOutputs, string[] digits)
        {
            var output = string.Empty;

            foreach (var encryptedOutput in encryptedOutputs)
            {
                for (var i = 0; i < digits.Length; i++)
                {
                    var digit = digits[i];
                    if (encryptedOutput.Length == digit.Length && encryptedOutput.Except(digit).Count() == 0)
                    {
                        output += i;
                        break;
                    }
                }
            }

            return int.Parse(output);
        }

        private static string[] GetEncryptedNumbers(string[] numberHint)
        {
            var number1 = numberHint.Single(n => n.Length == 2);
            var number7 = numberHint.Single(n => n.Length == 3);
            var number4 = numberHint.Single(n => n.Length == 4);
            var number8 = numberHint.Single(n => n.Length == 7);
            var number6 = numberHint.Single(n => n.Length == 6 && !(n.Contains(number1[0]) && n.Contains(number1[1])));
            var number3 = numberHint.Single(n => n.Length == 5 && n.Contains(number1[0]) && n.Contains(number1[1]));
            var number9 = numberHint.Single(n => n.Length == 6 && n.Except(number3).Count() == 1);
            var number0 = numberHint.Single(n => n.Length == 6 && n != number6 && n != number9);
            var number2 = numberHint.Single(n => n.Length == 5 && n != number3 && n.Contains(number8.Except(number9).Single()));
            var number5 = numberHint.Single(n => n.Length == 5 && n != number3 && n != number2);

            return new string[] { number0, number1, number2, number3, number4, number5, number6, number7, number8, number9 };
        }
    }
}
