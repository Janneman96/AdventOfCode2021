using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main()
        {
            var inputLines = System.IO.File.ReadAllLines(@"C:\git\adventofcode2021Day10\Day10\input.txt");

            var chunkTypes = new List<ChunkType>();
            chunkTypes.Add(new ChunkType { OpeningCharacter = '(', ClosingCharacter = ')', UnclosedScore = 1 });
            chunkTypes.Add(new ChunkType { OpeningCharacter = '[', ClosingCharacter = ']', UnclosedScore = 2 });
            chunkTypes.Add(new ChunkType { OpeningCharacter = '{', ClosingCharacter = '}', UnclosedScore = 3 });
            chunkTypes.Add(new ChunkType { OpeningCharacter = '<', ClosingCharacter = '>', UnclosedScore = 4 });

            var scores = new List<ulong>();

            foreach (var inputLine in inputLines)
            {
                var result = GetLineResult(inputLine, chunkTypes);
                if (!result.Corrupt)
                {
                    scores.Add(CalculateScore(result, chunkTypes));
                }
            }

            scores.Sort();

            Console.WriteLine(scores[scores.Count / 2]);
        }

        private static ulong CalculateScore(LineResult result, List<ChunkType> chunkTypes)
        {
            ulong score = 0;

            for (var i = result.UnclosedCharacters.Count - 1; i >= 0; i--)
            {
                var unclosedCharacter = result.UnclosedCharacters[i];
                score *= 5;
                score += chunkTypes.Single(chunkType => chunkType.OpeningCharacter == unclosedCharacter).UnclosedScore;
            }

            Console.WriteLine($"{string.Join("", result.UnclosedCharacters)} - {score} points.");

            return score;
        }

        static LineResult GetLineResult(string line, List<ChunkType> chunkTypes)
        {
            var unclosedCharacters = new List<char>();

            foreach (var character in line)
            {
                foreach (var chunkType in chunkTypes)
                {
                    if (character == chunkType.ClosingCharacter)
                    {
                        if (unclosedCharacters.Any() && unclosedCharacters.Last() == chunkType.OpeningCharacter)
                        {
                            unclosedCharacters.RemoveAt(unclosedCharacters.Count - 1);
                        }
                        else
                        {
                            var expected = chunkTypes.Single(c => c.OpeningCharacter == unclosedCharacters.Last()).ClosingCharacter;
                            return new LineResult
                            {
                                Corrupt = true
                            };
                        }
                    }
                    else if (character == chunkType.OpeningCharacter)
                    {
                        unclosedCharacters.Add(character);
                    }
                }
            }

            return new LineResult
            {
                Corrupt = false,
                UnclosedCharacters = unclosedCharacters
            };
        }
    }

    public class LineResult
    {
        public bool Corrupt { get; set; }
        public List<char> UnclosedCharacters { get; set; }
    }

    public class ChunkType
    {
        public char OpeningCharacter { get; set; }
        public char ClosingCharacter { get; set; }
        public ulong UnclosedScore { get; set; }
    }

    // part 1
    //    static void Main()
    //    {
    //        var inputLines = System.IO.File.ReadAllLines(@"C:\git\adventofcode2021Day10\Day10\input.txt");

    //        var chunkTypes = new List<ChunkType>();
    //        chunkTypes.Add(new ChunkType { OpeningCharacter = '(', ClosingCharacter = ')', ErrorScore = 3 });
    //        chunkTypes.Add(new ChunkType { OpeningCharacter = '[', ClosingCharacter = ']', ErrorScore = 57 });
    //        chunkTypes.Add(new ChunkType { OpeningCharacter = '{', ClosingCharacter = '}', ErrorScore = 1197 });
    //        chunkTypes.Add(new ChunkType { OpeningCharacter = '<', ClosingCharacter = '>', ErrorScore = 25137 });

    //        var score = 0;

    //        foreach (var inputLine in inputLines)
    //        {
    //            score += GetScore(inputLine, chunkTypes);
    //        }

    //        Console.WriteLine(score);
    //    }

    //    static int GetScore(string line, List<ChunkType> chunkTypes)
    //    {
    //        var characters = new List<char>();

    //        foreach (var character in line)
    //        {
    //            foreach (var chunkType in chunkTypes)
    //            {
    //                if (character == chunkType.ClosingCharacter)
    //                {
    //                    if (characters.Any() && characters.Last() == chunkType.OpeningCharacter)
    //                    {
    //                        characters.RemoveAt(characters.Count - 1);
    //                    }
    //                    else
    //                    {
    //                        var expected = chunkTypes.Single(c => c.OpeningCharacter == characters.Last()).ClosingCharacter;
    //                        Console.WriteLine($"{line} - Expected {expected}, but found {character} instead.");
    //                        return chunkType.ErrorScore;
    //                    }
    //                }
    //                else if (character == chunkType.OpeningCharacter)
    //                {
    //                    characters.Add(character);
    //                }
    //            }
    //        }

    //        return 0;
    //    }
    //}
}
