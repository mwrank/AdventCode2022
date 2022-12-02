using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode2022.Library
{
    public static class Day1
    {
        /// <summary>
        /// Advent code day 1 part 1
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>Sum of calories for the elf carrying the most calories</returns>
        public static int Part1(string filePath)
        {
            List<Elf> elves  = LoadElvesFromFile(filePath);
            return elves.OrderByDescending(x => x.TotalCalories).First().TotalCalories;
        }

        /// <summary>
        /// Advent code day 1 part 2
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>Sum of calories for the top three elves carrying the most calories</returns>
        public static int Part2(string filePath)
        {
            List<Elf> elves = LoadElvesFromFile(filePath);
            return elves.OrderByDescending(x => x.TotalCalories).Take(3).Sum(x => x.TotalCalories);
        }

        /// <summary>
        /// Load elves from text file
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>List of elves</returns>
        private static List<Elf> LoadElvesFromFile(string filePath)
        {
            List<Elf> elves = new List<Elf>();
            Elf elf = new Elf();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    int snack = int.Parse(line);
                    elf.Snacks.Add(snack);
                }
                else
                {
                    elves.Add(elf);
                    elf = new Elf();
                }
            }

            elves.Add(elf);
            return elves;
        }
    }

    internal class Elf
    {
        public List<int> Snacks { get; set; }

        public int TotalCalories
        {
            get { return Snacks.Sum(); }
        }

        public Elf()
        {
            Snacks = new List<int>();
        }
    }
}
