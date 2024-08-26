namespace NumberOrderingApi.Services
{
    public interface INumberOrderingService
    {
        void SortAndSaveNumbers(int[] numbers);

        int[] GetLastSortedNumbers();
    }
}