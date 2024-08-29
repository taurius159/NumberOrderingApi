using NumberOrderingApi.Data.Repositories;
using NumberOrderingApi.Services.Sorting;

namespace NumberOrderingApi.Services
{
    public class NumberOrderingService : INumberOrderingService
    {
        private readonly ISortingService _sortingService;
        private readonly INumbersRepository _numbersRepository;
        public NumberOrderingService(ISortingService sortingService, INumbersRepository numbersRepository)
        {
            _sortingService = sortingService;
            _numbersRepository = numbersRepository;
        }

        public async Task SortAndSaveNumbers(int[] numbers)
        {
            if(numbers == null || numbers.Length == 0)
            {
                throw new ArgumentNullException(nameof(numbers));
            }
            
            await _numbersRepository.SaveResults(_sortingService.Sort(numbers));
        }

        public async Task<int[]> GetLastSortedNumbers()
        {
            var lastNumbers = await _numbersRepository.ReadLastSavedResults();
            return lastNumbers;
        }
    }
}
