using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    public class Program
    {
        static void Main()
        {
            // get initial paper
            var inputFilePath = @"C:\git\adventofcode2021\Day13\Day13\input.txt";
            var inputLines = System.IO.File.ReadAllLines(inputFilePath);
            var dots = inputLines
                .TakeWhile(inputLine =>
                    !string.IsNullOrWhiteSpace(inputLine))
                .Select(inputLine =>
                    new Coordinate
                    {
                        X = int.Parse(inputLine.Split(',')[0]),
                        Y = int.Parse(inputLine.Split(',')[1])
                    });


            // get folds from input
            var folds = inputLines
                .SkipWhile(inputLine => !string.IsNullOrEmpty(inputLine))
                .SkipWhile(inputLine => string.IsNullOrEmpty(inputLine))
                .TakeWhile(inputLine => !string.IsNullOrEmpty(inputLine))
                .Select(inputLine => new Fold { FoldX = inputLine.Contains('x') });

            // prepare paper size
            var columns = 1311;//largest X * 2 + 1
            var rows = 895;//largest Y * 2 + 1

            var paper = new bool[rows, columns];


            // populate paper
            foreach (var dot in dots)
            {
                paper[dot.Y, dot.X] = true;
            }


            // print paper
            Print(columns, rows, paper);

            // Fold paper recursively
            Fold(columns, rows, paper, folds);
        }

        private static bool[,] Fold(int currentMaxX, int currentMaxY, bool[,] paper, IEnumerable<Fold> foldsLeft)
        {
            var fold = foldsLeft.First();
            var newMaxX = fold.FoldX ? currentMaxX / 2 : currentMaxX;
            var newMaxY = fold.FoldX ? currentMaxY : currentMaxY / 2;
            Console.WriteLine($"X{currentMaxX}, {newMaxX}; Y{currentMaxY}, {newMaxY}");
            var newPaper = new bool[newMaxY, newMaxX];

            var dotsCount = 0;

            for (var y = 0; y < newMaxY; y++)
            {
                for (var x = 0; x < newMaxX; x++)
                {
                    newPaper[y, x] =
                        paper[y, x]
                        ||
                        (
                            fold.FoldX
                                ? paper[y, currentMaxX - x - 1]
                                : paper[currentMaxY - y - 1, x]
                        );
                    if (newPaper[y, x])
                    {
                        dotsCount++;
                    }
                }
            }

            Console.WriteLine($"{dotsCount} dots in:");
            Print(newMaxX, newMaxY, newPaper);

            if (foldsLeft.Count() > 1)
            {
                return Fold(newMaxX, newMaxY, newPaper, foldsLeft.Skip(1));
            }
            return newPaper;
        }

        private static void Print(int columns, int rows, bool[,] paper)
        {
            for (var y = 0; y < rows; y++)
            {
                var row = string.Empty;
                for (var x = 0; x < columns; x++)
                {
                    row += paper[y, x] ? '#' : '.';
                }
                Console.WriteLine(row);
            }
            Console.WriteLine("");
        }
    }

    class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Fold
    {
        public bool FoldX { get; set; }
    }
}
