/*
using NUnit.Framework;
using System.Numerics;

namespace Data.Tests
{
    [TestFixture]
    public class BallAPITests
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

            ball = AbstractBallApi.CreateInstance(position, deltaX, deltaY, mass, size, isSimulationRunning);
        }

        [Test]
        public void BallAPI_Move_UpdatesPosition()
        {
            // Arrange
            int initialX = ball.X;
            int initialY = ball.Y;

            // Act
            ball.Move(); // Simulate movement

            // Assert
            Assert.AreEqual(initialX + ball.VelocityX, ball.X);
            Assert.AreEqual(initialY + ball.VelocityY, ball.Y);
        }

        [Test]
        public void BallAPI_CreateBallAPITest()
        {
            Assert.IsNotNull(ball);
            Assert.IsInstanceOf<AbstractBallApi>(ball);
        }

        [Test]
        public void BallAPI_PositionTest()
        {
            Vector2 expectedPosition = new Vector2(2, 2);

            Vector2 actualPosition = ball.Position;

            Assert.AreEqual(expectedPosition, actualPosition);
        }

        // Add other test methods...

        [Test]
        public void BallAPI_IsSimulationRunning_GetValue()
        {
            Assert.IsFalse(ball.isWorking);
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
} */