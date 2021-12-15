using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    class Program
    {
        static void Main()
        {
            var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021\Day12\Day12\input.txt");
            var pathing = new Pathing();
            var paths = pathing.GetPaths(inputLines);
            Console.WriteLine(string.Join('\n', paths));
            Console.WriteLine($"Paths: {paths.Count()}");
            //Console.WriteLine(paths.Count(path => path.Split(',').Skip(1).Take(path.Count() - 2).Any(character => char.IsLower(character[0]))));//part 1 LOL
        }
    }

    public class Pathing
    {
        private ICollection<Cave> Caves { get; } = new List<Cave>();

        public IEnumerable<string> GetPaths(string[] inputLines)
        {
            AddCaves(inputLines);
            var startCave = Caves.Single(cave => cave.Name == "start");
            var paths = GetPaths(new List<Cave> { startCave });
            var pathStrings = paths.Select(path => string.Join(',', path.Select(cave => cave.Name)));
            return pathStrings;
        }

        private IEnumerable<List<Cave>> GetPaths(List<Cave> subPath)
        {
            var newPaths = new List<List<Cave>>();
            var currentCave = subPath.Last();
            if (currentCave.Name == "end")
            {
                newPaths.Add(subPath);
            }
            else if (currentCave.Name == "start" && subPath.Count(cave => cave.Name == "start") > 1)
            {

            }
            else if (
                char.IsUpper(currentCave.Name[0])
                || !subPath.Take(subPath.Count() - 1).Contains(currentCave)
                || !subPath.Take(subPath.Count() - 1).GroupBy(cave => cave.Name).ToList().Any(group => char.IsLower(group.First().Name[0]) && group.Count() > 1))
            {
                foreach (var connectedTo in currentCave.ConnectedTo)
                {
                    var newSubPath = subPath.Concat(new List<Cave> { connectedTo }).ToList();
                    newPaths.AddRange(GetPaths(newSubPath));
                }
            }
            return newPaths;
        }

        private void AddCaves(string[] connections)
        {
            foreach (var connection in connections)
            {
                var caveNames = connection.Split('-');
                var leftName = caveNames[0];
                var rightName = caveNames[1];

                var leftCave = GetCave(leftName);
                var rightCave = GetCave(rightName);

                leftCave.ConnectedTo.Add(rightCave);
                rightCave.ConnectedTo.Add(leftCave);
            }
        }

        private Cave GetCave(string name)
        {
            var cave = Caves.SingleOrDefault(cave => cave.Name == name);
            if (cave == null)
            {
                cave = new Cave(name);
                Caves.Add(cave);
            }
            return cave;
        }
    }

    public class Cave
    {
        public Cave(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public ICollection<Cave> ConnectedTo { get; } = new List<Cave>();

        public bool IsBigCave() => char.IsUpper(Name[0]);
        public bool IsSmallCave() => !IsBigCave();
    }
}
