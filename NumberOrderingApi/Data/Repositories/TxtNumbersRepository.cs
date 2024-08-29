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
            if(!Directory.Exists(_fileDirectory))
            {
                Directory.CreateDirectory(_fileDirectory);
            }

            var filePath = Path.Combine(_fileDirectory, $"{DateTime.Now:yyyyMMddHHmmssfff}.txt");

            var content = string.Join(" ", numbers);

            await File.AppendAllLinesAsync(filePath, [content]);
        }

        public async Task<int[]> ReadLastSavedResults()
        {
            if (!Directory.Exists(_fileDirectory) || !Directory.EnumerateFiles(_fileDirectory).Any())
            {
                return [];
            }

            var lastFile = Directory.GetFiles(_fileDirectory).OrderByDescending(f => new FileInfo(f).CreationTime).First();
            
            var fileContent = await File.ReadAllTextAsync(lastFile);

            return fileContent.Split(" ").Select(int.Parse).ToArray();
        }
    }
}
