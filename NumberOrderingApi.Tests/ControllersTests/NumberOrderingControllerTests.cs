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
            _mockNumberOrderingService.Setup(x => x.SortAndSaveNumbers(CreateRequestBody().Numbers))
                .ReturnsAsync(ValidationResult.Success);

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
            _mockNumberOrderingService
                .Setup(s => s.SortAndSaveNumbers(request.Numbers))
                .ReturnsAsync(ValidationResult.Success);

            // Act
            var result = await _controller.OrderNumbers(request);

            // Assert
            _mockNumberOrderingService.Verify(s => s.SortAndSaveNumbers(request.Numbers), Times.Once);
        }

        [TestMethod]
        public async Task OrderNumbers_ShouldReturnUnprocessableEntityResponse_WhenModelWasInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Numbers", "The Numbers field is required.");

            // Act
            var result = await _controller.OrderNumbers(CreateRequestBody());

            // Assert
            var unprocessableEntityResult = result as UnprocessableEntityObjectResult;
            Assert.IsNotNull(unprocessableEntityResult);
            Assert.AreEqual(422, unprocessableEntityResult.StatusCode);
        }

        [TestMethod]
        public async Task OrderNumbers_ShouldReturnUnprocessableEntityResponse_WhenModelWasValidButNumberOrderingServiceReturnsFailedValidation()
        {
            // Arrange
            _mockNumberOrderingService.Setup(x => x.SortAndSaveNumbers(CreateRequestBody().Numbers))
                .ReturnsAsync(new ValidationResult("Numbers validation failed for some reason."));

            // Act
            var result = await _controller.OrderNumbers(CreateRequestBody());

            // Assert
            var unprocessableEntityResult = result as UnprocessableEntityObjectResult;
            Assert.IsNotNull(unprocessableEntityResult);
            Assert.AreEqual(422, unprocessableEntityResult.StatusCode);
        }

        [TestMethod]
        public async Task OrderNumbers_ShouldReturnServerError_WhenExceptionIsThrown()
        {
            // Arrange
            _mockNumberOrderingService.Setup(x => x.SortAndSaveNumbers(CreateRequestBody().Numbers))
                .ThrowsAsync(new Exception("An error occurred."));

            // Act
            var result = await _controller.OrderNumbers(CreateRequestBody());

            // Assert
            var serverErrorResult = result as ObjectResult;
            Assert.IsNotNull(serverErrorResult);
            Assert.AreEqual(500, serverErrorResult.StatusCode);
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
