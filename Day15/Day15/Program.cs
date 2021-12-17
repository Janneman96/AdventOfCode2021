using System;
using System.Collections.Generic;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main()
        {
            var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021\Day15\Day15\example.txt");
            Cavern cavern = GetCavern(inputLines);
            Helper.Cavern = cavern;

            for (var step = 0; step < 100; step++)
            {
                foreach (var position in cavern.List)
                {
                    position.CalculateTotalRisk();
                }
                Helper.PrintCavern();
            }
        }

        private static Cavern GetCavern(string[] inputLines)
        {
            var maxRowIndex = inputLines.First().Length - 1;
            var maxColumnIndex = inputLines.Length - 1;

            var cavernArray2d = new Position[maxColumnIndex + 1, maxRowIndex + 1];
            var cavernList = new List<Position>();

            for (var rowIndex = 0; rowIndex <= maxRowIndex; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex <= maxColumnIndex; columnIndex++)
                {
                    var position = new Position
                    {
                        RiskLevel = int.Parse(inputLines[columnIndex][rowIndex].ToString()),
                        ColumnIndex = columnIndex,
                        RowIndex = rowIndex,
                        TotalRisk = int.MaxValue
                    };
                    cavernArray2d[columnIndex, rowIndex] = position;
                    cavernList.Add(position);
                }
            }

            for (var rowIndex = maxRowIndex; rowIndex >= 0; rowIndex--)
            {
                for (var columnIndex = maxColumnIndex; columnIndex >= 0; columnIndex--)
                {
                    var right = columnIndex < maxColumnIndex ? cavernArray2d[columnIndex + 1, rowIndex] : null;
                    var left = columnIndex > 0 ? cavernArray2d[columnIndex - 1, rowIndex] : null;
                    var bottom = rowIndex < maxRowIndex ? cavernArray2d[columnIndex, rowIndex + 1] : null;
                    var top = rowIndex > 0 ? cavernArray2d[columnIndex, rowIndex - 1] : null;

                    if (right != null)
                    {
                        cavernArray2d[columnIndex, rowIndex].Right = right;
                        cavernList.Add(right);
                    }
                    if (bottom != null)
                    {
                        cavernArray2d[columnIndex, rowIndex].Bottom = bottom;
                        cavernList.Add(bottom);
                    }
                    if (left != null)
                    {
                        cavernArray2d[columnIndex, rowIndex].Left = left;
                        cavernList.Add(left);
                    }
                    if (top != null)
                    {
                        cavernArray2d[columnIndex, rowIndex].Top = (top);
                        cavernList.Add(top);
                    }
                }
            }

            return new Cavern { Array2d = cavernArray2d, List = cavernList };
        }
    }

    class Cavern
    {
        public Position[,] Array2d { get; set; }
        public List<Position> List { get; set; }
    }

    class Position
    {
        public int ColumnIndex { get; set; }
        public int RowIndex { get; set; }
        public int RiskLevel { get; set; }
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
        public int TotalRisk { private get; set; }
        public int CalculateTotalRisk()
        {
            if (TotalRisk != int.MaxValue)
            {
                return TotalRisk;
            }
            if (Right == null && Bottom == null)
            {
                TotalRisk = RiskLevel;
            }

            var lowestTotalRisk = int.MaxValue;
            lowestTotalRisk = ConnectedPositions.Min(connectedPosition => connectedPosition.TotalRisk);

            foreach (var connectedPosition in ConnectedPositions)
            {
                if (connectedPosition.TotalRisk != int.MaxValue)
                {
                    CalculateTotalRisk(lowestTotalRisk, 0);
                }
            }
            TotalRisk = RiskLevel + ConnectedPositions.Min(connectedPosition => connectedPosition.TotalRisk);
            return TotalRisk;
        }

        public int CalculateTotalRisk(int budget, int price)
        {
            if (TotalRisk != int.MaxValue)
            {
                return TotalRisk;
            }

            return RiskLevel + ConnectedPositions.Min(connectedPosition => connectedPosition.CalculateTotalRisk(budget, price + RiskLevel));
        }
    }

    class Helper
    {
        public static ulong Iterations { get; set; } = 0;
        public static Cavern Cavern { get; set; }
        public static void PrintCavern()
        {
            for (var column = 0; column < Cavern.Array2d.GetLength(0); column++)
            {
                for (var row = 0; row < Cavern.Array2d.GetLength(1); row++)
                {
                    var position = Cavern.Array2d[column, row];
                    if (position.CalculateTotalRisk() != int.MaxValue)
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
