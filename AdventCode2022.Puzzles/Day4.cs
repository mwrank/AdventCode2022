using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventCode2022.Puzzles
{
    public class Day4
    {
        private static List<AssignedPair> ReadPairsFromFile(string filePath)
        {
            List<AssignedPair> pairs = new List<AssignedPair>();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                string[] parts = line.Split(",");
                AssignedPair pair = new AssignedPair();

                foreach (string part in parts)
                {
                    string[] sections = part.Split("-");
                    Elf2 elf = new Elf2(int.Parse(sections[0]), int.Parse(sections[1]));
                    pair.Elves.Add(elf);
                }

                pairs.Add(pair);
            }

            return pairs;
        }

        public static int Part1(string filePath)
        {
            List<AssignedPair> pairs = ReadPairsFromFile(filePath);
            return pairs.Where(x => x.HasFullOverlap()).Count();
        }

        public static int Part2(string filePath)
        {
            List<AssignedPair> pairs = ReadPairsFromFile(filePath);
            return pairs.Where(x => x.HasAnyOverlap()).Count();
        }
    }

    internal class Elf2
    {
        public int[] AssignedRange { get; set; }

        public Elf2(int start, int end)
        {
            AssignedRange = new int[] { start, end };
        }

        public List<int> GetAssignedSections()
        {
            int start = AssignedRange[0];
            int end = AssignedRange[1];
            List<int> sections = new List<int>();

            for(int i = start; i <=end; i++)
            {
                sections.Add(i);
            }

            return sections;
        }
    }

    internal class AssignedPair
    {
        public List<Elf2> Elves { get; set; }

        public AssignedPair()
        {
            Elves = new List<Elf2>();
        }

        public List<int> AllSections()
        {
            List<int> sections = Elves.ElementAt(0).GetAssignedSections();
            sections.AddRange(Elves.ElementAt(1).GetAssignedSections());
            return sections;
        }

        public bool HasFullOverlap()
        {
            List<int> elf1Sections = Elves.ElementAt(0).GetAssignedSections();
            List<int> elf2Sections = Elves.ElementAt(1).GetAssignedSections();

            if(elf1Sections.Count() > elf2Sections.Count())
            {
                return elf1Sections.Intersect(elf2Sections).Count() == elf2Sections.Count;
            }
            else
            {
                return elf2Sections.Intersect(elf1Sections).Count() == elf1Sections.Count;
            }
        }

        public bool HasAnyOverlap()
        {
            List<int> sections = AllSections();
            int distinct = sections.Distinct().Count();
            return sections.Count() != distinct;
        }
    }
}
