using System;
using System.Collections.Generic;

namespace Day5
{
    public class Program
    {
        static void Main(string[] args)
        {
            var inputLines = System.IO.File.ReadAllLines(@"C:\git\adventofcode2021Day5\Day5\input.txt");

            var clouds = new Dictionary<string, int>();

            var cloudOverlapPoints = 0;

            foreach (var inputLine in inputLines)
            {
                var splittedLine = inputLine.Split(" -> ");
                var point1 = splittedLine[0];
                var point2 = splittedLine[1];
                var splittedPoint1 = point1.Split(',');
                var splittedPoint2 = point2.Split(',');
                var x1 = int.Parse(splittedPoint1[0]);
                var y1 = int.Parse(splittedPoint1[1]);
                var x2 = int.Parse(splittedPoint2[0]);
                var y2 = int.Parse(splittedPoint2[1]);

                foreach (var key in GetPoints(x1, y1, x2, y2))
                {
                    if (clouds.ContainsKey(key))
                    {
                        var current = ++clouds[key];
                        if (current == 2)
                        {
                            cloudOverlapPoints++;
                        }
                    }
                    else
                    {
                        clouds.Add(key, 1);
                    }
                }
            }

            Console.WriteLine($"Number of points where at least two lines overlap: {cloudOverlapPoints}");//juiste antwoord is 5 bij example
        }

        public static List<string> GetPoints(int x1, int y1, int x2, int y2)
        {
            var clouds = new List<string>();

            var x = x1;
            var y = y1;

            var doReverseOrderX = x1 > x2;
            var doReverseOrderY = y1 > y2;

            while ((doReverseOrderX ? x >= x2 : x <= x2) && (doReverseOrderY ? y >= y2 : y <= y2))
            {
                var key = $"{x},{y}";
                if (!clouds.Contains(key))
                {
                    clouds.Add(key);
                }
                if (x1 != x2)
                {
                    _ = doReverseOrderX ? x-- : x++;
                }
                if (y1 != y2)
                {
                    _ = doReverseOrderY ? y-- : y++;
                }
                if (x1 == x2 && y1 == y2)
                {
                    break;
                }
            }

            return clouds;
        }






        // ########## Failed attempt ##########

        //private const int DiagramWidth = 10;
        //private const int DiagramHeight = 10;

        //static void Main(string[] args)
        //{
        //    var inputLines = System.IO.File.ReadAllLines(@"C:\git\adventofcode2021Day5\Day5\exampleInput.txt");

        //    var diagram = new Diagram();

        //    foreach (var line in inputLines)
        //    {
        //        diagram.AddLine(GetCloud(line));
        //    }

        //    diagram.WriteToConsole();

        //    Console.WriteLine($"Total overlap: {diagram.AmountOfOverlap}");
        //}

        //class Diagram
        //{
        //    private int[,] diagram { get; set; }
        //    public int AmountOfOverlap { get; private set; }

        //    public Diagram()
        //    {
        //        diagram = new int[DiagramWidth, DiagramHeight];
        //    }

        //    public void AddLine(Cloud cloud)
        //    {
        //        if (cloud.X1 == cloud.X2)
        //        {
        //            for (var y = Math.Min(cloud.Y1, cloud.Y2); y <= Math.Max(cloud.Y1, cloud.Y2); y++)
        //            {
        //                var current = ++diagram[cloud.X1, y];
        //                if (current == 2)
        //                {
        //                    AmountOfOverlap++;
        //                }
        //            }
        //        }
        //        else if (cloud.Y1 == cloud.Y2)
        //        {
        //            for (var x = Math.Min(cloud.X1, cloud.X2); x <= Math.Max(cloud.X1, cloud.X2); x++)
        //            {
        //                var current = ++diagram[x, cloud.Y1];
        //                if (current == 2)
        //                {
        //                    AmountOfOverlap++;
        //                }
        //            }
        //        }
        //    }

        //    public void WriteToConsole()
        //    {
        //        for (var y = 0; y < DiagramHeight; y++)
        //        {
        //            var writeLine = string.Empty;
        //            for (var x = 0; x < DiagramWidth; x++)
        //            {
        //                writeLine += diagram[x, y];
        //            }
        //            Console.WriteLine(writeLine);
        //        }
        //    }
        //}

        //static Cloud GetCloud(string inputLine)
        //{
        //    var splittedLine = inputLine.Split(" -> ");
        //    var point1 = splittedLine[0];
        //    var point2 = splittedLine[1];
        //    var splittedPoint1 = point1.Split(',');
        //    var splittedPoint2 = point2.Split(',');

        //    return new Cloud
        //    {
        //        X1 = int.Parse(splittedPoint1[0]),
        //        Y1 = int.Parse(splittedPoint1[1]),
        //        X2 = int.Parse(splittedPoint2[0]),
        //        Y2 = int.Parse(splittedPoint2[1])
        //    };
        //}

        //class Cloud
        //{
        //    public int X1 { get; set; }
        //    public int Y1 { get; set; }
        //    public int X2 { get; set; }
        //    public int Y2 { get; set; }
        //}
    }
}
