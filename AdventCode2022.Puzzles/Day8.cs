using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventCode2022.Puzzles
{
    public static class Day8
    {
        public static int Part1(string filePath)
        {
            Forest forest = new Forest();
            forest.BuildFromMap(filePath);

            int totalVisible = 0;

            for (int row = 0; row < forest.Trees.GetLength(0); row++)
            {
                for (int col = 0; col < forest.Trees.GetLength(1); col++)
                {
                    if (forest.IsVisible(row, col))
                        totalVisible++;
                }
            }

            return totalVisible;
        }

        public static int Part2(string filePath)
        {
            Forest forest = new Forest();
            forest.BuildFromMap(filePath);

            int largestScenicScore = 0;

            for (int row = 0; row < forest.Trees.GetLength(0); row++)
            {
                for (int col = 0; col < forest.Trees.GetLength(1); col++)
                {
                    int score = forest.ScenicScore(row, col);

                    if (score > largestScenicScore)
                        largestScenicScore = score;
                }
            }

            return largestScenicScore;
        }
    }

    internal class Tree
    {
        public int Height { get; set; }
    }

    internal class Forest
    {
        public Tree[,] Trees { get; set; }

        public Forest() { }

        public void BuildFromMap(string filePath)
        {
            IEnumerable<string> lines = System.IO.File.ReadLines(filePath);
            Trees = new Tree[lines.First().Length, lines.Count()];

            for (int row = 0; row < lines.First().Length; row++)
            {
                char[] columns = lines.ElementAt(row).ToCharArray();

                for (int col = 0; col < columns.Count(); col++)
                {
                    Trees[row, col] = new Tree() { Height = int.Parse(columns[col].ToString()) };
                }
            }
        }

        public bool IsVisible(int row, int col)
        {
            return IsVisibleFromEast(row, col) || IsVisibleFromWest(row, col) ||
                IsVisibleFromSouth(row, col) || IsVisibleFromNorth(row, col);
        }

        public bool IsVisibleFromEast(int row, int col)
        {
            int height = Trees[row, col].Height;
            int size = Trees.GetLength(1);

            for(int i = col + 1; i < size; i++)
            {
                Tree otherTree = Trees[row, i];

                if(otherTree.Height >= height)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsVisibleFromWest(int row, int col)
        {
            int height = Trees[row, col].Height;

            for (int i = col - 1; i >= 0; i--)
            {
                Tree otherTree = Trees[row, i];

                if (otherTree.Height >= height)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsVisibleFromNorth(int row, int col)
        {
            int height = Trees[row, col].Height;

            for (int i = row - 1; i >= 0; i--)
            {
                Tree otherTree = Trees[i, col];

                if (otherTree.Height >= height)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsVisibleFromSouth(int row, int col)
        {
            int height = Trees[row, col].Height;
            int size = Trees.GetLength(0);

            for (int i = row + 1; i < size; i++)
            {
                Tree otherTree = Trees[i, col];

                if (otherTree.Height >= height)
                {
                    return false;
                }
            }

            return true;
        }

        public int ScenicScore(int row, int col)
        {
            return TreesSeenEast(row, col) * TreesSeenWest(row, col) *
                TreeSeenSouth(row, col) * TreesSeenNorth(row, col);
        }

        public int TreesSeenEast(int row, int col)
        {
            int height = Trees[row, col].Height;
            int size = Trees.GetLength(1);
            int numberSeen = 0;

            for (int i = col + 1; i < size; i++)
            {
                Tree otherTree = Trees[row, i];
                numberSeen += 1;

                if (otherTree.Height >= height)
                    break;
            }

            return numberSeen;
        }

        public int TreesSeenWest(int row, int col)
        {
            int height = Trees[row, col].Height;
            int numberSeen = 0;

            for (int i = col - 1; i >= 0; i--)
            {
                Tree otherTree = Trees[row, i];
                numberSeen += 1;

                if (otherTree.Height >= height)
                    break;
            }

            return numberSeen;
        }

        public int TreesSeenNorth(int row, int col)
        {
            int height = Trees[row, col].Height;
            int numberSeen = 0;

            for (int i = row - 1; i >= 0; i--)
            {
                Tree otherTree = Trees[i, col];
                numberSeen += 1;

                if (otherTree.Height >= height)
                    break;
            }

            return numberSeen;
        }

        public int TreeSeenSouth(int row, int col)
        {
            int height = Trees[row, col].Height;
            int size = Trees.GetLength(0);
            int numberSeen = 0;

            for (int i = row + 1; i < size; i++)
            {
                Tree otherTree = Trees[i, col];
                numberSeen += 1;

                if (otherTree.Height >= height)
                    break;
            }

            return numberSeen;
        }
    }
}
