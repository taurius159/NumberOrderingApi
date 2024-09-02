using System.ComponentModel.DataAnnotations;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using NumberOrderingApi.Data.Repositories;
using NumberOrderingApi.Services;

namespace NumberOrderingApi.Tests.Services
{
    [TestClass]
    public class NumberOrderingServiceTests
    {
        private Mock<ISortPerformerService> _mockSortPerformerService;
        private Mock<INumbersRepository> _mockNumbersRepository;
        private Mock<INumberValidationService> _numberValidationService;
        private Mock<ILogger<NumberOrderingService>> _logger;
        private NumberOrderingService _numberOrderingService;

        [TestInitialize]
        public void Setup()
        {
            _mockSortPerformerService = new Mock<ISortPerformerService>();
            _mockNumbersRepository = new Mock<INumbersRepository>();
            _numberValidationService = new Mock<INumberValidationService>();
            _logger = new Mock<ILogger<NumberOrderingService>>();
            _numberOrderingService = new NumberOrderingService(_mockSortPerformerService.Object, _mockNumbersRepository.Object, _numberValidationService.Object, _logger.Object);
        }

        [TestMethod]
        public async Task SortAndSaveNumbers_ShouldExecuteSortAndSaveFunctionality_WhenValidationDoesNotThrowException()
        {
            // Arrange
            var numbers = new[] { 3, 1, 2 };

            // Act
            await _numberOrderingService.SortAndSaveNumbers(numbers);

            // Assert
            _mockSortPerformerService.Verify(s => s.Sort(numbers), Times.Once);
            _mockNumbersRepository.Verify(r => r.SaveResults(It.IsAny<int[]>()), Times.Once);
        }

        [TestMethod]
        public async Task SortAndSaveNumbers_ShouldNotExecuteSortAndSaveFunctionality_WhenValidationThrowsException()
        {
            // Arrange
            var numbers = new[] { 3, 1, 2 };
            _numberValidationService.Setup(v => v.ValidateNumbers(numbers)).Throws(new ValidationException());

            // Act and assert
            await Assert.ThrowsExceptionAsync<ValidationException>(async () =>
            {
                await _numberOrderingService.SortAndSaveNumbers(numbers);
            });

            _mockSortPerformerService.Verify(s => s.Sort(numbers), Times.Never);
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
