using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCode2022.Puzzles
{
    public static class Day10
    {
        public static int Part1(string filePath)
        {
            CathodeRayTube crt = new CathodeRayTube();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                string[] parts = line.Split(" ");
                string function = parts[0];

                if (function == "noop")
                    crt.Noop();

                if (function == "addx")
                {
                    int val = int.Parse(parts[1]);
                    crt.AddX(val);
                }
            }

            int twenty = crt.GetValueAtCycle(20);
            int sixty = crt.GetValueAtCycle(60);
            int oneHundred = crt.GetValueAtCycle(100);
            int oneHundredForty = crt.GetValueAtCycle(140);
            int oneHundredEighty = crt.GetValueAtCycle(180);
            int twoHundredTwenty = crt.GetValueAtCycle(220);

            return twenty + sixty + oneHundred + oneHundredForty +
                oneHundredEighty + twoHundredTwenty;
        }
    }

    internal class CathodeRayTube
    {
        private Dictionary<int, int> Cycles { get; set; }

        public CathodeRayTube()
        {
            Cycles = new Dictionary<int, int>();
            Cycles.Add(1, 1);
        }

        public int CycleCount
        {
            get { return Cycles.Last().Key; }
        }

        public int CurrentValue
        {
            get { return Cycles.Last().Value; }
        }

        public int GetValueAtCycle(int cycle)
        {
            if (Cycles.ContainsKey(cycle))
                return cycle * Cycles[cycle];

            return 0;
        }

        public void Noop()
        {
            Cycle(0);
        }

        public void AddX(int val)
        {
            Cycle(0);
            Cycle(val);
        }

        private void Cycle(int val)
        {
            int count = CycleCount + 1;
            int newVal = CurrentValue + val;

            Cycles.Add(count, newVal);
        }
    }
}
