using AdventCode2022.Library;
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
    }
}
