using System;
using System.Collections.Generic;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Pathing
    {
        public ICollection<string> GetPaths(string filename)
        {
            var paths = new List<string>();
            var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021Day10\Day10\{filename}.txt");

            return paths;
        }
    }
}
