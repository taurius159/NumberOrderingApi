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
        public void Sort_ShouldReturnSortedArray_WhenArrayIsUnsorted()
        {
            // Arrange
            int[] numbers = [5, 3, 8];
            int[] expected = [3, 5, 8];

            // Act
            int[] actual = _service.Sort(numbers);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_ShouldReturnSortedArray_WhenArrayHasLargeValues()
        {
            // Arrange
            int[] numbers = [int.MaxValue, int.MinValue, 0];
            int[] expected = [int.MinValue, 0, int.MaxValue];

            // Act
            int[] actual = _service.Sort(numbers);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_ShouldReturnEmptyArray_WhenArrayIsEmpty()
        {
            // Arrange
            int[] numbers = [];
            int[] expected = [];

            // Act
            int[] actual = _service.Sort(numbers);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_ShouldReturnSameArray_WhenArrayHasSingleElement()
        {
            // Arrange
            int[] numbers = [5];
            int[] expected = [5];

            // Act
            int[] actual = _service.Sort(numbers);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_ShouldReturnSameArray_WhenArrayIsAlreadySorted()
        {
            // Arrange
            int[] numbers = [1, 2, 3];
            int[] expected = [1, 2, 3];

            // Act
            int[] actual = _service.Sort(numbers);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_ShouldReturnSortedArray_WhenArrayIsReverseSorted()
        {
            // Arrange
            int[] numbers = [9, 8, 7];
            int[] expected = [7, 8, 9];

            // Act
            int[] actual = _service.Sort(numbers);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Sort_ShouldReturnSortedArray_WhenArrayHasDuplicates()
        {
            // Arrange
            int[] numbers = [5, 7, 5];
            int[] expected = [5, 5, 7];

            // Act
            int[] actual = _service.Sort(numbers);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
