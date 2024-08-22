using NumeriuUzsakymasApi.Services;
namespace NumeriuUzsakymasApi.Tests.Services
{
    [TestClass]
    public class SortingServiceTests
    {
        private ISortingService _sortingService;

        [TestInitialize]
        public void Initialize()
        {
            _sortingService = new SortingService();
        }

        [TestMethod]
        public void BubbleSort_SortsNumbersInAscendingOrder()
        {
            // Arrange
            int[] numbers = new int[] { 5, 3, 8, 1, 2, 9, 4, 7, 6 };

            // Act
            int[] sortedNumbers = _sortingService.BubbleSort(numbers);

            // Assert
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, sortedNumbers);
        }
    }
}
