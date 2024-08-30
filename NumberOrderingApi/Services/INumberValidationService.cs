using System.ComponentModel.DataAnnotations;

namespace NumberOrderingApi.Services
{
    public interface INumberValidationService
    {
        ValidationResult ValidateNumbers(int[] numbers);
    }
}