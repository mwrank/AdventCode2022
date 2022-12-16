using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventCode2022.Puzzles
{
    public static class Day8
    {
        /// <summary>
        /// Advent code day 8 part 1
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Number of trees are visible from outside the grid</returns>
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

        /// <summary>
        /// Advent code day 8 part 2
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Highest scenic score</returns>
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

        /// <summary>
        /// Reads file into 2D array of trees
        /// </summary>
        /// <param name="filePath">Path to file</param>
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

        /// <summary>
        /// Is visible from any direction
        /// </summary>
        /// <param name="row">Row in grid</param>
        /// <param name="col">Col in grid</param>
        /// <returns></returns>
        public bool IsVisible(int row, int col)
        {
            return IsVisibleFromEast(row, col) || IsVisibleFromWest(row, col) ||
                IsVisibleFromSouth(row, col) || IsVisibleFromNorth(row, col);
        }

        /// <summary>
        /// Is visible from the east
        /// </summary>
        /// <param name="row">Row in grid</param>
        /// <param name="col">Col in grid</param>
        /// <returns></returns>
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

        /// <summary>
        /// Is visible from the west
        /// </summary>
        /// <param name="row">Row in grid</param>
        /// <param name="col">Col in grid</param>
        /// <returns></returns>
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

        /// <summary>
        /// Is visible from the north
        /// </summary>
        /// <param name="row">Row in grid</param>
        /// <param name="col">Col in grid</param>
        /// <returns></returns>
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

        /// <summary>
        /// Is visible from the south
        /// </summary>
        /// <param name="row">Row in grid</param>
        /// <param name="col">Col in grid</param>
        /// <returns></returns>
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

        /// <summary>
        /// Scenic score
        /// </summary>
        /// <param name="row">Row in grid</param>
        /// <param name="col">Col in grid</param>
        /// <returns>Number of trees visible * each direction to calc score</returns>
        public int ScenicScore(int row, int col)
        {
            return TreesSeenEast(row, col) * TreesSeenWest(row, col) *
                TreeSeenSouth(row, col) * TreesSeenNorth(row, col);
        }


        /// <summary>
        /// Number of trees seen to the east
        /// </summary>
        /// <param name="row">Row in grid</param>
        /// <param name="col">Col in grid</param>
        /// <returns></returns>
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

        /// <summary>
        /// Number of trees seen to the west
        /// </summary>
        /// <param name="row">Row in grid</param>
        /// <param name="col">Col in grid</param>
        /// <returns></returns>
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

        /// <summary>
        /// Number of trees seen to the north
        /// </summary>
        /// <param name="row">Row in grid</param>
        /// <param name="col">Col in grid</param>
        /// <returns></returns>
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

        /// <summary>
        /// Number of trees seen to the south
        /// </summary>
        /// <param name="row">Row in grid</param>
        /// <param name="col">Col in grid</param>
        /// <returns></returns>
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
