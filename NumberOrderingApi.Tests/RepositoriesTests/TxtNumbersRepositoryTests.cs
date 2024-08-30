using NumberOrderingApi.Data.Repositories;

namespace NumberOrderingApi.Tests.Repositories
{
    [TestClass]
    public class TxtNumbersRepositoryTests
    {
        private string _customTempPath;
        private TxtNumbersRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _customTempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            _repository = new TxtNumbersRepository(_customTempPath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(_customTempPath))
            {
                Directory.Delete(_customTempPath, true);
            }
        }

        [TestMethod]
        public async Task SaveResults_ShouldCreateFileWithNumbers_WhenNumbersArePassed()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3 };

            // Act
            await _repository.SaveResults(numbers);

            // Assert
            var files = Directory.GetFiles(_customTempPath);
            Assert.AreEqual(1, files.Length);

            var fileContent = await File.ReadAllTextAsync(files[0]);
            Assert.AreEqual("1 2 3", fileContent);
        }

        [TestMethod]
        public async Task SaveResults_ShouldNotCreateFile_WhenEmptyArrayIsPassed()
        {
            // Arrange
            var numbers = Array.Empty<int>();

            // Act
            await _repository.SaveResults(numbers);

            // Assert
            var directoryExists = Directory.Exists(_customTempPath);
            Assert.AreEqual(false, directoryExists);
        }

        [TestMethod]
        public async Task SaveResults_ShouldCreate2Files_When2ConcurrentRequestsHappen()
        {
            // Arrange
            var numbers1 = new[] { 1, 2, 3 };
            var numbers2 = new[] { 4, 5, 6 };
            var tasks = new List<Task>
            {
                _repository.SaveResults(numbers1),
                _repository.SaveResults(numbers2)
            };

            // Act
            await Task.WhenAll(tasks);

            // Assert
            var files = Directory.GetFiles(_customTempPath);
            Assert.AreEqual(2, files.Length);
        }

        [TestMethod]
        public async Task ReadLastSavedResults_ShouldReturnLastSavedNumbers_WhenAFewFilesAreSaved()
        {
            // Arrange
            var numbers1 = new[] { 1, 2, 3 };
            var numbers2 = new[] { 4, 5, 6 };

            await _repository.SaveResults(numbers1);
            await Task.Delay(1); // 1 milisecond delay to ensure the first file is being saved first
            await _repository.SaveResults(numbers2);

            // Act
            var result = await _repository.ReadLastSavedResults();

            // Assert
            Assert.AreEqual("4 5 6", result);
        }

        [TestMethod]
        public async Task ReadLastSavedResults_ShouldReturnEmptyString_WhenNoFilesAreSaved()
        {
            // Act
            var result = await _repository.ReadLastSavedResults();

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public async Task ReadLastSavedResults_ShouldReturnOneFileContent_WhenOneFileIsSaved()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3 };

            // Act
            await _repository.SaveResults(numbers);
            var result = await _repository.ReadLastSavedResults();

            // Assert
            Assert.AreEqual("1 2 3", result);
        }
    }
}
