using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventCode2022.Puzzles
{
    public static class Day9
    {
        public static int Part1(string filePath)
        {
            KnotPoint head = new KnotPoint(0, 0);
            KnotPoint tail = new KnotPoint(0, 0);
            List<string> visitedPositions = new List<string>();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                string[] parts = line.Split(" ");
                string direction = parts[0];
                int count = int.Parse(parts[1]);

                for (int i = 0; i < count; i++)
                {
                    if (direction == "U")
                    {
                        head.Move(0, 1);
                        tail.Follow(head);
                    }

                    if (direction == "D")
                    {
                        head.Move(0, -1);
                        tail.Follow(head);
                    }

                    if (direction == "L")
                    {
                        head.Move(-1, 0);
                        tail.Follow(head);
                    }

                    if (direction == "R")
                    {
                        head.Move(1, 0);
                        tail.Follow(head);
                    }

                    visitedPositions.Add(string.Format("{0}-{1}", tail.X, tail.Y));
                }
            }

            return visitedPositions.Distinct().Count();
        }

        public static int Part2(string filePath)
        {
            KnotPoint head = new KnotPoint(0, 0); // 1
            KnotPoint two = new KnotPoint(0, 0);
            KnotPoint three = new KnotPoint(0, 0);
            KnotPoint four = new KnotPoint(0, 0);
            KnotPoint five = new KnotPoint(0, 0);
            KnotPoint six = new KnotPoint(0, 0);
            KnotPoint seven = new KnotPoint(0, 0);
            KnotPoint eight = new KnotPoint(0, 0);
            KnotPoint nine = new KnotPoint(0, 0);
            KnotPoint tail = new KnotPoint(0, 0); // 10
            List<string> visitedPositions = new List<string>();

            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                string[] parts = line.Split(" ");
                string direction = parts[0];
                int count = int.Parse(parts[1]);

                for (int i = 0; i < count; i++)
                {
                    if (direction == "U")
                    {
                        head.Move(0, 1);
                        two.Follow(head);
                        three.Follow(two);
                        four.Follow(three);
                        five.Follow(four);
                        six.Follow(five);
                        seven.Follow(six);
                        eight.Follow(seven);
                        nine.Follow(eight);
                        tail.Follow(nine);
                    }

                    if (direction == "D")
                    {
                        head.Move(0, -1);
                        two.Follow(head);
                        three.Follow(two);
                        four.Follow(three);
                        five.Follow(four);
                        six.Follow(five);
                        seven.Follow(six);
                        eight.Follow(seven);
                        nine.Follow(eight);
                        tail.Follow(nine);
                    }

                    if (direction == "L")
                    {
                        head.Move(-1, 0);
                        two.Follow(head);
                        three.Follow(two);
                        four.Follow(three);
                        five.Follow(four);
                        six.Follow(five);
                        seven.Follow(six);
                        eight.Follow(seven);
                        nine.Follow(eight);
                        tail.Follow(nine);
                    }

                    if (direction == "R")
                    {
                        head.Move(1, 0);
                        two.Follow(head);
                        three.Follow(two);
                        four.Follow(three);
                        five.Follow(four);
                        six.Follow(five);
                        seven.Follow(six);
                        eight.Follow(seven);
                        nine.Follow(eight);
                        tail.Follow(nine);
                    }

                    visitedPositions.Add(string.Format("{0}-{1}", tail.X, tail.Y));
                }
            }

            return visitedPositions.Distinct().Count();
        }
    }

    internal class KnotPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public KnotPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(int x, int y)
        {
            X += x;
            Y += y;
        }

        public void Follow(KnotPoint head)
        {
            if(IsFar(head))
            {
                int offX = Math.Sign(head.X - X);
                int offY = Math.Sign(head.Y - Y);

                X += offX;
                Y += offY;
            }
        }

        private bool IsFar(KnotPoint head)
        {
            if (Math.Abs(X - head.X) > 1 || Math.Abs(Y - head.Y) > 1)
                return true;

            return false;
        }
    }
}
