using System.ComponentModel.DataAnnotations;

namespace NumberOrderingApi.Services
{
    public interface INumberValidationService
    {
        void ValidateNumbers(int[] numbers);
    }
}