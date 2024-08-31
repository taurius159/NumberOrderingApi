using NumberOrderingApi.Services.Sorting;
namespace NumberOrderingApi.Tests.ServicesTests.SortingTests
{
    [TestClass]
    public class BubbleSortServiceTests : SortServiceTestsBase
    {
        protected override ISortingService _service => new BubbleSortService();
    }
}
