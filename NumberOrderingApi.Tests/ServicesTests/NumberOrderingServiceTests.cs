using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NumberOrderingApi.Data.Repositories;
using NumberOrderingApi.Services;
using NumberOrderingApi.Services.Sorting;
using System;

namespace NumberOrderingApi.Tests.Services
{
    [TestClass]
    public class NumberOrderingServiceTests
    {
        private Mock<ISortingService> _mockSortingService;
        private Mock<INumbersRepository> _mockNumbersRepository;
        private NumberOrderingService _numberOrderingService;

        [TestInitialize]
        public void Setup()
        {
            _mockSortingService = new Mock<ISortingService>();
            _mockNumbersRepository = new Mock<INumbersRepository>();
            _numberOrderingService = new NumberOrderingService(_mockSortingService.Object, _mockNumbersRepository.Object);
        }

        [TestMethod]
        public void SortAndSaveNumbers_ShouldCallSortAndSaveResults()
        {
            // Arrange
            var numbers = new[] { 3, 1, 2 };
            var sortedNumbers = new[] { 1, 2, 3 };
            _mockSortingService.Setup(s => s.Sort(numbers)).Returns(sortedNumbers);

            // Act
            _numberOrderingService.SortAndSaveNumbers(numbers);

            // Assert
            _mockSortingService.Verify(s => s.Sort(numbers), Times.Once);
            _mockNumbersRepository.Verify(r => r.SaveResults(sortedNumbers), Times.Once);
        }

        [TestMethod]
        public void GetLastSortedNumbers_ShouldReturnLastSavedResults()
        {
            // Arrange
            var lastSavedNumbers = new[] { 1, 2, 3 };
            _mockNumbersRepository.Setup(r => r.ReadLastSavedResults()).Returns(lastSavedNumbers);

            // Act
            var result = _numberOrderingService.GetLastSortedNumbers();

            // Assert
            Assert.AreEqual(lastSavedNumbers, result);
            _mockNumbersRepository.Verify(r => r.ReadLastSavedResults(), Times.Once);
        }

        [TestMethod]
        public void SortAndSaveNumbers_ShouldHandleNullInput()
        {
            // Arrange
            int[] numbers = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _numberOrderingService.SortAndSaveNumbers(numbers));
        }

        [TestMethod]
        public void SortAndSaveNumbers_ShouldHandleEmptyArray()
        {
            // Arrange
            var numbers = new int[] { };
            var sortedNumbers = new int[] { };
            _mockSortingService.Setup(s => s.Sort(numbers)).Returns(sortedNumbers);

            // Act
            _numberOrderingService.SortAndSaveNumbers(numbers);

            // Assert
            _mockSortingService.Verify(s => s.Sort(numbers), Times.Once);
            _mockNumbersRepository.Verify(r => r.SaveResults(sortedNumbers), Times.Once);
        }
    }
}
