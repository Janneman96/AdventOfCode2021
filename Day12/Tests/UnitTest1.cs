using Day12;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [DataTestMethod]
        [DataRow("input10", 10)]
        //[DataRow("input19", 19)]
        //[DataRow("input226", 226)]
        public void ShouldCalculateAmountOfPaths(string input, int expectedPaths)
        {
            // Arrange
            var inputLines = System.IO.File.ReadAllLines($@"C:\git\adventofcode2021\Day12\Day12\{input}.txt");
            var pathing = new Pathing();

            // Act
            var result = pathing.GetPaths(inputLines);

            // Assert
            Assert.AreEqual(expectedPaths, result.Count());
        }
    }
}
