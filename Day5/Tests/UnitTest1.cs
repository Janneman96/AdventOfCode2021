

using Day5;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void horizontalInOrder()
        {
            // Arrange & Act
            var result = Program.GetPoints(x1: 1, y1: 4, x2: 3, y2: 4);

            // Assert

            Assert.IsTrue(result.Contains("1,4"));
            Assert.IsTrue(result.Contains("2,4"));
            Assert.IsTrue(result.Contains("3,4"));
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void horizontalInReverseOrder()
        {
            // Arrange & Act
            var result = Program.GetPoints(x1: 3, y1: 4, x2: 1, y2: 4);

            // Assert

            Assert.IsTrue(result.Contains("3,4"));
            Assert.IsTrue(result.Contains("2,4"));
            Assert.IsTrue(result.Contains("1,4"));
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void verticalInOrder()
        {
            // Arrange & Act
            var result = Program.GetPoints(x1: 4, y1: 1, x2: 4, y2: 3);

            // Assert

            Assert.IsTrue(result.Contains("4,1"));
            Assert.IsTrue(result.Contains("4,2"));
            Assert.IsTrue(result.Contains("4,3"));
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void verticalInReverseOrder()
        {
            // Arrange & Act
            var result = Program.GetPoints(x1: 4, y1: 3, x2: 4, y2: 1);

            // Assert

            Assert.IsTrue(result.Contains("4,3"));
            Assert.IsTrue(result.Contains("4,2"));
            Assert.IsTrue(result.Contains("4,1"));
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void diagonalInOrder()
        {
            // Arrange & Act
            var result = Program.GetPoints(x1: 0, y1: 0, x2: 1, y2: 1);

            // Assert
            Assert.IsTrue(result.Contains("0,0"));
            Assert.IsTrue(result.Contains("1,1"));
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void diagonalInReverseOrder()
        {
            // Arrange & Act
            var result = Program.GetPoints(x1: 1, y1: 1, x2: 0, y2: 0);

            // Assert
            Assert.IsTrue(result.Contains("0,0"));
            Assert.IsTrue(result.Contains("1,1"));
            Assert.AreEqual(2, result.Count);
        }
    }
}