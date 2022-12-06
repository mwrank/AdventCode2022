using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventCode2022.Puzzles
{
    public static class Day6
    {
        public static int Part1(string filePath)
        {
            string firstLine = System.IO.File.ReadLines(filePath).First();
            CommunicationDevice device = new CommunicationDevice();
            return device.FindFirstPacketMarker(firstLine, 4);
        }

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

        public int FindFirstPacketMarker(string signal, int length)
        {
            char[] signalArr = signal.ToCharArray();

            for(int i = (length - 1); i < signalArr.Count(); i++)
            {
                char curr = signalArr[i];
                List<char> prev = new List<char>();

                for (int k = 0; k < length; k++)
                {
                    prev.Add(signalArr[i - k]);
                }

                int distinctCount = prev.Distinct().Count();

                if (distinctCount == prev.Count())
                    return (i + 1);
            }

            return -1;
        }
    }
}
