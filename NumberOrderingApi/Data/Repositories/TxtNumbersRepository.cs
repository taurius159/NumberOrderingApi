namespace NumberOrderingApi.Data.Repositories
{
    public class TxtNumbersRepository : INumbersRepository
    {
        private readonly string _fileDirectory;
        
        public TxtNumbersRepository(string fileDirectory)
        {
            _fileDirectory = fileDirectory;
        }

        public async Task SaveResults(int[] numbers)
        {
            EnsureDirectoryExists(_fileDirectory);

            var filePath = CreateFilePathWithTimestamp(_fileDirectory);

            var content = CreateFileContentFromIntArray(numbers);

            await WriteToFileAsync(filePath, content);
        }

        public async Task<int[]> ReadLastSavedResults()
        {
            if (DirectoryDoesNotExistOrIsEmpty())
            {
                return [];
            }

            var fileContent = await GetTextFromLatestFile();

            return ParseTextToNumbers(fileContent);
        }
        
        private string CreateFilePathWithTimestamp(string fileDirectory)
        {
            return Path.Combine(fileDirectory, $"{DateTime.Now:yyyyMMddHHmmssfff}.txt");
        }

        private string CreateFileContentFromIntArray(int[] numbers)
        {
            return string.Join(" ", numbers);
        }

        private async Task WriteToFileAsync(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
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

        private int[] ParseTextToNumbers(string content)
        {
            return content.Split(' ').Select(int.Parse).ToArray();
        }

        private bool DirectoryDoesNotExistOrIsEmpty()
        {
            return !Directory.Exists(_fileDirectory) || !Directory.EnumerateFiles(_fileDirectory).Any();
        }
    }
}
