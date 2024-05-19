using NUnit.Framework;
using System.Numerics;

namespace Data.Tests
{
    [TestFixture]
    public class DataTest
    {
        private AbstractBallApi ball;

        [SetUp]
        public void Setup()
        {
            Vector2 position = new Vector2(2, 2);
            int deltaX = 1;
            int deltaY = 1;
            int size = 10;
            int mass = 5;
            bool isSimulationRunning = false;

            ball = AbstractBallApi.CreateInstance(position, deltaX, deltaY, size, mass, isSimulationRunning);
        }

        [Test]
        public void BallAPI_PositionTest()
        {
            Vector2 position = new Vector2(2, 2);

            var excpectedPosition = ball.Position;

            Assert.AreEqual(excpectedPosition, position);
        }

        // Add other test methods...
    }

    [TestFixture]
    public class DataAPITest
    {
        private AbstractDataApi data;

        [SetUp]
        public void Setup()
        {
            int boardWidth = 500;
            int boardHeight = 400;
            data = AbstractDataApi.CreateInstance(boardHeight, boardWidth);
        }

        [Test]
        public void DataAPI_getBoardWidth()
        {
            int expectedValue = data.getWidthOfWindow();

            Assert.AreEqual(expectedValue, 500);
        }

        [Test]
        public void DataAPI_getBoardHeight()
        {
            int expectedValue = data.getHeightOfWindow();

            Assert.AreEqual(expectedValue, 400);
        }
    }
}