using System.ComponentModel.DataAnnotations;

namespace NumberOrderingApi.Services
{
    public interface INumberOrderingService
    {
        Task SortAndSaveNumbers(int[] numbers);

        Task<string> LoadContentOfLatestSavedFile();
    }
}