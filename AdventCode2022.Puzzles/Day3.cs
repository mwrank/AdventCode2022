using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventCode2022.Puzzles
{
    public static class Day3
    {
        /// <summary>
        /// Build list of Runesacks from file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>List of Runesacks</returns>
        private static List<RuneSack> ReadRuneSacksFromFile(string filePath)
        {
            List<RuneSack> sacks = new List<RuneSack>();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                char[] items = line.ToCharArray();
                Compartment first = new Compartment();
                Compartment second = new Compartment();

                for (int i = 0; i < (items.Length / 2); i++)
                {
                    first.Items.Add(new Item(items[i]));
                }

                for (int k = (items.Length / 2); k < items.Length; k++)
                {
                    second.Items.Add(new Item(items[k]));
                }

                RuneSack sack = new RuneSack();
                sack.Compartments.Add(first);
                sack.Compartments.Add(second);
                sacks.Add(sack);
            }

            return sacks;
        }

        /// <summary>
        /// Get sum of shared priority items
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Sum of common items priority value</returns>
        public static int Part1(string filePath)
        {
            List<RuneSack> sacks = ReadRuneSacksFromFile(filePath);
            int result = 0;

            foreach(RuneSack s in sacks)
            {
                result += s.Compartments[0].Items.Where(x => s.Compartments[1].Items.Select(i => i.Text).Contains(x.Text)).First().Value;
            }

            return result;
        }

        /// <summary>
        /// Get sum of badge priority values
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Sum of badge priority values</returns>
        public static int Part2(string filePath)
        {
            List<RuneSack> sacks = ReadRuneSacksFromFile(filePath);
            List<Item> badges = new List<Item>();
            int result = 0;

            for(int i = 0; i < sacks.Count; i+=3)
            {
                List<Item> one = sacks[i].AllItems();
                List<Item> two = sacks[i+1].AllItems();
                List<Item> three = sacks[i+2].AllItems();

                Item common = one.Where(
                    x => two.Select(y => y.Text).Contains(x.Text) &&
                    three.Select(z => z.Text).Contains(x.Text)).First();

                result += common.Value;
            }

            return result;
        }
    }

    internal class Item
    {
        public char Text { get; set; }
        public int Value { get; set; }

        public Item() { }

        public Item(char text)
        {
            Text = text;

            // we can figure this out from the ascii value
            Value = Char.IsUpper(text) ? (((int)text - 64) + 26) : (int)Char.ToUpper(text) - 64;
        }
    }

    internal class Compartment
    {
        public List<Item> Items { get; set; }

        public Compartment()
        {
            Items = new List<Item>();
        }
    }

    internal class RuneSack
    {
        public List<Compartment> Compartments { get; set; }

        public RuneSack()
        {
            Compartments = new List<Compartment>();
        }

        /// <summary>
        /// Get all items in compartments
        /// </summary>
        /// <returns>List of all items in compartments</returns>
        public List<Item> AllItems()
        {
            List<Item> items = new List<Item>();

            foreach(Compartment c in Compartments)
            {
                items.AddRange(c.Items);
            }

            return items;
        }
    }
}
