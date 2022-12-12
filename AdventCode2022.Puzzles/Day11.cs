using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AdventCode2022.Puzzles
{
    public static class Day11
    {
        public static int Part1(string filePath)
        {
            int linesPerMonkey = 7;
            MonkeyInTheMiddle game = new MonkeyInTheMiddle();
            List<string> lines = System.IO.File.ReadLines(filePath).ToList();
            lines.Add(string.Empty);

            for(int i = 0; i < lines.Count(); i += linesPerMonkey)
            {
                int monkeyId = int.Parse(lines[i].Split(" ")[1].Replace(":", ""));
                string[] items = lines[i + 1].Replace("Starting items: ", "").Trim().Split(",");
                string operation = lines[i + 2].Replace("Operation: new = ", "").Trim();
                int modulus = int.Parse(lines[i + 3].Split(" ")[5]);
                int trueMonkey = int.Parse(lines[i + 4].Split(" ")[9]);
                int falseMonkey = int.Parse(lines[i + 5].Split(" ")[9]);

                Monkey monkey = new Monkey(monkeyId, operation, modulus, trueMonkey, falseMonkey);

                foreach (string item in items)
                {
                    monkey.Items.Enqueue(int.Parse(item));
                }

                game.Monkeys.Add(monkey);
            }

            for(int round = 0; round < 20; round++)
            {
                foreach(Monkey m in game.Monkeys)
                {
                    game.ProcessItems(m.Id);
                }
            }

            return game.MonkeyBusinessScore();
        }
    }

    internal class MonkeyInTheMiddle
    {
        public List<Monkey> Monkeys { get; set; }

        public MonkeyInTheMiddle()
        {
            Monkeys = new List<Monkey>();
        }

        public int MonkeyBusinessScore()
        {
            List<Monkey> selected = Monkeys.OrderByDescending(x => x.Inspections).Take(2).ToList();
            return selected.First().Inspections * selected.Last().Inspections;
        }

        private void Throw(Monkey from, Monkey to, int newWorryScore)
        {
            from.Items.Dequeue();
            to.Items.Enqueue(newWorryScore);
        }

        public void ProcessItems(int index)
        {
            Monkey monkey = Monkeys.ElementAt(index);

            while (monkey.Items.Count() > 0)
            {
                int operaationResult = monkey.Inspect() / 3;
                bool testResult = monkey.Test(operaationResult);

                Monkey throwTo = testResult ?
                    Monkeys.ElementAt(monkey.TrueMonkeyId) :
                    Monkeys.ElementAt(monkey.FalseMonkeyId);

                Throw(monkey, throwTo, operaationResult);
            }
        }
    }

    internal class Monkey
    {
        public int Id { get; set; }
        public string OperationText { get; set; }
        public int ModulusTest { get; set; }
        public int TrueMonkeyId { get; set; }
        public int FalseMonkeyId { get; set; }
        public int Inspections { get; set; }
        public Queue<int> Items { get; set; }

        public Monkey(int id, string operation, int modulus, int trueMonkey, int falseMonkey)
        {
            Id = id;
            OperationText = operation;
            ModulusTest = modulus;
            TrueMonkeyId = trueMonkey;
            FalseMonkeyId = falseMonkey;
            Inspections = 0;
            Items = new Queue<int>();
        }


        public int Inspect()
        {
            string operation = OperationText.Replace("old", Items.Peek().ToString());
            Inspections++;

            DataTable dt = new DataTable();
            return int.Parse(dt.Compute(operation, "").ToString());
        }

        public bool Test(int num)
        {
            return (num % ModulusTest) == 0;
        }
    }
}
