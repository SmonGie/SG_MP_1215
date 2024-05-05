using Moq;
using Data;

namespace Tests
{
    [TestFixture]
    public class AbstractDataApiTests
    {
        [Test]
        public void CreateInstance_ReturnsNonNullInstance()
        {
            var mockDataApi = new Mock<AbstractDataApi>();

            AbstractDataApi instance = mockDataApi.Object;

            Assert.IsNotNull(instance);
        }

        [Test]
        public void BallRadius_ReturnsValidValue()
        {
            
            var mockDataApi = new Mock<AbstractDataApi>();
            mockDataApi.Setup(api => api.BallRadius).Returns(10); // Ustawiamy wartość zwracaną przez BallRadius na 10

            int radius = mockDataApi.Object.BallRadius;

            Assert.AreEqual(10, radius);
        }

        [Test]
        public void HeightWindow_ReturnsValidValue()
        {
            var mockDataApi = new Mock<AbstractDataApi>();
            mockDataApi.Setup(api => api.HeightWindow).Returns(100); // Ustawiamy wartość zwracaną przez HeightWindow na 100

            int height = mockDataApi.Object.HeightWindow;

            Assert.AreEqual(100, height);
        }

        [Test]
        public void WidthWindow_ReturnsValidValue()
        {
            var mockDataApi = new Mock<AbstractDataApi>();
            mockDataApi.Setup(api => api.WidthWindow).Returns(200); // Ustawiamy wartość zwracaną przez WidthWindow na 200

            int width = mockDataApi.Object.WidthWindow;

            Assert.AreEqual(200, width);
        }
    }
}



