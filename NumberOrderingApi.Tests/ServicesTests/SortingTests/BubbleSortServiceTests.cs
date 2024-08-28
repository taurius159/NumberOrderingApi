using NumberOrderingApi.Services.Sorting;
namespace NumberOrderingApi.Tests.ServicesTests.SortingTests
{
    [TestClass]
    public class BubbleSortServiceTests
    {
        private ISortingService _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new BubbleSortService();
        }

        [TestMethod]
        public void Sort_SortsNumbersCorrectly()
        {
            // Arrange
            int[] numbers = new int[] { 5, 3, 8, 1, 2, 9, 4, 7, 6 };
            int[] expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Act
            int[] actual = _service.Sort(numbers);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_HandlesEmptyArray()
        {
            // Arrange
            int[] numbers = new int[] { };
            int[] expected = new int[] { };

            // Act
            int[] actual = _service.Sort(numbers);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_HandlesSingleElementArray()
        {
            // Arrange
            int[] numbers = new int[] { 5 };
            int[] expected = new int[] { 5 };

            // Act
            int[] actual = _service.Sort(numbers);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
