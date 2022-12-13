using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventCode2022.Puzzles
{
    public static class Day11
    {
        public static BigInteger Part1(string filePath)
        {
            MonkeyInTheMiddle game = new MonkeyInTheMiddle();
            List<string> lines = System.IO.File.ReadLines(filePath).ToList();
            game.BuildFromLines(lines);

            for(int round = 0; round < 20; round++)
            {
                foreach(Monkey m in game.Monkeys)
                {
                    game.ProcessItems(m.Id, 3);
                }
            }

            return game.MonkeyBusinessScore();
        }

        public static BigInteger Part2(string filePath)
        {
            MonkeyInTheMiddle game = new MonkeyInTheMiddle();
            List<string> lines = System.IO.File.ReadLines(filePath).ToList();
            game.BuildFromLines(lines);

            for (int round = 0; round < 10000; round++)
            {
                foreach (Monkey m in game.Monkeys)
                {
                    game.ProcessItems(m.Id, 1);
                }
            }

            return game.MonkeyBusinessScore();
        }
    }

    internal class MonkeyInTheMiddle
    {
        public List<Monkey> Monkeys { get; set; }
        private BigInteger LCM { get; set; }

        public MonkeyInTheMiddle()
        {
            Monkeys = new List<Monkey>();
        }

        public void BuildFromLines(List<string> lines)
        {
            lines.Add(string.Empty);

            for (int i = 0; i < lines.Count(); i += 7)
            {
                int monkeyId = int.Parse(lines[i].Split(" ")[1].Replace(":", ""));
                string[] items = lines[i + 1].Replace("Starting items: ", "").Trim().Split(",");
                string operation = lines[i + 2].Replace("Operation: new = ", "").Trim();
                int divisibleTest = int.Parse(lines[i + 3].Split(" ")[5]);
                int trueMonkey = int.Parse(lines[i + 4].Split(" ")[9]);
                int falseMonkey = int.Parse(lines[i + 5].Split(" ")[9]);

                Monkey monkey = new Monkey(monkeyId, operation, divisibleTest, trueMonkey, falseMonkey);

                foreach (string item in items)
                {
                    monkey.Items.Enqueue(int.Parse(item));
                }

                Monkeys.Add(monkey);
            }
        }

        public BigInteger MonkeyBusinessScore()
        {
            List<Monkey> selected = Monkeys.OrderByDescending(x => x.InspectionCount).Take(2).ToList();
            return BigInteger.Multiply(selected.First().InspectionCount, selected.Last().InspectionCount);
        }

        private void Throw(Monkey from, Monkey to, BigInteger newWorryScore)
        {
            from.Items.Dequeue();
            to.Items.Enqueue(newWorryScore);
        }

        public void ProcessItems(int index, int worryDivider)
        {
            if(LCM == 0)
            {
                LCM = CalcLCM(Monkeys.Select(x => x.DivisibleTest).ToArray());
            }

            Monkey monkey = Monkeys.ElementAt(index);

            while (monkey.Items.Count() > 0)
            {
                BigInteger operationResult = monkey.Inspect();
                operationResult =(operationResult / worryDivider) % LCM;

                bool testResult = monkey.Test(operationResult);

                Monkey throwTo = testResult ?
                    Monkeys.ElementAt(monkey.IfTrueMonkeyId) :
                    Monkeys.ElementAt(monkey.IfFalseMonkeyId);

                Throw(monkey, throwTo, operationResult);
            }
        }

        private int CalcGCD(int n1, int n2)
        {
            return n2 == 0 ? n1 : CalcGCD(n2, n1 % n2);
        }

        private int CalcLCM(int[] numbers)
        {
            return numbers.Aggregate((s, val) => s * val);
        }
    }

    internal class Monkey
    {
        public int Id { get; set; }
        public string OperationText { get; set; }
        public int DivisibleTest { get; set; }
        public int IfTrueMonkeyId { get; set; }
        public int IfFalseMonkeyId { get; set; }
        public int InspectionCount { get; set; }
        public Queue<BigInteger> Items { get; set; }

        public Monkey(int id, string operation, int divisibleTest, int trueMonkey, int falseMonkey)
        {
            Id = id;
            OperationText = operation;
            DivisibleTest = divisibleTest;
            IfTrueMonkeyId = trueMonkey;
            IfFalseMonkeyId = falseMonkey;
            InspectionCount = 0;
            Items = new Queue<BigInteger>();
        }


        public BigInteger Inspect()
        {
            string operation = OperationText.Replace("old", Items.Peek().ToString());
            string[] operationParts = operation.Split(" ");
            InspectionCount++;

            if(operationParts[1] == "*")
            {
                return BigInteger.Multiply(
                    BigInteger.Parse(operationParts[0]),
                    BigInteger.Parse(operationParts[2]));
            }
            else
            {
                return BigInteger.Add(
                    BigInteger.Parse(operationParts[0]),
                    BigInteger.Parse(operationParts[2]));
            }
        }

        public bool Test(BigInteger num)
        {
            return (num % DivisibleTest) == 0;
        }
    }
}
