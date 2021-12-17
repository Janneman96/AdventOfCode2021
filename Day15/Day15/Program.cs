using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main()
        {
            var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021\Day15\Day15\input.txt");
            var cavern = GetCavern(inputLines);
            Logger.Cavern = cavern;

            CalculateEachTotalRisk(cavern);

            Console.WriteLine();
            Console.Write($"The total risk of the safest route is ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(cavern[0, 0].TotalRisk - cavern[0,0].RiskLevel);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('.');
        }

        private static void CalculateEachTotalRisk(Position[,] cavern)
        {
            var maxX = cavern.GetLength(1) - 1;
            var maxY = cavern.GetLength(0) - 1;

            for (var setSize = 0; setSize <= Math.Max(maxX, maxY); setSize++)
            {
                for (var loopInSet = 0; loopInSet <= setSize; loopInSet++)
                {
                    var y = maxY - loopInSet;
                    var x = maxX + loopInSet - setSize;
                    cavern[y, x].CalculateTotalRisk();
                }
                Logger.PrintCavern();
            }
            for (var setSize = Math.Max(maxX, maxY) - 1; setSize >= 0; setSize--)
            {
                for (var loopInSet = 0; loopInSet <= setSize; loopInSet++)
                {
                    var y = setSize - loopInSet;
                    var x = loopInSet;
                    cavern[y, x].CalculateTotalRisk();
                }
                Logger.PrintCavern();
            }
        }

        private static Position[,] GetCavern(string[] inputLines)
        {
            var maxX = inputLines.First().Length - 1;
            var maxY = inputLines.Length - 1;

            var cavernArray2d = new Position[maxY + 1, maxX + 1];
            var cavernList = new List<Position>();

            for (var x = 0; x <= maxX; x++)
            {
                for (var y = 0; y <= maxY; y++)
                {
                    var position = new Position
                    {
                        RiskLevel = int.Parse(inputLines[y][x].ToString()),
                        ColumnIndex = y,
                        RowIndex = x,
                        TotalRisk = int.MaxValue
                    };
                    cavernArray2d[y, x] = position;
                    cavernList.Add(position);
                }
            }

            for (var y = maxY; y >= 0; y--)
            {
                for (var x = maxX; x >= 0; x--)
                {
                    var right = x < maxX ? cavernArray2d[y, x + 1] : null;
                    var left = x > 0 ? cavernArray2d[y, x - 1] : null;
                    var bottom = y < maxY ? cavernArray2d[y + 1, x] : null;
                    var top = y > 0 ? cavernArray2d[y - 1, x] : null;

                    if (right != null)
                    {
                        cavernArray2d[y, x].Right = right;
                    }
                    if (bottom != null)
                    {
                        cavernArray2d[y, x].Bottom = bottom;
                    }
                    if (left != null)
                    {
                        cavernArray2d[y, x].Left = left;
                    }
                    if (top != null)
                    {
                        cavernArray2d[y, x].Top = top;
                    }
                }
            }

            return cavernArray2d;
        }
    }

    class Position
    {
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        public int RiskLevel { get; set; }
        public bool IsEndPosition
        {
            get
            {
                return Right == null && Bottom == null;
            }
        }
        public Position Top { get; set; }
        public Position Right { get; set; }
        public Position Bottom { get; set; }
        public Position Left { get; set; }
        public Position[] ConnectedPositions
        {
            get
            {
                var positions = new List<Position>();
                if (Top != null) positions.Add(Top);
                if (Right != null) positions.Add(Right);
                if (Bottom != null) positions.Add(Bottom);
                if (Left != null) positions.Add(Left);
                return positions.ToArray();
            }
        }
        public int TotalRisk { get; set; }
        public int CalculateTotalRisk()
        {
            if (TotalRisk != int.MaxValue)
            {
                return TotalRisk;
            }
            if (IsEndPosition)
            {
                TotalRisk = RiskLevel;
                return TotalRisk;
            }

            var lowestTotalRisk = int.MaxValue;
            lowestTotalRisk = ConnectedPositions.Min(connectedPosition => connectedPosition.TotalRisk);

            foreach (var connectedPosition in ConnectedPositions)
            {
                if (connectedPosition.TotalRisk != int.MaxValue)
                {
                    CalculateTotalRisk(new List<Position> { this }, lowestTotalRisk, 0);
                }
            }
            TotalRisk = RiskLevel + ConnectedPositions.Min(connectedPosition => connectedPosition.TotalRisk);
            return TotalRisk;
        }

        public int CalculateTotalRisk(IEnumerable<Position> visitedPositions, int budget, int price)
        {
            if (price > budget)
            {
                return int.MaxValue;
            }
            if (TotalRisk != int.MaxValue)
            {
                return TotalRisk;
            }

            var unvisitedNeighbours = ConnectedPositions.Except(visitedPositions);

            if (unvisitedNeighbours.Any())
            {
                return
                RiskLevel +
                ConnectedPositions
                    .Except(visitedPositions)
                    .Min(connectedPosition =>
                        connectedPosition.CalculateTotalRisk(
                            visitedPositions.Concat(new List<Position> { this }),
                            budget,
                            price + RiskLevel));
            }

            return int.MaxValue;
        }
    }

    class Logger
    {
        public static ulong Iterations { get; set; } = 0;
        public static Position[,] Cavern { get; set; }
        public static void PrintCavern()
        {
            Console.WriteLine();
            Console.WriteLine($"Iteration {++Iterations}");
            for (var column = 0; column < Cavern.GetLength(0); column++)
            {
                for (var row = 0; row < Cavern.GetLength(1); row++)
                {
                    var position = Cavern[column, row];
                    if (position.TotalRisk != int.MaxValue)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(position.RiskLevel);
                }
                Console.WriteLine();
            }
        }
    }
}
