using NumberOrderingApi.Services.Sorting;

namespace NumberOrderingApi.Services
{
    public class NumberOrderingService : INumberOrderingService
    {
        private readonly ISortingService _sortingService;
        private readonly string _filePath = "result.txt";

        public NumberOrderingService(ISortingService sortingService)
        {
            _sortingService = sortingService;
        }

        public async void SaveSortedNumber(int[] numbers)
        {
            var sortedNumbers = _sortingService.Sort(numbers);

            await System.IO.File.WriteAllTextAsync(_filePath, string.Join(" ", sortedNumbers));
        }
    }
}