using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventCode2022.Puzzles
{
    public static class Day6
    {
        /// <summary>
        /// Advent code day 6 part 1
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>Index of marker start</returns>
        public static int Part1(string filePath)
        {
            string firstLine = System.IO.File.ReadLines(filePath).First();
            CommunicationDevice device = new CommunicationDevice();
            return device.FindFirstPacketMarker(firstLine, 4);
        }

        /// <summary>
        /// Advent code day 6 part 2
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns>Index of marker start</returns>
        public static int Part2(string filePath)
        {
            string firstLine = System.IO.File.ReadLines(filePath).First();
            CommunicationDevice device = new CommunicationDevice();
            return device.FindFirstPacketMarker(firstLine, 14);
        }
    }

    internal class CommunicationDevice
    {
        public CommunicationDevice() { }

        /// <summary>
        /// Find first packet marker
        /// </summary>
        /// <param name="signal">Signal to check</param>
        /// <param name="length">Length of window to check for duplicate char</param>
        /// <returns>Index of marker start</returns>
        public int FindFirstPacketMarker(string signal, int length)
        {
            char[] signalArr = signal.ToCharArray(); // make an array

            for(int i = 0; i < signalArr.Count(); i++)
            {
                // use skip and take to create sliding window
                List<char> prev = signalArr.Skip(i).Take(length).ToList();
                int distinctCount = prev.Distinct().Count();

                // check if count = distinct count
                if (distinctCount == prev.Count())
                    return (i + length);
            }

            return -1;
        }
    }
}
