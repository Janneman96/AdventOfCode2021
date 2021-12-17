using Day15;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Example()
        {
            // Arrange
            var inputLines = new string[]
            {
                "1163751742",
                "1381373672",
                "2136511328",
                "3694931569",
                "7463417111",
                "1319128137",
                "1359912421",
                "3125421639",
                "1293138521",
                "2311944581",
            };
            var cavern = Program.GetCavern(inputLines);

            // Act
            var result = Program.GetTotalRisk(cavern);

            // Assert
            Assert.AreEqual(40, result);
        }


        [TestMethod]
        public void GoesDiagonal()
        {
            // Arrange
            var inputLines = new string[]
            {
                "19999",
                "11999",
                "91199",
                "99119",
                "99911"
            };
            var cavern = Program.GetCavern(inputLines);

            // Act
            var result = Program.GetTotalRisk(cavern);

            // Assert
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void GoesBackUp()
        {
            // Arrange
            var inputLines = new string[]
            {
                "19999",
                "19111",
                "19191",
                "11191",
                "99991",
            };
            var cavern = Program.GetCavern(inputLines);

            // Act
            var result = Program.GetTotalRisk(cavern);

            // Assert
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void GoesLeft()
        {
            // Arrange
            var inputLines = new string[]
            {
                "11119",
                "99919",
                "91119",
                "91999",
                "91111",
            };
            var cavern = Program.GetCavern(inputLines);

            // Act
            var result = Program.GetTotalRisk(cavern);

            // Assert
            Assert.AreEqual(12, result);
        }
    }
}
