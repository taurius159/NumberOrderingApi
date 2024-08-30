namespace NumberOrderingApi.Data.Repositories
{
    public class TxtNumbersRepository : INumbersRepository
    {
        private readonly string _fileDirectory;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        
        public TxtNumbersRepository(string fileDirectory)
        {
            _fileDirectory = fileDirectory;
        }

        public async Task SaveResults(int[] numbers)
        {
            if (numbers.Length == 0)
            {
                return;
            }
            
            await _semaphore.WaitAsync();
            try
            {
                EnsureDirectoryExists(_fileDirectory);

                var filePath = CreateUniqueFilePathWithTimestamp(_fileDirectory);

                var content = CreateFileContentFromIntArray(numbers);

                await File.WriteAllTextAsync(filePath, content);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<string> ReadLastSavedResults()
        {
            if (DirectoryDoesNotExistOrIsEmpty())
            {
                return string.Empty;
            }

            var fileContent = await GetTextFromLatestFile();

            return fileContent;
        }
        
        private string CreateUniqueFilePathWithTimestamp(string fileDirectory)
        {
            return Path.Combine(fileDirectory, $"{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid()}.txt");
        }

        private string CreateFileContentFromIntArray(int[] numbers)
        {
            return string.Join(" ", numbers);
        }

        private void EnsureDirectoryExists(string fileDirectory)
        {
            if(!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
        }

        private async Task<string> GetTextFromLatestFile()
        {
            var lastFile = Directory.GetFiles(_fileDirectory).OrderByDescending(f => new FileInfo(f).CreationTime).First();
            var fileContent = await File.ReadAllTextAsync(lastFile);

            return fileContent;
        }

        private bool DirectoryDoesNotExistOrIsEmpty()
        {
            return !Directory.Exists(_fileDirectory) || !Directory.EnumerateFiles(_fileDirectory).Any();
        }
    }
}
