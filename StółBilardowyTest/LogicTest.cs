using NUnit.Framework;
using Moq;
using System.Numerics;
using Data;
using Logic;

namespace Logic.Tests
{
    [TestFixture]
    public class LogicBallTests
    {
        private logicBall logic;
        private FakeDataApi fakeDataApi;

        [SetUp]
        public void Setup()
        {
            fakeDataApi = new FakeDataApi();
            logic = new logicBall(fakeDataApi);
        }

        [Test]
        public void GetNumberOfBalls_ReturnsCorrectNumber()
        {
            // Arrange
            int expectedNumberOfBalls = 5;
            fakeDataApi.SpawnBalls(expectedNumberOfBalls);

            // Act
            int actualNumberOfBalls = logic.GetNumberOfBalls();

            // Assert
            Assert.AreEqual(expectedNumberOfBalls, actualNumberOfBalls);
        }

    }

    public class FakeDataApi : AbstractDataApi
    {
        private List<IBall> Balls { get; }

        public FakeDataApi()
        {
            Balls = new List<IBall>();
        }

        public override event EventHandler<BallEventArgs> BallEvent
        {
            add { }
            remove { }
        }

        public override int Width => 600;

        public override int Height => 600;

        public override int GetNumberOfBalls()
        {
            return Balls.Count;
        }

        public override Vector2 GetBallPosition(int number)
        {
            if (number < 0 || number >= Balls.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            return Balls[number].Position;
        }

        public override void SpawnBalls(int amount)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                IBall ball = new FakeBall(new Vector2(random.Next(100, Width - 100), random.Next(100, Height - 100)));
                Balls.Add(ball);
            }
        }

        public override IBall GetBall(int number)
        {
            if (number < 0 || number >= Balls.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            return Balls[number];
        }

        private class FakeBall : IBall
        {
            public Vector2 Position { get; }
            public Vector2 Velocity { get; set; }
            public float Mass { get; }

            public FakeBall(Vector2 position)
            {
                Position = position;
                Velocity = Vector2.Zero;
                Mass = 1.0f;
            }
        }
    }
}