using System.ComponentModel.DataAnnotations;

namespace NumberOrderingApi.Services
{
    public interface INumberOrderingService
    {
        Task<ValidationResult> SortAndSaveNumbers(int[] numbers);

        Task<string> LoadContentOfLatestSavedFile();
    }
}