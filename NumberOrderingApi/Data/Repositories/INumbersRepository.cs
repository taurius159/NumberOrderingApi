namespace NumberOrderingApi.Data.Repositories
{
    public interface INumbersRepository
    {
        public Task SaveResults(int[] content);
        public Task<string> ReadLastSavedResults();
    }
}