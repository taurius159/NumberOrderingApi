using System.ComponentModel.DataAnnotations;
using NumberOrderingApi.Models;
using NumberOrderingApi.Tests.Helpers;

namespace NumberOrderingApi.Tests.Models
{
    [TestClass]
    public class AddNumberOrderingRequestTests
    {
        [TestMethod]
        public void Numbers_ShouldPassValidation_WhenModelIsValid()
        {
            // Arrange
            var model = new AddNumberOrderingRequest
            {
                Numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
            };

            // Act
            var results = ModelValidationHelper.ValidateModel(model);

            // Assert
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Numbers_ShouldNotPassValidation_WhenAtLeastOneNumberIsNotInRangeFrom1To10()
        {
            // Arrange
            var model = new AddNumberOrderingRequest
            {
                Numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11 }
            };

            // Act
            var results = ModelValidationHelper.ValidateModel(model);

            // Assert
            Assert.AreNotEqual(0, results.Count);
        }

        [TestMethod]
        public void Numbers_ShouldNotPassValidation_WhenMoreThan10NumbersAreProvided()
        {
            // Arrange
            var model = new AddNumberOrderingRequest
            {
                Numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 2 }
            };

            // Act
            var results = ModelValidationHelper.ValidateModel(model);

            // Assert
            Assert.AreNotEqual(0, results.Count);
        }

        [TestMethod]
        public void Numbers_ShouldNotPassValidation_WhenNumbersAreNotProvided()
        {
            // Arrange
            var model = new AddNumberOrderingRequest
            {
                Numbers = new int[] { }
            };

            // Act
            var results = ModelValidationHelper.ValidateModel(model);

            // Assert
            Assert.AreNotEqual(0, results.Count);
        }

        [TestMethod]
        public void Numbers_ShouldNotPassValidation_WhenNumbersAreNotUnique()
        {
            // Arrange
            var model = new AddNumberOrderingRequest
            {
                Numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9 }
            };

            // Act
            var results = ModelValidationHelper.ValidateModel(model);

            // Assert
            Assert.AreNotEqual(0, results.Count);
        }
    }
}
