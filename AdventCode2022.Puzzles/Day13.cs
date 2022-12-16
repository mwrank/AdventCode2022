using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace AdventCode2022.Puzzles
{
    public class Day13
    {
        /// <summary>
        /// Advent of code day 13 part 1
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Sum of the indices of correct pairs</returns>
        public static int Part1(string filePath)
        {
            string[] lines = System.IO.File.ReadLines(filePath).ToArray();
            DistressSignal distressSignal = new DistressSignal();
            distressSignal.BuildFromLines(lines);

            int result = 0;
            
            for(int i = 0; i < distressSignal.PacketPairs.Count(); i++)
            {
                if (distressSignal.PacketPairs[i].IsOrdered())
                    result += (i + 1);
            }

            return result;
        }

        /// <summary>
        /// Advent of code day 13 part 1
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Sum of the indices of correct pairs</returns>
        public static int Part2(string filePath)
        {
            List<string> lines = System.IO.File.ReadLines(filePath).ToList();
            lines.Add(Environment.NewLine);
            lines.Add("[[2]]");
            lines.Add("[[6]]");

            DistressSignal distressSignal = new DistressSignal();
            distressSignal.BuildFromLines(lines.ToArray());

            // flatten the packet pairs to a single list
            List<Packet> packets = distressSignal.PacketPairs.SelectMany(x => x.Packets).ToList();
            PacketComparer pc = new PacketComparer();
            var t = packets.OrderBy(x => x.Data, pc).ToList();

            int indexOf2 = t.IndexOf(packets.Where(x => x.InitialString == "[[2]]").First()) + 1;
            int indexOf6 = t.IndexOf(packets.Where(x => x.InitialString == "[[6]]").First()) + 1;

            return (indexOf2 * indexOf6);
        }
    }

    internal class DistressSignal
    {
        public List<PacketPair> PacketPairs { get; set; }

        public DistressSignal()
        {
            PacketPairs = new List<PacketPair>();
        }

        /// <summary>
        /// Read lines into pairs of packets
        /// </summary>
        /// <param name="lines">Lines to parse</param>
        public void BuildFromLines(string[] lines)
        {
            for (int i = 0; i < lines.Count(); i += 3)
            {
                Packet first = new Packet(lines[i]);
                Packet second = new Packet(lines[i + 1]);

                PacketPair pair = new PacketPair();
                pair.AddPacket(first);
                pair.AddPacket(second);

                PacketPairs.Add(pair);
            }
        }
    }

    internal class PacketPair
    {
        public List<Packet> Packets { get; }

        public PacketPair()
        {
            Packets = new List<Packet>();
        }

        /// <summary>
        /// Adds a packet to the pair
        /// </summary>
        /// <param name="p"></param>
        public void AddPacket(Packet p)
        {
            Packets.Add(p);
        }

        /// <summary>
        /// Checks if packet pair is ordered correctly
        /// </summary>
        /// <returns></returns>
        public bool IsOrdered()
        {
            PacketComparer pc = new PacketComparer();
            return pc.Compare(Packets.ElementAt(0).Data, Packets.ElementAt(1).Data) <= 0;
        }
    }

    internal class Packet
    {
        private Queue<char> ParserQueue { get; set; }
        public string InitialString { get; set; }
        public List<object> Data { get; set; }

        public Packet(string line)
        {
            InitialString = line;
            ParserQueue = new Queue<char>();
            char[] arr = line.ToCharArray();

            // load up the chars in a queue
            for(int i = 0; i < arr.Count(); i++)
                ParserQueue.Enqueue(arr[i]);

            Data = ParseList();
        }

        /// <summary>
        /// Build object from queue items
        /// </summary>
        /// <returns></returns>
        private object Parse()
        {
            char next = ParserQueue.Peek();

            if (char.IsDigit(next))
            {
                return ParseInt();
            }
            else
            {
                return ParseList();
            }
        }

        /// <summary>
        /// Build list from queue items
        /// </summary>
        /// <returns></returns>
        private List<object> ParseList()
        {
            List<object> elements = new List<object>();
            ParserQueue.Dequeue(); // remove '['

            while (ParserQueue.Peek() != ']')
            {
                if (ParserQueue.Peek() == ',')
                {
                    ParserQueue.Dequeue();
                }

                object el = Parse();
                elements.Add(el);
            }

            ParserQueue.Dequeue(); // remove ']'
            return elements;
        }

        /// <summary>
        /// Build int form queue items
        /// </summary>
        /// <returns></returns>
        private int ParseInt()
        {
            string token = string.Empty;

            while (char.IsDigit(ParserQueue.Peek()))
            {
                token += ParserQueue.Dequeue();
            }

            return int.Parse(token);
        }
    }

    internal class PacketComparer : IComparer<object>
    {
        /// <summary>
        /// Compare objects (ints)
        /// </summary>
        /// <param name="first">First object</param>
        /// <param name="second">Second object</param>
        /// <returns></returns>
        public int Compare([AllowNull] object first, [AllowNull] object second)
        {
            // both ints
            if (first is int && second is int)
            {
                return Math.Sign((int)first - (int)second);
            }
            else if (first is List<object> && second is List<object>) // both lists
            {
                return CompareLists((List<object>)first, (List<object>)second); // list, int
            }
            else if (first is int && second is List<object>)
            {
                return CompareLists(new List<object>() { first }, (List<object>)second);
            }
            else // int, list
            {
                return CompareLists((List<object>)first, new List<object>() { second });
            }
        }

        /// <summary>
        /// Compares lists
        /// </summary>
        /// <param name="first">First list</param>
        /// <param name="second">Second list</param>
        /// <returns>Sign</returns>
        private int CompareLists(List<object> first, List<object> second)
        {
            // get the count of the smaller list
            int idx = Math.Min(first.Count(), second.Count());

            for (int ix = 0; ix < idx; ix++)
            {
                object firstObj = first[ix]; // obj in first list
                object secondObj = second[ix]; // obj in second list
                int diff = Compare(firstObj, secondObj); // call compare with those obj

                if (diff < 0)
                {
                    return -1;
                }
                else if (diff > 0)
                {
                    return 1;
                }
            }

            // -1 less than 0
            // 0 then 0
            // 1 greater than 0
            return Math.Sign(first.Count() - second.Count());
        }
    }
}
