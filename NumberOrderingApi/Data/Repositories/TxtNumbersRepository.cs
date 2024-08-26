namespace NumberOrderingApi.Data.Repositories
{
    public class TxtNumbersRepository : INumbersRepository
    {
        private readonly string _fileDirectory;
        
        public TxtNumbersRepository(string fileDirectory)
        {
            _fileDirectory = fileDirectory;
        }

        public void SaveResults(int[] numbers)
        {
            if(!Directory.Exists(_fileDirectory))
            {
                Directory.CreateDirectory(_fileDirectory);
            }
            //save even second and milisecond
            var filePath = Path.Combine(_fileDirectory, $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.txt");

            var content = string.Join(" ", numbers);

            File.AppendAllLines(filePath, [content]);
        }

        public int[] ReadLastSavedFile()
        {
            if (!Directory.Exists(_fileDirectory) || !Directory.EnumerateFiles(_fileDirectory).Any())
            {
                return [];
            }

            var lastFile = Directory.GetFiles(_fileDirectory).OrderByDescending(f => new FileInfo(f).CreationTime).First();

            return File.ReadAllText(lastFile).Split(" ").Select(int.Parse).ToArray();
        }
    }
}
