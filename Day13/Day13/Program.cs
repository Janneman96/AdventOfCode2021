using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main()
        {
            // get initial paper
            var inputLines = System.IO.File.ReadAllLines(@"C:\git\adventofcode2021\Day13\Day13\exampleInput.txt");
            var dots = inputLines
                .TakeWhile(inputLine =>
                    !string.IsNullOrWhiteSpace(inputLine))
                .Select(inputLine =>
                    new Coordinate
                    {
                        X = int.Parse(inputLine.Split(',')[0]),
                        Y = int.Parse(inputLine.Split(',')[1])
                    });
            var columns = dots.Max(coordinate => coordinate.X) + 1;
            var rows = dots.Max(coordinate => coordinate.Y) + 1;

            var paper = new bool[rows, columns];


            // populate paper
            foreach (var dot in dots)
            {
                paper[dot.Y, dot.X] = true;
            }


            // print paper
            Print(columns, rows, paper);


            // get folds from input
            var folds = inputLines
                .SkipWhile(inputLine => !string.IsNullOrEmpty(inputLine))
                .SkipWhile(inputLine => string.IsNullOrEmpty(inputLine))
                .TakeWhile(inputLine => !string.IsNullOrEmpty(inputLine))
                .Select(inputLine => new Fold { FoldX = inputLine.Contains('x') })
                .Take(2)//part1
                .ToArray();

            Print(columns, rows / 2, FoldY(columns, rows, paper));
            Print(columns / 2, rows / 2, FoldX(columns, rows / 2, FoldY(columns, rows, paper)));
        }

        private static void Print(int columns, int rows, bool[,] paper)
        {
            Console.WriteLine("");
            for (var y = 0; y < rows; y++)
            {
                var row = string.Empty;
                for (var x = 0; x < columns; x++)
                {
                    row += paper[y, x] ? '#' : '.';
                }
                Console.WriteLine(row);
            }
        }

        private static bool[,] FoldY(int currentMaxX, int currentMaxY, bool[,] paper)
        {
            var newMaxX = currentMaxX;
            var newMaxY = currentMaxY / 2;

            var newPaper = new bool[newMaxY, newMaxX];
            for (var y = 0; y < newMaxY; y++)
            {
                for (var x = 0; x < newMaxX; x++)
                {
                    newPaper[y, x] = paper[y, x] || paper[currentMaxY - y - 1, x];
                }
            }
            return newPaper;
        }

        private static bool[,] FoldX(int currentMaxX, int currentMaxY, bool[,] paper)
        {
            var newMaxX = currentMaxX / 2;
            var newMaxY = currentMaxY;

            var newPaper = new bool[newMaxY, newMaxX];
            for (var y = 0; y < newMaxY; y++)
            {
                if (y == 2)
                {

                }
                for (var x = 0; x < newMaxX; x++)
                {
                    newPaper[y, x] = paper[y, x] || paper[y, currentMaxX - x - 1];
                }
            }
            return newPaper;
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
