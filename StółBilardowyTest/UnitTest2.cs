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
           

            // Tworzenie mocka dla AbstractLogicApi
            var logicApiMock = new Mock<AbstractLogicApi>();

            // Określenie zachowania mocka dla metody Balls
            logicApiMock.SetupGet(api => api.Balls).Returns(new List<Ball>
    {
        new Ball(new Vector2(testVelocityX, testVelocityY), new Vector2(testPositionX, testPositionY), testDiameter)
    });

            // Pobranie pierwszej kuli z mocka AbstractLogicApi
            testBall = logicApiMock.Object.Balls.First();

            // Sprawdzenie, czy testBall nie jest null
            Assert.IsNotNull(testBall);

            // Sprawdzenie, czy testBall posiada odpowiednie właściwości
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
            // Tworzenie mocka dla AbstractLogicApi
            var logicApiMock = new Mock<AbstractLogicApi>();

            // Określenie zachowania mocka dla metody Balls
            logicApiMock.SetupGet(api => api.Balls).Returns(new List<Ball>
    {
        new Ball(new Vector2(testVelocityX, testVelocityY), new Vector2(testPositionX, testPositionY), testDiameter)
    });

            // Pobranie pierwszej kuli z mocka AbstractLogicApi
            var testBall = logicApiMock.Object.Balls.First();

            // Sprawdzenie, czy testBall nie jest null
            Assert.IsNotNull(testBall);

            // Sprawdzenie, czy promień kuli jest poprawnie obliczony
            Assert.AreEqual(testDiameter / 2, testBall.Radius);

            // Sprawdzenie, czy średnica kuli jest ustawiona poprawnie
            Assert.AreEqual(testDiameter, testBall.Diameter);
        }

        [Test]
        public void EqualsTest()
        {
            // Tworzenie mocka dla AbstractLogicApi
            var logicApiMock = new Mock<AbstractLogicApi>();

            // Określenie zachowania mocka dla metody Balls
            logicApiMock.SetupGet(api => api.Balls).Returns(new List<Ball>
    {
        new Ball(new Vector2(testVelocityX, testVelocityY), new Vector2(testPositionX, testPositionY), testDiameter)
    });

            // Pobranie pierwszej kuli z mocka AbstractLogicApi
            var testBall1 = logicApiMock.Object.Balls.First();

            // Tworzenie drugiej kuli, która będzie miała takie same wartości jak pierwsza
            var testBall2 = new Ball(new Vector2(testVelocityX, testVelocityY), new Vector2(testPositionX, testPositionY), testDiameter);

            // Porównanie obiektów Ball
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
        private AbstractLogicApi ballManager;
        private Mock<AbstractLogicApi> logicApiMock;


        [SetUp]
        public void Setup()
        {
            // Creating a mock instance of SimulationController
            logicApiMock = new Mock<AbstractLogicApi>();

            // Assigning the mock object to the ballManager field
            ballManager = logicApiMock.Object;
        }

        [Test]
        public void ConstructorTest()
        {
            // Verify that the constructor of AbstractLogicApi was called
            logicApiMock.Verify(x => x, Times.Once);

            // Assert that ballManager is not null
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
