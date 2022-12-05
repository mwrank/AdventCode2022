using AdventCode2022.Puzzles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            string path = @"Data\Day4\full.txt";
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
    }
}
