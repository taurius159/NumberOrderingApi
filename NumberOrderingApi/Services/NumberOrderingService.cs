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
        public NumberOrderingService(ISortPerformerService sortPerformerService, INumbersRepository numbersRepository, INumberValidationService numberValidationService)
        {
            _sortPerformerService = sortPerformerService;
            _numbersRepository = numbersRepository;
            _numberValidationService = numberValidationService;
        }

        public async Task<ValidationResult> SortAndSaveNumbers(int[] numbers)
        {          
            var validationResult = _numberValidationService.ValidateNumbers(numbers);

            if (validationResult == ValidationResult.Success)
            {
                var sortedNumbers = _sortPerformerService.Sort(numbers);
                await _numbersRepository.SaveResults(sortedNumbers);
            }

            return validationResult;
        }

        public async Task<string> LoadContentOfLatestSavedFile()
        {
            var lastNumbers = await _numbersRepository.ReadLastSavedResults();
            
            return lastNumbers;
        }
    }
}
