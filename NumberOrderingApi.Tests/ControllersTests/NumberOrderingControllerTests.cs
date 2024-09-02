using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NumberOrderingApi.Controllers;
using NumberOrderingApi.Models;
using NumberOrderingApi.Services;

namespace NumberOrderingApi.Tests.ControllersTests
{
    [TestClass]
    public class NumberOrderingControllerTests
    {

        private Mock<INumberOrderingService> _mockNumberOrderingService;
        private NumberOrderingController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockNumberOrderingService = new Mock<INumberOrderingService>();
            _controller = new NumberOrderingController(_mockNumberOrderingService.Object);
        }

        private AddNumberOrderingRequest CreateRequestBody()
        {
            return new AddNumberOrderingRequest
            {
                Numbers = new[] { 3, 1, 2 }
            };
        }

        [TestMethod]
        public async Task OrderNumbers_ShouldReturnOkResponse_WhenModelWasValid()
        {
            // Arrange
            _mockNumberOrderingService.Setup(x => x.SortAndSaveNumbers(It.IsAny<int[]>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.OrderNumbers(CreateRequestBody());

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task OrderNumbers_ShouldCallSortAndSaveNumbers_WhenModelWasValid()
        {
            // Arrange
            var request = CreateRequestBody();

            // Act
            var result = await _controller.OrderNumbers(request);

            // Assert
            _mockNumberOrderingService.Verify(s => s.SortAndSaveNumbers(request.Numbers), Times.Once);
        }

        [TestMethod]
        public async Task OrderNumbers_ShouldReturnUnproccessableEntityResponse_WhenModelWasInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Numbers", "The Numbers field is required.");

            // Act
            var result = await _controller.OrderNumbers(CreateRequestBody());

            // Assert
            var unproccessableEntityResult = result as UnprocessableEntityObjectResult;
            Assert.IsNotNull(unproccessableEntityResult);
        }

        [TestMethod]
        public async Task OrderNumbers_ShouldThrowValidationException_WhenModelWasValidButNumberOrderingServiceThrowsValidationException()
        {
            // Arrange
            _mockNumberOrderingService.Setup(x => x.SortAndSaveNumbers(CreateRequestBody().Numbers))
                .Throws(new ValidationException("Validation error."));

            // Act and assert
            await Assert.ThrowsExceptionAsync<ValidationException>(async () =>
            {
                await _controller.OrderNumbers(CreateRequestBody());
            });
        }

        [TestMethod]
        public async Task OrderNumbers_ShouldThrowInternalServerException_WhenExceptionIsThrown()
        {
            // Arrange
            _mockNumberOrderingService.Setup(x => x.SortAndSaveNumbers(CreateRequestBody().Numbers))
                .Throws(new Exception("An error occurred."));

            // Act and assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await _controller.OrderNumbers(CreateRequestBody());
            });
        }

        [TestMethod]
        public async Task LoadContentOfLatestSavedFile_ShouldReturnOkResponseWithFileContent_IfFileWasFound()
        {
            // Arrange
            _mockNumberOrderingService.Setup(x => x.LoadContentOfLatestSavedFile())
                .ReturnsAsync("1 2 3");

            // Act
            var result = await _controller.LoadContentOfLatestSavedFile();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("1 2 3", okResult.Value);
        }

        [TestMethod]
        public async Task LoadContentOfLatestSavedFile_ShouldReturnNotFoundResponse_WhenEmptyStringWasReturnedByRepository()
        {
            // Arrange
            _mockNumberOrderingService.Setup(x => x.LoadContentOfLatestSavedFile())
                .ReturnsAsync(string.Empty);

            // Act
            var result = await _controller.LoadContentOfLatestSavedFile();

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
        }
    }
}
