using Moq;
using NumberOrderingApi.Services;
using NumberOrderingApi.Services.Sorting;
namespace NumberOrderingApi.Tests.Services
{
    [TestClass]
    public class NumberOrderingServiceTests
    {
        private NumberOrderingService _service;
        private Mock<ISortingService> _sortingServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _sortingServiceMock = new Mock<ISortingService>();
            _service = new NumberOrderingService(_sortingServiceMock.Object);
        }

        [TestMethod]
        public void SaveSortedNumber_SavesSortedNumbers()
        {
            // Arrange
            int[] numbers = new int[] { 5, 3, 8, 1, 2, 9, 4, 7, 6 };
            int[] sortedNumbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            _sortingServiceMock.Setup(x => x.Sort(numbers)).Returns(sortedNumbers);

            // Act
            _service.SaveSortedNumber(numbers);

            // Assert
            _sortingServiceMock.Verify(x => x.Sort(numbers), Times.Once);
            string expected = "1 2 3 4 5 6 7 8 9";
            string actual = System.IO.File.ReadAllText("result.txt");
            Assert.AreEqual(expected, actual);
        }
    }
}