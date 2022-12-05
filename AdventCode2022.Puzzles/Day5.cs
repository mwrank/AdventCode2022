using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventCode2022.Puzzles
{
    public static class Day5
    {
        private static SupplyShip ReadStartingSupplyStacksFromFile(string filePath)
        {
            SupplyShip ship = new SupplyShip();
            string firstLine = System.IO.File.ReadLines(filePath).First();

            // each column is 4 chars
            for (int i = 0; i < firstLine.Length; i += 4)
            {
                ship.Stacks.Add(new SupplyStack());
            }

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }
                else
                {
                    int stackCounter = 0;
                    for (int i = 0; i < line.Length; i += 4)
                    {
                        int len = (i + 4 < line.Length) ? 4 : 3;
                        string val = line.Substring(i, len).Replace("[", "").Replace("]", "").Trim();

                        if (!string.IsNullOrEmpty(val))
                        {
                            if (!int.TryParse(val, out int temp))
                                ship.Stacks[stackCounter].Items.Push(val);
                        }

                        stackCounter++;
                    }
                }
            }

            foreach (SupplyStack stack in ship.Stacks)
            {
                stack.Reverse();
            }

            return ship;
        }

        private static List<CraneInstructions> ReadCraneInstructionsFromFile(string filePath)
        {
            List<CraneInstructions> instructions = new List<CraneInstructions>();
            bool startReading = false;

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                if(string.IsNullOrEmpty(line))
                {
                    startReading = true;
                    continue;
                }

                if(startReading)
                {
                    CraneInstructions instruction = new CraneInstructions();
                    instruction.BuildFromText(line);
                    instructions.Add(instruction);
                }
            }

            return instructions;
        }

        public static string Part1(string filePath)
        {
            SupplyShip ship = ReadStartingSupplyStacksFromFile(filePath);
            List<CraneInstructions> instructions = ReadCraneInstructionsFromFile(filePath);
            string result = string.Empty;

            foreach(CraneInstructions instruction in instructions)
            {
                SupplyStack startingStack = ship.Stacks[instruction.FromStack-1];
                SupplyStack endingStack = ship.Stacks[instruction.ToStack-1];

                for(int i = 0; i < instruction.Amount; i++)
                {
                    string val = startingStack.Items.Pop();
                    endingStack.Items.Push(val);
                }
            }

            foreach(SupplyStack stack in ship.Stacks)
            {
                result += stack.Items.Peek();
            }

            return result;
        }

        public static string Part2(string filePath)
        {
            SupplyShip ship = ReadStartingSupplyStacksFromFile(filePath);
            List<CraneInstructions> instructions = ReadCraneInstructionsFromFile(filePath);
            string result = string.Empty;

            foreach (CraneInstructions instruction in instructions)
            {
                SupplyStack startingStack = ship.Stacks[instruction.FromStack - 1];
                SupplyStack endingStack = ship.Stacks[instruction.ToStack - 1];

                List<string> crates = new List<string>();

                for (int i = 0; i < instruction.Amount; i++)
                {
                    string val = startingStack.Items.Pop();
                    crates.Add(val);
                }

                crates.Reverse();

                foreach(string crate in crates)
                {
                    endingStack.Items.Push(crate);
                }
            }

            foreach (SupplyStack stack in ship.Stacks)
            {
                result += stack.Items.Peek();
            }

            return result;
        }
    }

    internal class CraneInstructions
    {
        public int Amount { get; set; }
        public int FromStack { get; set; }
        public int ToStack { get; set; }

        public CraneInstructions() { }

        public void BuildFromText(string text)
        {
            string[] parts = text.Split(" ");
            Amount = int.Parse(parts[1]);
            FromStack = int.Parse(parts[3]);
            ToStack = int.Parse(parts[5]);
        }
    }

    internal class SupplyShip
    {
        public List<SupplyStack> Stacks { get; set; }

        public SupplyShip()
        {
            Stacks = new List<SupplyStack>();
        }
    }

    internal class SupplyStack
    {
        public Stack<string> Items { get; set; }

        public SupplyStack()
        {
            Items = new Stack<string>();
        }

        public void Reverse()
        {
            Stack<string> s = new Stack<string>();

            while(Items.Count() != 0)
            {
                string val = Items.Pop();
                s.Push(val);
            }

            Items = s;
        }
    }
}
