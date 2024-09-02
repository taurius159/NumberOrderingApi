using System.Diagnostics;
using NumberOrderingApi.Services.Sorting;

namespace NumberOrderingApi.Services
{
    public class SortPerformerService : ISortPerformerService
    {
        private readonly IEnumerable<ISortingService> _sortingServices;

        private readonly ILogger<SortPerformerService> _logger;

        public SortPerformerService(IEnumerable<ISortingService> sortingServices, ILogger<SortPerformerService> logger)
        {
            _sortingServices = sortingServices;
            _logger = logger;
        }

        public int[] Sort(int[] numbers)
        {
            int[] sortedNumbers = [];
            bool isFirst = true;

            foreach (var sortingService in _sortingServices)
            {
                if (isFirst)
                {
                    sortedNumbers = ExecuteAndLogSorting(sortingService, numbers);
                    isFirst = false;
                }
                else
                {
                    throw new NotImplementedException("Not implemented yet");
                    Task.Run(() => ExecuteAndLogSorting(sortingService, numbers));
                }
            }

            return sortedNumbers;
        }

        private int[] ExecuteAndLogSorting(ISortingService sortingService, int[] numbers)
        {
            var stopwatch = Stopwatch.StartNew();
            var sortedNumbers = sortingService.Sort(numbers);
            stopwatch.Stop();

            // Convert elapsed ticks to microseconds
            var elapsedMicroseconds = stopwatch.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));

            _logger.LogInformation($"{sortingService.GetType().Name} took {elapsedMicroseconds} µs");

            return sortedNumbers;
        }
    }
}
