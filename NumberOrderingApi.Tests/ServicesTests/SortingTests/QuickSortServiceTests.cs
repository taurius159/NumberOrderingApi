using NumberOrderingApi.Services.Sorting;
namespace NumberOrderingApi.Tests.ServicesTests.SortingTests
{
    [TestClass]
    public class QuickSortServiceTests : SortServiceTestsBase
    {
        protected override ISortingService _service => new QuickSortService();
    }
}
