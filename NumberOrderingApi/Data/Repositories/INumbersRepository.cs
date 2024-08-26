namespace NumberOrderingApi.Data.Repositories
{
    public interface INumbersRepository
    {
        public void SaveResults(int[] content);
        public int[] ReadLastSavedFile();
    }
}