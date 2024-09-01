using Microsoft.Extensions.Logging;
using Moq;
using NumberOrderingApi.Services;
using NumberOrderingApi.Services.Sorting;

namespace NumberOrderingApi.Tests.Services
{
    [TestClass]
    public class SortPerformerServiceTests
    {
        private Mock<ILogger<SortPerformerService>> _mockLogger;
        private List<Mock<ISortingService>> _mockSortingServices;
        private SortPerformerService _sortPerformerService;

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<SortPerformerService>>();
            _mockSortingServices = new List<Mock<ISortingService>>
            {
                new Mock<ISortingService>(),
                new Mock<ISortingService>(),
                new Mock<ISortingService>()
            };

            var sortingServices = _mockSortingServices.Select(m => m.Object).ToList();
            _sortPerformerService = new SortPerformerService(sortingServices, _mockLogger.Object);
        }

        [TestMethod]
        public void Sort_ShouldReturnSortedNumbers_FromFirstDeclaredSortingService()
        {
            // Arrange
            var numbers = new[] { 3, 1, 2 };
            var sortedNumbers = new[] { 1, 2, 3 };
            _mockSortingServices[0].Setup(s => s.Sort(numbers)).Returns(sortedNumbers);

            // Act
            var result = _sortPerformerService.Sort(numbers);

            // Assert
            CollectionAssert.AreEqual(sortedNumbers, result);
            _mockSortingServices[0].Verify(s => s.Sort(numbers), Times.Once);
        }

        [TestMethod]
        public void Sort_ShouldLogExecutionTime_ForEachSortingService()
        {
            // Arrange
            var numbers = new[] { 3, 1, 2 };
            var sortedNumbers = new[] { 1, 2, 3 };
            foreach (var mockSortingService in _mockSortingServices)
            {
                mockSortingService.Setup(s => s.Sort(numbers)).Returns(sortedNumbers);
            }

            // Act
            _sortPerformerService.Sort(numbers);

            // Assert
            foreach (var mockSortingService in _mockSortingServices)
            {
                mockSortingService.Verify(s => s.Sort(numbers), Times.Once);
            }

            // Verify that the logger was called for each sorting service
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("took")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Exactly(_mockSortingServices.Count));
        }
    }
}
