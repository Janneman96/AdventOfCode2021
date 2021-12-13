//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Day12
//{
//    class Program
//    {
//        static void Main()
//        {
//            //var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021\Day12\Day12\input.txt");
//            var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021\Day12\Day12\input10.txt");
//            var pathing = new Pathing();
//            Console.WriteLine($"Paths: {pathing.GetPaths(inputLines).Count()}");
//        }
//    }

//    public class Pathing
//    {
//        private ICollection<Cave> Caves { get; } = new List<Cave>();

//        public IEnumerable<string> GetPaths(string[] connections)
//        {
//            AddCaves(connections);

//            var paths = Caves.Single(cave => cave.Name == "start").GetPathsToEnd();

//            Console.WriteLine(string.Join('\n', paths));
//            return paths;
//        }

//        private void AddCaves(string[] connections)
//        {
//            foreach (var connection in connections)
//            {
//                var caveNames = connection.Split('-');
//                var leftName = caveNames[0];
//                var rightName = caveNames[1];

//                var leftCave = GetCave(leftName);
//                var rightCave = GetCave(rightName);

//                leftCave.ConnectedTo.Add(rightCave);
//                if (leftCave.IsBigCave() || rightCave.IsBigCave())
//                {
//                    rightCave.ConnectedTo.Add(leftCave);
//                }
//            }
//        }

//        private Cave GetCave(string name)
//        {
//            var cave = Caves.SingleOrDefault(cave => cave.Name == name);
//            if (cave == null)
//            {
//                cave = new Cave(name);
//                Caves.Add(cave);
//            }
//            return cave;
//        }
//    }

//    public class Cave
//    {
//        public Cave(string name)
//        {
//            Name = name;
//        }

//        public string Name { get; }
//        public ICollection<Cave> ConnectedTo { get; } = new List<Cave>();

//        public bool IsBigCave() => char.IsUpper(Name[0]);
//        public bool IsSmallCave() => !IsBigCave();

//        public IEnumerable<string> GetPossiblePathsAsStrings()
//        {
//            return GetPathsToEnd()
//                .Select(cave => string.Join(',', cave));
//        }

//        public IEnumerable<string> GetPathsToEnd()
//        {
//            var paths = new List<List<Cave>>();
//            AddPaths(paths);
//            var pathsToEndAsString = new List<string>();
//            foreach (var path in paths)
//            {
//                if (path.Last().Name == "end")
//                {
//                    pathsToEndAsString.Add(string.Join(',', path.Select(cave => cave.Name)));
//                }
//            }
//            return pathsToEndAsString;
//        }

//        public void AddPaths(List<List<Cave>> pathsToThisCave)
//        {
//            AddStart(pathsToThisCave);
            
//            Explore(pathsToThisCave);
//        }

//        private List<List<Cave>> Explore(List<List<Cave>> pathsToThisCave)
//        {
//            var pathsToNextCave = new List<List<Cave>>();

//            foreach (var path in pathsToThisCave)
//            {
//                path.Add(this);
//                pathsToNextCave.Add(path);

//                foreach (var cave in ConnectedTo)
//                {
//                    bool caveIsWorthExploring = !pathsToThisCave.Any(path => path.Any(c => c.IsSmallCave() && c.Name == cave.Name));
//                    if (caveIsWorthExploring)
//                    {
//                        pathsToNextCave = cave.Explore(pathsToNextCave);
//                    }
//                }
//            }

//            return pathsToNextCave;
//        }

//        private void AddStart(List<List<Cave>> currentPaths)
//        {
//            if (!currentPaths.Any())
//            {
//                currentPaths.Add(new List<Cave> { this });
//            }
//        }
//    }
//}
///*
//input: 
//start
//output:
//start,A
//start,b

//input:
//start,b

//output:
//start,b,A
//start,b,d
//start,b,end




//*/