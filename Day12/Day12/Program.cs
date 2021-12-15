using System;
using System.Collections.Generic;
using System.Linq;

Pseudo from phone

GetPaths(){
  DiscoveredPaths = new List<Path>();

  Bool MorePathsAvailable = true;

  While (MorePathsAvailable) {
    Var nextPath = getNextPath(discoveredpaths)
    MorePathsAvailable = nextpath == null
    if (MorePathsAvailable) DiscoveredPaths.add(nextpath)
  }

  Return discoveredpaths.where( last == end )
}

Getnextpath(discoveredpaths) {
  // First time
  Var newPath = new List<Path>()
  En
  Var node = startNode

  While(node.availableConnections.Any()) {
    New path.add Node.availableConnections(0);
    node = node.availableConnections(0)
  }

  // OptimalisationIdea; trace back until another option is available; pick that option
}



namespace Day12
{
    class Program
    {
        static void Main()
        {
            //var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021\Day12\Day12\input.txt");
            var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021\Day12\Day12\input10.txt");
            var pathing = new Pathing();
            var paths = pathing.GetPaths(inputLines);
            Console.WriteLine(string.Join('\n', paths));
            Console.WriteLine($"Paths: {paths.Count()}");
        }
    }

    public class Pathing
    {
        private ICollection<Cave> Caves { get; } = new List<Cave>();

        public IEnumerable<string> GetPaths(string[] connections)
        {
            AddCaves(connections);

            var startCave = Caves.Single(cave => cave.Name == "start");
            var paths = new List<List<Cave>>();
            paths.Add(new List<Cave>() { startCave });

            paths = CalculatePaths(startCave, paths);


            return paths.Select(path => string.Join(',', path.Select(cave => cave.Name)));
        }

        private static List<List<Cave>> CalculatePaths(Cave startCave, List<List<Cave>> paths)
        {
            var newPaths = new List<List<Cave>>();
            foreach (var cave in startCave.ConnectedTo)
            {
                if (!paths.Any(path => path.Any(c => c.Name == cave.Name)))
                {
                    var newPath = $"{startCave.Name},{cave.Name}";
                    //todo change from string[] to cave[][]
                    newPaths.Add(newPath);
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
                if (leftCave.IsBigCave() || rightCave.IsBigCave())
                {
                    rightCave.ConnectedTo.Add(leftCave);
                }
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
