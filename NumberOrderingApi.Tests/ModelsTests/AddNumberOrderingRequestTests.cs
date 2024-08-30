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
        public void Numbers_ShouldNotPassValidation_WhenAtLeastOneNumberValueIsNotInRangeFrom1To10()
        {
            // Arrange
            var modelWithValueMoreThan10 = new AddNumberOrderingRequest
            {
                Numbers = new int[] { 11, 2, 3 }
            };
            var modelWithValueLessThan1 = new AddNumberOrderingRequest
            {
                Numbers = new int[] {2, 3, 0}
            };

            // Act
            var resultsModelWithValueMoreThan10 = ModelValidationHelper.ValidateModel(modelWithValueMoreThan10);
            var resultsModelWithValueLessThan1 = ModelValidationHelper.ValidateModel(modelWithValueLessThan1);

            // Assert
            Assert.AreNotEqual(0, resultsModelWithValueMoreThan10.Count);
            Assert.AreNotEqual(0, resultsModelWithValueLessThan1.Count);
        }

        [TestMethod]
        public void Numbers_ShouldNotPassValidation_WhenLessThan2ValuesArePassed()
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
        public void Numbers_ShouldNotPassValidation_WhenMoreThan10ValuesArePassed()
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
