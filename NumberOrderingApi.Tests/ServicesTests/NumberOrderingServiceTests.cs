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
        private Mock<NumberValidationService> _numberValidationService;
        private NumberOrderingService _numberOrderingService;

        [TestInitialize]
        public void Setup()
        {
            _mockSortingService = new Mock<ISortingService>();
            _mockNumbersRepository = new Mock<INumbersRepository>();
            _numberValidationService = new Mock<NumberValidationService>();
            _numberOrderingService = new NumberOrderingService(_mockSortingService.Object, _mockNumbersRepository.Object, _numberValidationService.Object);
        }

        [TestMethod]
        public async void SortAndSaveNumbers_ShouldCallSortAndSaveResults()
        {
            // Arrange
            var numbers = new[] { 3, 1, 2 };
            var sortedNumbers = new[] { 1, 2, 3 };
            _mockSortingService.Setup(s => s.Sort(numbers)).Returns(sortedNumbers);

            // Act
            await _numberOrderingService.SortAndSaveNumbers(numbers);

            // Assert
            _mockSortingService.Verify(s => s.Sort(numbers), Times.Once);
            _mockNumbersRepository.Verify(r => r.SaveResults(sortedNumbers), Times.Once);
        }

        // [TestMethod]
        // public async void GetLastSortedNumbers_ShouldReturnLastSavedResults()
        // {
        //     // Arrange
        //     var lastSavedNumbers = new[] { 1, 2, 3 };
        //     _mockNumbersRepository.Setup(r => r.ReadLastSavedResults()).ReturnsAsync(lastSavedNumbers);

        //     // Act
        //     var result = await _numberOrderingService.GetLastSortedNumbers();

        //     // Assert
        //     Assert.AreEqual(lastSavedNumbers, result);
        //     _mockNumbersRepository.Verify(r => r.ReadLastSavedResults(), Times.Once);
        // }
    }
}
