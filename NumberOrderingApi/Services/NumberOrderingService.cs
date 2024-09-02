using System.ComponentModel.DataAnnotations;
using NumberOrderingApi.Data.Repositories;
using NumberOrderingApi.Services.Sorting;

namespace NumberOrderingApi.Services
{
    public class NumberOrderingService : INumberOrderingService
    {
        private readonly ISortPerformerService _sortPerformerService;
        private readonly INumbersRepository _numbersRepository;
        private readonly INumberValidationService _numberValidationService;
        private readonly ILogger<NumberOrderingService> _logger;
        public NumberOrderingService(ISortPerformerService sortPerformerService, INumbersRepository numbersRepository, INumberValidationService numberValidationService, ILogger<NumberOrderingService> logger)
        {
            _sortPerformerService = sortPerformerService;
            _numbersRepository = numbersRepository;
            _numberValidationService = numberValidationService;
            _logger = logger;
        }

        public async Task SortAndSaveNumbers(int[] numbers)
        {
            _numberValidationService.ValidateNumbers(numbers);
            var sortedNumbers = _sortPerformerService.Sort(numbers);
            
            await _numbersRepository.SaveResults(sortedNumbers);
        }

        public async Task<string> LoadContentOfLatestSavedFile()
        {
            var lastNumbers = await _numbersRepository.ReadLastSavedResults();
            
            return lastNumbers;
        }
    }
}
