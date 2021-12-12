using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine(Part2());

            //Console.WriteLine(Part1());
        }

        private static string Part2()
        {
            var caveArea =
                System.IO.File.ReadAllLines(@"C:\git\adventofcode2021Day9\Day9\input.txt")
                    .Select(line =>
                        line.Select(character =>
                            new CavePoint
                            {
                                Counted = false,
                                Value = int.Parse(character.ToString())
                            })
                        .ToList())
                    .ToList();

            var cave = new Cave(caveArea);

            var lowestPoints = GetLowestPoints(cave);
            var basinSizes = lowestPoints.Select(lowestPoint => CalculateBasinSize(cave, lowestPoint));
            var largestBasins = basinSizes.OrderByDescending(basin => basin).ToList();
            return $"Part 2: {largestBasins[0] * largestBasins[1] * largestBasins[2]}";
        }

        private static List<CavePoint> GetLowestPoints(Cave cave)
        {
            var lowestPoints = new List<CavePoint>();

            for (var x = 0; x < cave.CaveWidth; x++)
            {
                for (var y = 0; y < cave.CaveHeight; y++)
                {
                    var center = cave.caveArea[y][x];
                    center.X = x;
                    center.Y = y;
                    var left = x > 0 ? cave.caveArea[y][x - 1] : new CavePoint();
                    var right = x < cave.CaveWidth - 1 ? cave.caveArea[y][x + 1] : new CavePoint();
                    var up = y > 0 ? cave.caveArea[y - 1][x] : new CavePoint();
                    var down = y < cave.CaveHeight - 1 ? cave.caveArea[y + 1][x] : new CavePoint();

                    if (center.Value < left.Value && center.Value < right.Value && center.Value < up.Value && center.Value < down.Value)
                    {
                        lowestPoints.Add(center);
                    }
                }
            }

            return lowestPoints;
        }

        private static int CalculateBasinSize(Cave cave, CavePoint center)
        {
            if (center.Counted || center.Value == 9)
            {
                return 0;
            }
            center.Counted = true;

            var surroundingCavePoints = new List<CavePoint>
            {
                center.X > 0 ? cave.caveArea[center.Y][center.X - 1] : new CavePoint(),
                center.X < cave.CaveWidth - 1 ? cave.caveArea[center.Y][center.X + 1] : new CavePoint(),
                center.Y > 0 ? cave.caveArea[center.Y - 1][center.X] : new CavePoint(),
                center.Y < cave.CaveHeight - 1 ? cave.caveArea[center.Y + 1][center.X] : new CavePoint()
            };

            return 1 + surroundingCavePoints.Sum(cavePoint => CalculateBasinSize(cave, cavePoint));
        }

        class CavePoint
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool Counted { get; set; } = true;
            public int Value { get; set; } = 9;
        }

        class Cave
        {
            public Cave(List<List<CavePoint>> caveArea)
            {
                this.caveArea = caveArea;
                CaveWidth = caveArea.First().Count;
                CaveHeight = caveArea.Count;
            }
            public List<List<CavePoint>> caveArea { get; }
            public int CaveWidth { get; }
            public int CaveHeight { get; }
        }

        //private static string Part1()
        //{
        //    var caveArea =
        //        System.IO.File.ReadAllLines(@"C:\git\adventofcode2021Day9\Day9\input.txt")
        //            .Select(line =>
        //                line.Select(character =>
        //                    int.Parse(
        //                        character.ToString()))
        //                .ToList())
        //            .ToList();

        //    var caveWidth = caveArea.First().Count;
        //    var caveHeight = caveArea.Count;

        //    var sum = 0;

        //    for (var x = 0; x < caveWidth; x++)
        //    {
        //        for (var y = 0; y < caveHeight; y++)
        //        {
        //            var center = caveArea[y][x];
        //            var left = x > 0 ? caveArea[y][x - 1] : int.MaxValue;
        //            var right = x < caveWidth - 1 ? caveArea[y][x + 1] : int.MaxValue;
        //            var up = y > 0 ? caveArea[y - 1][x] : int.MaxValue;
        //            var down = y < caveHeight - 1 ? caveArea[y + 1][x] : int.MaxValue;

        //            if (center < left && center < right && center < up && center < down)
        //            {
        //                sum += center + 1;
        //            }
        //        }
        //    }

        //    return $"Part 1: {sum}";
        //}
    }
}
