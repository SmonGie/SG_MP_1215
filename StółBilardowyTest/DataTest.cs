using NUnit.Framework;
using Moq;
using System.Numerics;

namespace Data.Tests
{
    [TestFixture]
    public class DataTest
    {
        private Mock<AbstractDataApi> mockData;
        private AbstractDataApi mockApi;

        [SetUp]
        public void Setup()
        {
            mockData = new Mock<AbstractDataApi>();
            mockApi = mockData.Object;
        }


        [Test]
        public void GetNumberOfBalls_ReturnsCorrectNumber()
        {
            // Arrange
            int expectedNumberOfBalls = 5;
            mockData.Setup(d => d.GetNumberOfBalls()).Returns(expectedNumberOfBalls); // Setup mock to return the expected number of balls

            // Act
            int actualNumberOfBalls = mockApi.GetNumberOfBalls(); // Call the method to get number of balls using mockApi

            // Assert
            Assert.AreEqual(expectedNumberOfBalls, actualNumberOfBalls); // Check if numbers match
        }

        [Test]
        public void SpawnBalls_AddsBalls()
        {
            // Arrange
            int initialNumberOfBalls = 0;
            int amount = 3;
            mockData.Setup(d => d.GetNumberOfBalls()).Returns(initialNumberOfBalls); // Setup mock to return initial number of balls

            // Act
            mockApi.SpawnBalls(amount); // Call the method to spawn balls using mockApi

            // Assert
            mockData.Verify(d => d.GetNumberOfBalls(), Times.Exactly(0)); 
            mockData.Verify(d => d.SpawnBalls(amount), Times.Once); 
        }
    }
}