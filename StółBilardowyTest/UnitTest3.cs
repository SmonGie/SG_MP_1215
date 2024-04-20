using Model;

namespace Tests
{
    [TestFixture]
    public class ModelApiTest
    {
        private const int testDiameter = 10;
        private const int testRadius = testDiameter / 2;
        private AbstractModelApi modelApi = AbstractModelApi.CreateInstance();

        [Test]
        public void ConstructorTest()
        {
            Assert.IsNotNull(modelApi);
        }

        [Test]
        public void SpawnBallsTest()
        {
            int numberOfBalls = 10;
            modelApi.SpawnBalls(numberOfBalls);

            // Tutaj można dodać dodatkowe asercje, jeśli jest coś do sprawdzenia po wywołaniu metody SpawnBalls
        }

        [Test]
        public void SubscribeTest()
        {
            TestObserver<IEnumerable<IBallModel>> observer = new TestObserver<IEnumerable<IBallModel>>();
            IDisposable subscription = modelApi.Subscribe(observer);
            Assert.IsNotNull(subscription);
        }

        // Klasa pomocnicza do testowania subskrypcji
        public class TestObserver<T> : IObserver<T>
        {
            public List<T> ReceivedValues { get; } = new List<T>();

            public void OnCompleted()
            {
                
            }

            public void OnError(Exception error)
            {
                
            }

            public void OnNext(T value)
            {
                ReceivedValues.Add(value);
            }
        }
    }
}