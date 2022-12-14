using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCode2022.Puzzles
{
    public static class Day12
    {
        public static int Part1(string filePath)
        {
            IEnumerable<string> lines = System.IO.File.ReadLines(filePath);
            Point[,] heights = new Point[lines.Count(), lines.First().Length];
            Point start = new Point(0, 0);
            Point end = new Point(0, 0);

            for (int row = 0; row < lines.Count(); row++)
            {
                char[] columns = lines.ElementAt(row).ToCharArray();

                for (int col = 0; col < columns.Count(); col++)
                {
                    char current = columns[col];

                    if (current == 'S')
                    {
                        start = new Point(row, col);
                    }
                    if (current == 'E')
                    {
                        end = new Point(row, col);
                    }

                    heights[row, col] = new Point(row, col) { Value = GetHeight(current) };
                }
            }

            Graph graph = new Graph(heights);
            BreadthFirstSearch bfs = new BreadthFirstSearch(graph, start);

            while (bfs.CanSearch())
            {
                Point currentLocation = bfs.Search();

                if (currentLocation.X == end.X && currentLocation.Y == end.Y)
                {
                    return bfs.GetDistance(currentLocation);
                }
            }

            return -1;
        }

        public static int Part2(string filePath)
        {
            IEnumerable<string> lines = System.IO.File.ReadLines(filePath);
            Point[,] heights = new Point[lines.Count(), lines.First().Length];
            List<Point> startingPoints = new List<Point>();
            List<int> distances = new List<int>();
            Point end = new Point(0, 0);

            for (int row = 0; row < lines.Count(); row++)
            {
                char[] columns = lines.ElementAt(row).ToCharArray();

                for (int col = 0; col < columns.Count(); col++)
                {
                    char current = columns[col];

                    if (current == 'S' || current == 'a')
                    {
                        startingPoints.Add(new Point(row, col));
                    }
                    if (current == 'E')
                    {
                        end = new Point(row, col);
                    }

                    heights[row, col] = new Point(row, col) { Value = GetHeight(current) };
                }
            }

            foreach (Point start in startingPoints)
            {
                Graph graph = new Graph(heights);
                BreadthFirstSearch bfs = new BreadthFirstSearch(graph, start);

                while (bfs.CanSearch())
                {
                    Point currentLocation = bfs.Search();

                    if (currentLocation.X == end.X && currentLocation.Y == end.Y)
                    {
                        distances.Add(bfs.GetDistance(currentLocation));
                    }
                }
            }

            return distances.OrderBy(x => x).First();
        }

        public static int GetHeight(char c)
        {
            if (c == 'S')
                return 1;

            if (c == 'E')
                c = 'z';

            return (int)Char.ToUpper(c) - 64;
        }
    }

    internal class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal class Graph
    {
        public Point[,] Points { get; set; }

        public int Rows
        {
            get { return Points.GetLength(0); }
        }

        public int Columns
        {
            get { return Points.GetLength(1); }
        }

        public Graph(Point[,] map)
        {
            Points = map;
        }

        private List<Point> GetNeighbors(Point p)
        {
            // list out possible neighbors
            List<Point> neighbors = new List<Point>();
            neighbors.Add(new Point(p.X - 1, p.Y)); // north
            neighbors.Add(new Point(p.X, p.Y + 1)); //south
            neighbors.Add(new Point(p.X + 1, p.Y)); // east
            neighbors.Add(new Point(p.X, p.Y - 1)); // west

            return neighbors;
        }

        public List<Point> FindNeighbors(Point p)
        {
            // limit to neighbors in bounds of grid
            List<Point> possibleNeighbors = GetNeighbors(p);
            return possibleNeighbors.Where(x => CanMove(p, x)).ToList();
        }

        public bool Contains(Point p)
        {
            // make sure the point is in the bounds of the graph
            return p.X >= 0 && p.Y >= 0 && p.X < this.Rows && p.Y < this.Columns;
        }

        public bool CanMove(Point from, Point to)
        {
            // if either position is not in the grid the move is not possible
            if (!Contains(from) || !Contains(to))
                return false;

            int toHeight = this.Points[to.X, to.Y].Value;
            int fromHeight = this.Points[from.X, from.Y].Value;

            // the condition for the puzzle is you can only move up
            // 1 height at a time. 
            return toHeight <= fromHeight + 1;
        }
    }

    internal class BreadthFirstSearch
    {
        public Graph Map { get; set; }
        public Point StartingPosition { get; set; }
        public int[,] Distances { get; set; }
        public Queue<Point> TrackerQueue { get; set; }

        public BreadthFirstSearch(Graph map, Point p)
        {
            Map = map;
            StartingPosition = p;
            Distances = new int[Map.Rows, Map.Columns];

            for (int row = 0; row < Map.Rows; row++)
            {
                for (int col = 0; col < Map.Columns; col++)
                {
                    this.Distances[row, col] = -1;
                }
            }

            Distances[p.X, p.Y] = 0;
            TrackerQueue = new Queue<Point>();
            TrackerQueue.Enqueue(StartingPosition);
        }

        public int GetDistance(Point p)
        {
            return Distances[p.X, p.Y];
        }

        public bool CanSearch()
        {
            return TrackerQueue.Count() > 0;
        }

        public Point Search()
        {
            // We have not solved the puzzle yet
            Point searching = TrackerQueue.Dequeue();

            foreach (Point neighbor in Map.FindNeighbors(searching))
            {
                // if the neighbor has not yet been explored, we 
                // set the distance and add it to the queue
                if (Distances[neighbor.X, neighbor.Y] == -1)
                {
                    Distances[neighbor.X, neighbor.Y] = Distances[searching.X, searching.Y] + 1;
                    TrackerQueue.Enqueue(neighbor);
                }
            }

            return searching;
        }
    }
}
