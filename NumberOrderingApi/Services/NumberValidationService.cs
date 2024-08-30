using System.ComponentModel.DataAnnotations;

namespace NumberOrderingApi.Services
{
    public class NumberValidationService : INumberValidationService
    {
        public ValidationResult ValidateNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                return new ValidationResult("No numbers provided.");
            }

            if (numbers.Length > 10 || numbers.Length < 2)
            {
                return new ValidationResult("There can be at minimum 2, and maximum 10 numbers");
            }

            if(numbers.Length != numbers.Distinct().Count())
            {
                return new ValidationResult("Numbers must be unique.");
            }

            if (numbers.Any(n => n < 1 || n > 10))
            {
                return new ValidationResult("Numbers must be between 1 and 10 (both inclusive).");
            }

            return ValidationResult.Success;
        }
    }
}