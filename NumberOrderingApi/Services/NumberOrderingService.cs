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

        public void SortAndSaveNumbers(int[] numbers)
        {
            _numbersRepository.SaveResults(_sortingService.Sort(numbers));
        }

        public int[] GetLastSortedNumbers()
        {
            return _numbersRepository.ReadLastSavedFile();
        }
    }
}
