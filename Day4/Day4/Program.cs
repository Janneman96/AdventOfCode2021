using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    class Program
    {
        static IList<int> balls;
        static ICollection<Card> cards = new List<Card>();

        static void Main(string[] args)
        {
            string[] inputLines = System.IO.File.ReadAllLines(@"C:\git\adventofcode2021Day4\Day4\input.txt");

            AddBalls(inputLines.First());
            AddCards(inputLines.Skip(1));

            int currentBallValue = 0;

            for (int i = 0; i < balls.Count() && !cards.Any(card => card.Bingo); i++)
            {
                currentBallValue = balls[i];
                foreach (var card in cards)
                {
                    card.AnnounceBall(currentBallValue);
                }
            }

            Console.WriteLine($"part 1: {cards.Single(card => card.Bingo).sumOfUnmarkedBoxValues * currentBallValue}");

            // part 2
            Card losingCard = null;
            for (int i = 0; i < balls.Count() && cards.Any(card => !card.Bingo); i++)
            {
                currentBallValue = balls[i];
                foreach (var card in cards)
                {
                    card.AnnounceBall(currentBallValue);
                    if (!card.Bingo)
                    {
                        losingCard = card;
                    }
                }
            }
            Console.WriteLine($"part 2: {losingCard.sumOfUnmarkedBoxValues * currentBallValue}");

            Console.ReadKey();
        }

        static void AddBalls(string lines) => balls = lines.Split(',').Select(item => int.Parse(item)).ToList();

        static void AddCards(IEnumerable<string> lines)
        {
            Card currentCard = null;
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    currentCard = new Card();
                    cards.Add(currentCard);
                }
                else
                {
                    foreach (string linePart in line.Split(' '))
                    {
                        if (!string.IsNullOrEmpty(linePart))
                        {
                            var number = int.Parse(linePart);
                            currentCard.AddBox(number);
                        }
                    }
                }
            }
        }

        class Box
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public int Value { get; set; }
            public bool Marked { get; set; }
            public void AnnounceBall(int ballNumber)
            {
                if (ballNumber.Equals(Value))
                {
                    Marked = true;
                }
            }

        }

        class Card
        {
            private const int rowsize = 5;
            public ICollection<Box> Boxes { get; } = new List<Box>();
            public int sumOfUnmarkedBoxValues => Boxes.Sum(box => box.Marked ? 0 : box.Value);

            public bool Bingo =>
                Boxes.GroupBy(box => box.Column).Any(column => column.All(box => box.Marked))
                || Boxes.GroupBy(box => box.Row).Any(row => row.All(box => box.Marked));

            public void AddBox(int value)
            {
                Boxes.Add(new Box
                {
                    Row = Boxes.Count / rowsize,
                    Column = Boxes.Count % rowsize,
                    Value = value
                });
            }

            public void AnnounceBall(int ballNumber)
            {
                foreach (var box in Boxes)
                {
                    box.AnnounceBall(ballNumber);
                }
            }
        }
    }
}
