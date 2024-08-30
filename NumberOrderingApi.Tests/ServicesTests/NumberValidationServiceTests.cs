using System.ComponentModel.DataAnnotations;
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
        [TestInitialize]
        public void Setup()
        {
            _numberValidationService = new NumberValidationService();
        }

        [TestMethod]
        public void ValidateNumbers_ShouldReturnSuccess_WhenNumbersMeetRequirements()
        {
            // Arrange
            var numbers = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            // Act
            var validationResult = _numberValidationService.ValidateNumbers(numbers);

            // Assert
            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void ValidateNumbers_ShouldReturnFailure_WhenNumbersIsNull()
        {
            // Arrange
            var numbers = Array.Empty<int>();

            // Act
            var validationResult = _numberValidationService.ValidateNumbers(numbers);

            // Assert
            Assert.AreNotEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void ValidateNumbers_ShouldReturnFailure_WhenNumbersCountIsLessThan2()
        {
            // Arrange
            var numbers = new[] { 1 };

            // Act
            var validationResult = _numberValidationService.ValidateNumbers(numbers);

            // Assert
            Assert.AreNotEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void ValidateNumbers_ShouldReturnFailure_WhenNumbersCountIsMoreThan10()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            // Act
            var validationResult = _numberValidationService.ValidateNumbers(numbers);

            // Assert
            Assert.AreNotEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void ValidateNumbers_ShouldReturnFailure_WhenNumbersAreNotDistinct()
        {
            // Arrange
            var numbers = new[] { 1, 1 };

            // Act
            var validationResult = _numberValidationService.ValidateNumbers(numbers);

            // Assert
            Assert.AreNotEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void ValidateNumbers_ShouldReturnFailure_WhenNumbersHaveValueBelow1()
        {
            // Arrange
            var numbers = new[] { 1, 0 };

            // Act
            var validationResult = _numberValidationService.ValidateNumbers(numbers);

            // Assert
            Assert.AreNotEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void ValidateNumbers_ShouldReturnFailure_WhenNumbersHaveValueAbove10()
        {
            // Arrange
            var numbers = new[] { 1, 11 };

            // Act
            var validationResult = _numberValidationService.ValidateNumbers(numbers);

            // Assert
            Assert.AreNotEqual(ValidationResult.Success, validationResult);
        }
    }
}
