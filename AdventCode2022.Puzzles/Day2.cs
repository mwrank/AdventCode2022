using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventCode2022.Puzzles
{
    public static class Day2
    {
        /// <summary>
        /// Advent code day 2 part 1
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>Sum score based on strategy guide</returns>
        public static int Part1(string filePath)
        {
            StrategyGuide strategyGuide = new StrategyGuide();
            strategyGuide.BuildFromFile(filePath);
            return strategyGuide.Hands.Sum(x => x.GetScore());
        }

        /// <summary>
        /// Advent code day 2 part 2
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>Sum score based on strategy guide</returns>
        public static int Part2(string filePath)
        {
            StrategyGuide strategyGuide = new StrategyGuide();
            strategyGuide.BuildFromFile(filePath, true);
            return strategyGuide.Hands.Sum(x => x.GetScore());
        }
    }

    internal enum Option
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3,
    }

    internal enum Outcome
    {
        Lost = 0,
        Draw = 3,
        Win = 6
    }

    internal class Hand
    {
        public Outcome Outcome { get; set; }
        public Option Opponent { get; set; }
        public Option Me { get; set; }

        public Hand(Option opponent, Outcome desiredOutcome)
        {
            Opponent = opponent;
            Outcome = desiredOutcome;

            if (desiredOutcome == Outcome.Draw)
                Me = opponent;

            if(desiredOutcome == Outcome.Win)
            {
                if (opponent == Option.Rock)
                    Me = Option.Paper;

                if (opponent == Option.Paper)
                    Me = Option.Scissors;

                if (opponent == Option.Scissors)
                    Me = Option.Rock;
            }

            if (desiredOutcome == Outcome.Lost)
            {
                if (opponent == Option.Rock)
                    Me = Option.Scissors;

                if (opponent == Option.Paper)
                    Me = Option.Rock;

                if (opponent == Option.Scissors)
                    Me = Option.Paper;
            }
        }

        public Hand(Option opponent, Option me)
        {
            Opponent = opponent;
            Me = me;

            if(opponent == me)
            {
                Outcome = Outcome.Draw;
            }
            else
            {
                if (opponent == Option.Rock)
                    Outcome = me == Option.Paper ? Outcome.Win : Outcome.Lost;

                if (opponent == Option.Paper)
                    Outcome = me == Option.Scissors ? Outcome.Win : Outcome.Lost;

                if (opponent == Option.Scissors)
                    Outcome = me == Option.Rock ? Outcome.Win : Outcome.Lost;
            }
        }

        /// <summary>
        /// Get score
        /// </summary>
        /// <returns>Sum of option and outcome values</returns>
        public int GetScore()
        {
            int optionVal = (int)Me;
            int outcomesVal = (int)Outcome;

            return (optionVal + outcomesVal);
        }
    }

    internal class StrategyGuide
    {
        public List<Hand> Hands { get; set; }

        public StrategyGuide()
        {
            Hands = new List<Hand>();
        }

        /// <summary>
        /// Decrypts hand
        /// </summary>
        /// <param name="letter">Letter to decrypt</param>
        /// <returns>Option</returns>
        public Option DecryptOption(string letter)
        {
            if (letter == "A" || letter == "X")
                return Option.Rock;

            else if (letter == "B" || letter == "Y")
                return Option.Paper;

            else
                return Option.Scissors;
        }

        /// <summary>
        /// Decrypts outcome
        /// </summary>
        /// <param name="letter">Letter to decrypt</param>
        /// <returns>Outcome</returns>
        public Outcome DecryptOutcome(string letter)
        {
            if (letter == "X")
                return Outcome.Lost;

            else if (letter == "Y")
                return Outcome.Draw;

            else
                return Outcome.Win;
        }

        /// <summary>
        /// Build strategy guide from file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <param name="isOutcomeSecondColumn">Is second column outcome or hand</param>
        public void BuildFromFile(string filePath, bool isOutcomeSecondColumn = false)
        {
            if (isOutcomeSecondColumn)
            {
                foreach (string line in System.IO.File.ReadLines(filePath))
                {
                    string[] parts = line.Split(" ");
                    Option opponent = DecryptOption(parts[0]);
                    Outcome desiredOutcome = DecryptOutcome(parts[1]);
                    Hands.Add(new Hand(opponent, desiredOutcome));
                }
            }
            else
            {
                foreach (string line in System.IO.File.ReadLines(filePath))
                {
                    string[] parts = line.Split(" ");
                    Option opponent = DecryptOption(parts[0]);
                    Option me = DecryptOption(parts[1]);
                    Hands.Add(new Hand(opponent, me));
                }
            }
        }
    }
}
