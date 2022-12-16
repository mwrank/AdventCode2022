using AdventCode2022.Puzzles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace AdventCode2022.Tests
{
    [TestClass]
    public class AdventTests
    {
        #region Day1
        [TestMethod]
        public void Day1Part1()
        {
            string path = @"Data\Day1\sample.txt";
            int result = Day1.Part1(path);
            Assert.AreEqual(result, 24000);
        }

        [TestMethod]
        public void Day1Part2()
        {
            string path = @"Data\Day1\sample.txt";
            int result = Day1.Part2(path);
            Assert.AreEqual(result, 45000);
        }
        #endregion

        #region Day2
        [TestMethod]
        public void Day2Part1()
        {
            string path = @"Data\Day2\sample.txt";
            int result = Day2.Part1(path);
            Assert.AreEqual(result, 15);
        }

        [TestMethod]
        public void Day2Part2()
        {
            string path = @"Data\Day2\sample.txt";
            int result = Day2.Part2(path);
            Assert.AreEqual(result, 12);
        }
        #endregion

        #region Day3
        [TestMethod]
        public void Day3Part1()
        {
            string path = @"Data\Day3\sample.txt";
            int result = Day3.Part1(path);
            Assert.AreEqual(result, 157);
        }

        [TestMethod]
        public void Day3Part2()
        {
            string path = @"Data\Day3\sample.txt";
            int result = Day3.Part2(path);
            Assert.AreEqual(result, 70);
        }
        #endregion

        #region Day4
        [TestMethod]
        public void Day4Part1()
        {
            string path = @"Data\Day4\sample.txt";
            int result = Day4.Part1(path);
            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void Day4Part2()
        {
            string path = @"Data\Day4\sample.txt";
            int result = Day4.Part2(path);
            Assert.AreEqual(result, 4);
        }
        #endregion

        #region Day5
        [TestMethod]
        public void Day5Part1()
        {
            string path = @"Data\Day5\sample.txt";
            string result = Day5.Part1(path);
            Assert.AreEqual(result, "CMZ");
        }

        [TestMethod]
        public void Day5Part2()
        {
            string path = @"Data\Day5\sample.txt";
            string result = Day5.Part2(path);
            Assert.AreEqual(result, "MCD");
        }
        #endregion

        #region Day6
        [TestMethod]
        public void Day6Part1()
        {
            string path = @"Data\Day6\sample.txt";
            int result = Day6.Part1(path);
            Assert.AreEqual(result, 7);
        }

        [TestMethod]
        public void Day6Part2()
        {
            string path = @"Data\Day6\sample.txt";
            int result = Day6.Part2(path);
            Assert.AreEqual(result, 19);
        }
        #endregion

        #region Day7
        [TestMethod]
        public void Day7Part1()
        {
            string path = @"Data\Day7\sample.txt";
            int result = Day7.Part1(path);
            Assert.AreEqual(result, 95437);
        }

        [TestMethod]
        public void Day7Part2()
        {
            string path = @"Data\Day7\sample.txt";
            int result = Day7.Part2(path);
            Assert.AreEqual(result, 24933642);
        }
        #endregion

        #region Day8
        [TestMethod]
        public void Day8Part1()
        {
            string path = @"Data\Day8\sample.txt";
            int result = Day8.Part1(path);
            Assert.AreEqual(result, 21);
        }

        [TestMethod]
        public void Day8Part2()
        {
            string path = @"Data\Day8\sample.txt";
            int result = Day8.Part2(path);
            Assert.AreEqual(result, 8);
        }
        #endregion

        #region Day9
        [TestMethod]
        public void Day9Part1()
        {
            string path = @"Data\Day9\sample.txt";
            int result = Day9.Part1(path);
            Assert.AreEqual(result, 13);
        }

        [TestMethod]
        public void Day9Part2()
        {
            string path = @"Data\Day9\sample2.txt";
            int result = Day9.Part2(path);
            Assert.AreEqual(result, 36);
        }
        #endregion

        #region Day10
        [TestMethod]
        public void Day10Part1()
        {
            string path = @"Data\Day10\sample.txt";
            int result = Day10.Part1(path);
            Assert.AreEqual(result, 13140);
        }
        #endregion

        #region Day11
        [TestMethod]
        public void Day11Part1()
        {
            string path = @"Data\Day11\sample.txt";
            BigInteger result = Day11.Part1(path);
            Assert.AreEqual(result, 10605);
        }

        [TestMethod]
        public void Day11Part2()
        {
            string path = @"Data\Day11\sample.txt";
            BigInteger result = Day11.Part2(path);
            Assert.AreEqual(result, 2713310158);
        }
        #endregion

        #region Day12
        [TestMethod]
        public void Day12Part1()
        {
            string path = @"Data\Day12\sample.txt";
            int result = Day12.Part1(path);
            Assert.AreEqual(result, 31);
        }

        [TestMethod]
        public void Day12Part2()
        {
            string path = @"Data\Day12\sample.txt";
            int result = Day12.Part2(path);
            Assert.AreEqual(result, 29);
        }
        #endregion

        #region Day13
        [TestMethod]
        public void Day13Part1()
        {
            string path = @"Data\Day13\sample.txt";
            int result = Day13.Part1(path);
            Assert.AreEqual(result, 13);
        }

        [TestMethod]
        public void Day13Part2()
        {
            string path = @"Data\Day13\sample.txt";
            int result = Day13.Part2(path);
            Assert.AreEqual(result, 140);
        }
        #endregion
    }
}
