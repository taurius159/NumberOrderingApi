using System.ComponentModel.DataAnnotations;

namespace NumberOrderingApi.Services
{
    public class NumberValidationService : INumberValidationService
    {
        private readonly ILogger<NumberValidationService> _logger;
        public NumberValidationService(ILogger<NumberValidationService> logger)
        {
            _logger = logger;
        }

        public void ValidateNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                var message = "No numbers provided.";
                _logger.LogError($"NumberValidationService threw exception with message: {message}.");

                throw new ValidationException($"NumberValidationService error: {message}");
            }

            if (numbers.Length > 10 || numbers.Length < 2)
            {
                var message = "There can be at minimum 2, and maximum 10 numbers.";
                _logger.LogError($"NumberValidationService threw exception with message: {message}.");
                
                throw new ValidationException($"NumberValidationService error: {message}");
            }

            if(numbers.Length != numbers.Distinct().Count())
            {
                var message = "Numbers must be unique.";
                _logger.LogError($"NumberValidationService threw exception with message: {message}.");
                
                throw new ValidationException($"NumberValidationService error: {message}");
            }

            if (numbers.Any(n => n < 1 || n > 10))
            {
                var message = "Numbers must be between 1 and 10 (both inclusive).";
                _logger.LogError($"NumberValidationService threw exception with message: {message}.");
                
                throw new ValidationException($"NumberValidationService error: {message}");
            }
        }
    }
}
