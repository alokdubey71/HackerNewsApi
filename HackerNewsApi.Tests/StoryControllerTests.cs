using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNewsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HackerNewsApi.Tests
{
    [TestClass]
    public class StoryControllerTests
    {
        [TestMethod]
        public async Task GetNewestStories_ReturnsOkResult()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            storyServiceMock.Setup(x => x.GetNewestStories(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Story>());

            var controller = new StoryController(storyServiceMock.Object);

            // Act
            var result = await controller.GetNewestStories();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task SearchStories_ReturnsOkResult()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            storyServiceMock.Setup(x => x.SearchStories(It.IsAny<string>()))
                .ReturnsAsync(new List<Story>());

            var controller = new StoryController(storyServiceMock.Object);

            // Act
            var result = await controller.SearchStories("Sample");

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
         
        [TestMethod]
        public async Task GetNewestStories_ReturnsNotFoundResultWhenServiceReturnsNull()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            storyServiceMock.Setup(x => x.GetNewestStories(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((IEnumerable<Story>)null);

            var controller = new StoryController(storyServiceMock.Object);

            // Act
            var result = await controller.GetNewestStories();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        
        //I can use more test scenari's 
        // Thank you --ALOK

    }
}
