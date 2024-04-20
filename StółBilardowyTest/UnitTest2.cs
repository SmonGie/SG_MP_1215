using NUnit.Framework;
using Logic;
using Data;
using Moq;

namespace Tests
{
    [TestFixture]
    public class LogicApiTest
    {
        private readonly float testVelocityX = 0.2f;
        private readonly float testVelocityY = -0.1f;
        private readonly int testPositionX = 5;
        private readonly int testPositionY = 5;
        private readonly int testDiameter = 2;
        Ball testBall;

        [Test]
        public void ConstructorTest()
        {
            Vector2 velocity = new Vector2(testVelocityX, testVelocityY);
            Vector2 position = new Vector2(testPositionX, testPositionY);

            testBall = new Ball(velocity, position, testDiameter);

            Assert.IsNotNull(testBall);
            Assert.AreEqual(testVelocityX, testBall.Velocity.X);
            Assert.AreEqual(testVelocityY, testBall.Velocity.Y);
            Assert.AreEqual(testPositionX, testBall.Position.X);
            Assert.AreEqual(testPositionY, testBall.Position.Y);
            Assert.AreEqual(testDiameter, testBall.Diameter);
            Assert.AreEqual(testDiameter / 2, testBall.Radius);
        }

        [Test]
        public void ConstructorRadiusAndDiameterTest()
        {
            Vector2 velocity = new Vector2(testVelocityX, testVelocityY);
            Vector2 position = new Vector2(testPositionX, testPositionY);
            Ball testBall = new Ball(velocity, position, testDiameter);

            Assert.AreEqual(testDiameter / 2, testBall.Radius);
            Assert.AreEqual(testDiameter, testBall.Diameter);
        }

        [Test]
        public void EqualsTest()
        {
            Vector2 velocity = new Vector2(testVelocityX, testVelocityY);
            Vector2 position = new Vector2(testPositionX, testPositionY);
            Ball testBall1 = new Ball(velocity, position, testDiameter);
            Ball testBall2 = new Ball(velocity, position, testDiameter);

            Assert.AreEqual(testBall1, testBall2);
        }
    }
    [TestFixture]
    public class SimulationManagerTest
    {
        private const int testDiameter = 10;
        private const int testRadius = testDiameter / 2;
        private const int testWidth = 100;
        private const int testHeight = 100;
        private SimulationController ballManager;

        [SetUp]
        public void Setup()
        {
            ballManager = new SimulationController();
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.IsNotNull(ballManager);
        }

        [Test]
        public void RandomizedSpawningTest()
        {
            int numberOfBalls = 10;
            int counter = 0;

            // Ręcznie tworzymy kolekcję 10 kul do testowania
            List<Ball> balls = new List<Ball>();
            for (int i = 0; i < numberOfBalls; i++)
            {
                balls.Add(new Ball(50,50,30,40,10));
            }

            Assert.IsNotNull(ballManager);

            foreach (Ball ball in balls)
            {
                Assert.IsNotNull(ball);
                Assert.AreEqual(testDiameter, ball.Diameter);
                counter++;
            }
            Assert.AreEqual(numberOfBalls, counter);
        }
    }
}
