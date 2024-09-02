using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Moq;
using NumberOrderingApi.Data.Repositories;
using NumberOrderingApi.Services;
using NumberOrderingApi.Services.Sorting;

namespace NumberOrderingApi.Tests.Services
{
    [TestClass]
    public class NumberValidationServiceTests
    {
        private NumberValidationService _numberValidationService;
        private Mock<ILogger<NumberValidationService>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<NumberValidationService>>();
            _numberValidationService = new NumberValidationService(_mockLogger.Object);
        }

        [TestMethod]
        public void ValidateNumbers_ShouldNotThrowException_WhenNumbersMeetRequirements()
        {
            // Arrange
            var numbers = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            // Act
            Action act = () => _numberValidationService.ValidateNumbers(numbers);

            // Assert that exception was not thrown
            act();
        }

        [TestMethod]
        public void ValidateNumbers_ShouldThrowValidationException_WhenNumbersIsNull()
        {
            // Arrange
            var numbers = Array.Empty<int>();

            // Act and assert
            Assert.ThrowsException<ValidationException>(() => _numberValidationService.ValidateNumbers(numbers));
        }

        [TestMethod]
        public void ValidateNumbers_ShouldThrowValidationException_WhenNumbersCountIsLessThan2()
        {
            // Arrange
            var numbers = new[] { 1 };

            // Act and assert
            Assert.ThrowsException<ValidationException>(() => _numberValidationService.ValidateNumbers(numbers));

        }

        [TestMethod]
        public void ValidateNumbers_ShouldThrowValidationException_WhenNumbersCountIsMoreThan10()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            // Act and assert
            Assert.ThrowsException<ValidationException>(() => _numberValidationService.ValidateNumbers(numbers));
        }

        [TestMethod]
        public void ValidateNumbers_ShouldThrowValidationException_WhenNumbersAreNotDistinct()
        {
            // Arrange
            var numbers = new[] { 1, 1 };

            // Act and assert
            Assert.ThrowsException<ValidationException>(() => _numberValidationService.ValidateNumbers(numbers));
        }

        [TestMethod]
        public void ValidateNumbers_ShouldThrowValidationException_WhenNumbersHaveValueBelow1()
        {
            // Arrange
            var numbers = new[] { 1, 0 };

            // Act and assert
            Assert.ThrowsException<ValidationException>(() => _numberValidationService.ValidateNumbers(numbers));
        }

        [TestMethod]
        public void ValidateNumbers_ShouldThrowValidationException_WhenNumbersHaveValueAbove10()
        {
            // Arrange
            var numbers = new[] { 1, 11 };

            // Act and assert
            Assert.ThrowsException<ValidationException>(() => _numberValidationService.ValidateNumbers(numbers));
        }
    }
}
