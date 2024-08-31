using NumberOrderingApi.Services.Sorting;
namespace NumberOrderingApi.Tests.ServicesTests.SortingTests
{
    [TestClass]
    public class MergeSortServiceTests : SortServiceTestsBase
    {
        protected override ISortingService _service => new MergeSortService();
    }
}
