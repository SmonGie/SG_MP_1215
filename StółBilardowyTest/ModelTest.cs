/*
using Model;
using Moq;
namespace Tests
{
    [TestFixture]
    public class ModelApiTest
    {
        private const int testDiameter = 10;
        private const int testRadius = testDiameter / 2;
        private Mock<AbstractModelApi> modelApiMock;

        [SetUp]
        public void Setup()
        {
            modelApiMock = new Mock<AbstractModelApi>();
        }

        [Test]
        public void ConstructorTest()
        {
            
            AbstractModelApi modelApi = modelApiMock.Object;

            Assert.IsNotNull(modelApi);
        }

        [Test]
        public void SpawnBallsTest()
        {
            int numberOfBalls = 10;
            modelApiMock.Setup(m => m.SpawnBalls(numberOfBalls)); 

            modelApiMock.Object.SpawnBalls(numberOfBalls); 

            modelApiMock.Verify(m => m.SpawnBalls(numberOfBalls), Times.Once); 
                                                                               
        }


        [Test]
        public void SubscribeTest()
        {
            
            var observer = new Mock<TestObserver<IEnumerable<IBallModel>>>();
            var disposableMock = new Mock<IDisposable>();

            modelApiMock.Setup(m => m.Subscribe(It.IsAny<IObserver<IEnumerable<IBallModel>>>())) 
                       .Returns(disposableMock.Object); 

          
            IDisposable subscription = modelApiMock.Object.Subscribe(observer.Object); 

           
            Assert.IsNotNull(subscription); 
            modelApiMock.Verify(m => m.Subscribe(observer.Object), Times.Once); 
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
*/