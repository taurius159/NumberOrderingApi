using System.ComponentModel.DataAnnotations;
using Moq;
using NumberOrderingApi.Data.Repositories;
using NumberOrderingApi.Services;
using NumberOrderingApi.Services.Sorting;

namespace NumberOrderingApi.Tests.Services
{
    [TestClass]
    public class NumberOrderingServiceTests
    {
        private Mock<ISortingService> _mockSortingService;
        private Mock<INumbersRepository> _mockNumbersRepository;
        private Mock<INumberValidationService> _numberValidationService;
        private NumberOrderingService _numberOrderingService;

        [TestInitialize]
        public void Setup()
        {
            _mockSortingService = new Mock<ISortingService>();
            _mockNumbersRepository = new Mock<INumbersRepository>();
            _numberValidationService = new Mock<INumberValidationService>();
            _numberOrderingService = new NumberOrderingService(_mockSortingService.Object, _mockNumbersRepository.Object, _numberValidationService.Object);
        }

        [TestMethod]
        public async Task SortAndSaveNumbers_ShouldExecuteSortAndSaveFunctionality_WhenValidationIsUnsuccessful()
        {
            // Arrange
            var numbers = new[] { 3, 1, 2 };
            _numberValidationService.Setup(v => v.ValidateNumbers(numbers)).Returns(ValidationResult.Success);

            // Act
            await _numberOrderingService.SortAndSaveNumbers(numbers);

            // Assert
            _mockSortingService.Verify(s => s.Sort(numbers), Times.Once);
            _mockNumbersRepository.Verify(r => r.SaveResults(It.IsAny<int[]>()), Times.Once);
        }

        [TestMethod]
        public async Task SortAndSaveNumbers_ShouldNotExecuteSortAndSaveFunctionality_WhenValidationIsUnsuccessful()
        {
            // Arrange
            var numbers = new[] { 3, 1, 2 };
            _numberValidationService.Setup(v => v.ValidateNumbers(numbers)).Returns(new ValidationResult("Validation failed"));

            // Act
            await _numberOrderingService.SortAndSaveNumbers(numbers);

            // Assert
            _mockSortingService.Verify(s => s.Sort(numbers), Times.Never);
            _mockNumbersRepository.Verify(r => r.SaveResults(It.IsAny<int[]>()), Times.Never);
        }

        [TestMethod]
        public async Task LoadContentOfLatestSavedFile_ShouldCallNumbersRepository_WhenInvoked()
        {
            // Act
            await _numberOrderingService.LoadContentOfLatestSavedFile();

            // Assert
            _mockNumbersRepository.Verify(r => r.ReadLastSavedResults(), Times.Once);
        }

        [TestMethod]
        public async Task LoadContentOfLatestSavedFile_ShouldReturnWhatNumberRepositoryReturns()
        {
            // Arrange
            var expectedContent = "1, 2, 3";
            _mockNumbersRepository.Setup(r => r.ReadLastSavedResults()).ReturnsAsync(expectedContent);

            // Act
            var actualContent = await _numberOrderingService.LoadContentOfLatestSavedFile();

            // Assert
            Assert.AreEqual(expectedContent, actualContent);
        }
    }
}
