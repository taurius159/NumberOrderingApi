namespace NumberOrderingApi.Services
{
    public interface INumberOrderingService
    {
        Task SortAndSaveNumbers(int[] numbers);

        Task<int[]> GetLastSortedNumbers();
    }
}