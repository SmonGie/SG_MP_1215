using NUnit.Framework;
using System.Collections.Generic;
using Data;
using Moq;

namespace Logic.Tests
{
    [TestFixture]
    public class LogicBallTests
    {
        private AbstractLogicApi logicBall;
        private Mock<AbstractDataApi> dataApiMock;

        [SetUp]
        public void Setup()
        {
            dataApiMock = new Mock<AbstractDataApi>();
            logicBall = new logicBall(dataApiMock.Object);
        }

        [Test]
        public void SpawnBalls_SpawnsBall()
        {
            // Arrange
            var ballMock = new Mock<AbstractBallApi>();
            dataApiMock.Setup(x => x.SpawnBalls(true)).Returns(ballMock.Object);

            // Act
            logicBall.SpawnBalls();

            // Assert
            Assert.Contains(ballMock.Object, logicBall.logicBalls);
        }

        [Test]
        public void GetPositionX_ReturnsCorrectValue()
        {
            // Arrange
            var ballMock = new Mock<AbstractBallApi>();
            ballMock.Setup(x => x.Position).Returns(new System.Numerics.Vector2(10, 20));
            logicBall.logicBalls.Add(ballMock.Object);

            // Act
            var positionX = logicBall.GetPositionX(0);

            // Assert
            Assert.AreEqual(20, positionX);
        }

        [Test]
        public void GetPositionY_ReturnsCorrectValue()
        {
            // Arrange
            var ballMock = new Mock<AbstractBallApi>();
            ballMock.Setup(x => x.Position).Returns(new System.Numerics.Vector2(10, 20));
            logicBall.logicBalls.Add(ballMock.Object);

            // Act
            var positionY = logicBall.GetPositionY(0);

            // Assert
            Assert.AreEqual(10, positionY);
        }
    }
}
