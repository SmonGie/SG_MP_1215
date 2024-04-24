using Model;
using Moq;
namespace Tests
{
    [TestFixture]
    public class ModelApiTest
    {
        private const int testDiameter = 10;
        private const int testRadius = testDiameter / 2;
        // private AbstractModelApi modelApi = AbstractModelApi.CreateInstance();
        private Mock<AbstractModelApi> modelApiMock;

        [SetUp]
        public void Setup()
        {
            // Create a mock instance of AbstractModelApi
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
            // Arrange
            int numberOfBalls = 10;
            modelApiMock.Setup(m => m.SpawnBalls(numberOfBalls)); 

            // Act
            modelApiMock.Object.SpawnBalls(numberOfBalls); 

            // Assert
            modelApiMock.Verify(m => m.SpawnBalls(numberOfBalls), Times.Once); 
                                                                               
        }


        [Test]
        public void SubscribeTest()
        {
            // Arrange
            var observer = new Mock<TestObserver<IEnumerable<IBallModel>>>();
            var disposableMock = new Mock<IDisposable>();

            modelApiMock.Setup(m => m.Subscribe(It.IsAny<IObserver<IEnumerable<IBallModel>>>())) // Set up expectation for the method call
                       .Returns(disposableMock.Object); // Return a disposable mock object

            // Act
            IDisposable subscription = modelApiMock.Object.Subscribe(observer.Object); // Call the method

            // Assert
            Assert.IsNotNull(subscription); // Check if the subscription is not null
            modelApiMock.Verify(m => m.Subscribe(observer.Object), Times.Once); // Verify that the method was called with the expected observer
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