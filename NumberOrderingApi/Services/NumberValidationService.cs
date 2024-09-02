using System.ComponentModel.DataAnnotations;

namespace NumberOrderingApi.Services
{
    public class NumberValidationService : INumberValidationService
    {
        public void ValidateNumbers(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                throw new ValidationException("No numbers provided.");
            }

            if (numbers.Length > 10 || numbers.Length < 2)
            {
                throw new ValidationException("There can be at minimum 2, and maximum 10 numbers");
            }

            if(numbers.Length != numbers.Distinct().Count())
            {
                throw new ValidationException("Numbers must be unique.");
            }

            if (numbers.Any(n => n < 1 || n > 10))
            {
                throw new ValidationException("Numbers must be between 1 and 10 (both inclusive).");
            }
        }
    }
}