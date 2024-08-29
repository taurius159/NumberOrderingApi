using System.ComponentModel.DataAnnotations;
using NumberOrderingApi.Models;
using NumberOrderingApi.Tests.Helpers;

namespace NumberOrderingApi.Tests.Models
{
    [TestClass]
    public class AddNumberOrderingRequestTests
    {
        [TestMethod]
        public void Numbers_ShouldPassValidation_WhenRequirementsAreMet()
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
            var modelMoreThan10 = new AddNumberOrderingRequest
            {
                Numbers = new int[] { 1, 11 }
            };
            var modelLessThan2 = new AddNumberOrderingRequest
            {
                Numbers = new int[] {1}
            };

            // Act
            var resultsMoreThan10 = ModelValidationHelper.ValidateModel(modelMoreThan10);
            var resultsLessThan2 = ModelValidationHelper.ValidateModel(modelLessThan2);

            // Assert
            Assert.AreNotEqual(0, resultsMoreThan10.Count);
            Assert.AreNotEqual(0, resultsLessThan2.Count);
        }

        [TestMethod]
        public void Numbers_ShouldNotPassValidation_WhenLessThan2ArePassed()
        {
            // Arrange
            var modelLessThan2  = new AddNumberOrderingRequest
            {
                Numbers = new int[] {1}
            };
            // Act
            var result = ModelValidationHelper.ValidateModel(modelLessThan2);

            // Assert
            Assert.AreNotEqual(0, result.Count);
        }

        [TestMethod]
        public void Numbers_ShouldNotPassValidation_WhenMoreThan10ArePassed()
        {
            // Arrange
            var modelMoreThan10 = new AddNumberOrderingRequest
            {
                Numbers = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11}
            };
            // Act
            var result = ModelValidationHelper.ValidateModel(modelMoreThan10);

            // Assert
            Assert.AreNotEqual(0, result.Count);
        }

        [TestMethod]
        public void Numbers_ShouldNotPassValidation_WhenNumbersAreNotUnique()
        {
            // Arrange
            var model = new AddNumberOrderingRequest
            {
                Numbers = new int[] { 1, 2, 2 }
            };

            // Act
            var results = ModelValidationHelper.ValidateModel(model);

            // Assert
            Assert.AreNotEqual(0, results.Count);
        }
    }
}
