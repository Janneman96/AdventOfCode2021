using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    public class Program
    {
        static void Main()
        {
            /*
            var cavern = GetCavern($@"C:\git\adventofcode2021\Day15\Day15\input.txt");
            var cavern = GetCavern($@"C:\git\adventofcode2021\Day15\Day15\example.txt");
             */
            var cavern = GetCavern($@"C:\git\adventofcode2021\Day15\Day15\goesBackUp.txt");
            var totalRisk = GetTotalRisk(cavern);
            PrintAnswers(cavern, totalRisk);
            cavern[0, 0].PrintBestRoute(new List<Position>());
        }

        private static Position[,] GetCavern(string filePath) => GetCavern(System.IO.File.ReadAllLines(filePath));

        public static Position[,] GetCavern(string[] inputLines)
        {
            var maxX = inputLines.First().Length - 1;
            var maxY = inputLines.Length - 1;

            var cavernArray2d = new Position[maxY + 1, maxX + 1];
            var cavernList = new List<Position>();

            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    var position = new Position
                    {
                        RiskLevel = int.Parse(inputLines[y][x].ToString()),
                        Y = y,
                        X = x,
                        TotalRisk = int.MaxValue
                    };
                    position.Print(ConsoleColor.DarkGray);
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

        public static object GetTotalRisk(Position[,] cavern)
        {
            CalculateEachTotalRisk(cavern);
            return cavern[0, 0].TotalRisk - cavern[0, 0].RiskLevel;
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
            }
            for (var setSize = Math.Max(maxX, maxY) - 1; setSize >= 0; setSize--)
            {
                for (var loopInSet = 0; loopInSet <= setSize; loopInSet++)
                {
                    var y = setSize - loopInSet;
                    var x = loopInSet;
                    cavern[y, x].CalculateTotalRisk();
                }
            }
        }
        private static void PrintAnswers(Position[,] cavern, object totalRisk)
        {
            for (var i = 0; i < cavern.GetLength(0) + 1; i++) Console.Write('\n');
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"The total risk of the safest route is ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(totalRisk);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('.');
        }
    }

    public class Position
    {
        public void Print(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(X, Y);
            Console.Write(RiskLevel);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintBestRoute(IEnumerable<Position> visited)
        {
            if (visited.Contains(this))
            {
                return;
            }
            Print(ConsoleColor.Green);
            var lowestRisk = ConnectedPositions.OrderBy(position => position.TotalRisk).First().TotalRisk;
            foreach (var position in ConnectedPositions.Where(position => position.TotalRisk == lowestRisk))
            {
                position.PrintBestRoute(visited.Concat(new List<Position> { this }));
            }
        }

        public int Y { get; set; }
        public int X { get; set; }
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
            if (TotalRisk == int.MaxValue)
            {
                if (IsEndPosition)
                {
                    TotalRisk = RiskLevel;
                }
                else
                {
                    var lowestTotalRisk = int.MaxValue;
                    lowestTotalRisk = ConnectedPositions.Min(connectedPosition => connectedPosition.TotalRisk);

                    foreach (var connectedPosition in ConnectedPositions)
                    {
                        if (connectedPosition.TotalRisk != int.MaxValue)
                        {
                            CalculateTotalRisk(new List<Position> { this }, lowestTotalRisk, 0, X, Y);
                        }
                    }
                    TotalRisk = RiskLevel + ConnectedPositions.Min(connectedPosition => connectedPosition.TotalRisk);
                }
            }

            Print(ConsoleColor.White);

            return TotalRisk;
        }

        public int CalculateTotalRisk(IEnumerable<Position> visitedPositions, int budget, int price, int routeToX, int routeToY)
        {
            if (price > budget)
            {
                return int.MaxValue;
            }
            if (TotalRisk != int.MaxValue)
            {
                return TotalRisk;
            }

            Print(ConsoleColor.Yellow);
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
                            price + RiskLevel,
                            routeToX,
                            routeToY));
            }

            return int.MaxValue;
        }
    }
}
