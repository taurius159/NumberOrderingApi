using NumberOrderingApi.Data.Repositories;

namespace NumberOrderingApi.Tests.Repositories
{
    [TestClass]
    public class TxtNumbersRepositoryTests
    {
        private string _tempDirectory;
        private TxtNumbersRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            _repository = new TxtNumbersRepository(_tempDirectory);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(_tempDirectory))
            {
                Directory.Delete(_tempDirectory, true);
            }
        }

        [TestMethod]
        public async Task SaveResults_ShouldCreateFileWithNumbers()
        {
            // Arrange
            var numbers = new[] { 1, 2, 3 };

            // Act
            await _repository.SaveResults(numbers);

            // Assert
            var files = Directory.GetFiles(_tempDirectory);
            Assert.AreEqual(1, files.Length);

            var fileContent = await File.ReadAllTextAsync(files[0]);
            Assert.AreEqual("1 2 3\n", fileContent);
        }

        [TestMethod]
        public async Task ReadLastSavedResults_ShouldReturnLastSavedNumbers()
        {
            // Arrange
            var numbers1 = new[] { 1, 2, 3 };
            var numbers2 = new[] { 4, 5, 6 };

            await _repository.SaveResults(numbers1);
            await Task.Delay(10); // Ensure different timestamps
            await _repository.SaveResults(numbers2);

            // Act
            var result = await _repository.ReadLastSavedResults();

            // Assert
            CollectionAssert.AreEqual(numbers2, result);
        }

        [TestMethod]
        public async Task ReadLastSavedResults_ShouldReturnEmptyArray_WhenNoFilesExist()
        {
            // Act
            var result = await _repository.ReadLastSavedResults();

            // Assert
            Assert.AreEqual(0, result.Length);
        }
    }
}
