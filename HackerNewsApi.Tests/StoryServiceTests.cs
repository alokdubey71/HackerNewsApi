using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HackerNewsApi.Tests
{
    [TestClass]
    public class StoryServiceTests
    {
        [TestMethod]
        public async Task GetNewestStories_ReturnsCorrectNumberOfStories()
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync("[1, 2, 3, 4, 5]"); // Mock response from the API

            var storyService = new StoryService(httpClientMock.Object);

            // Act
            var result = await storyService.GetNewestStories(3, 1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }


        [TestMethod]
        public async Task GetNewestStories_HandlesEmptyResponse()
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync("[]"); // Mock empty response from the API

            var storyService = new StoryService(httpClientMock.Object);

            // Act
            var result = await storyService.GetNewestStories(3, 1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task SearchStories_ReturnsEmptyListForNoResults()
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync("[]"); // Mock empty response from the API

            var storyService = new StoryService(httpClientMock.Object);

            // Act
            var result = await storyService.SearchStories("Nonexistent");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetNewestStories_HandlesHttpClientException()
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ThrowsAsync(new HttpRequestException("Simulated exception")); // Simulate an exception from HttpClient

            var storyService = new StoryService(httpClientMock.Object);

            // Act & Assert
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await storyService.GetNewestStories(3, 1));
        }

        [TestMethod]
        public void SearchStories_HandlesInvalidJsonResponse()
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .ReturnsAsync("Invalid JSON"); // Mock an invalid JSON response from the API

            var storyService = new StoryService(httpClientMock.Object);

            // Act & Assert
            Assert.ThrowsExceptionAsync<Exception>(async () => await storyService.SearchStories("Invalid"));
        }
    }
}
