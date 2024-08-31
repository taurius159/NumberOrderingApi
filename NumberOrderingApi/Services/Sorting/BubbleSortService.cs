namespace NumberOrderingApi.Services.Sorting
{
    /// <summary>
    /// Service for sorting numbers using the BubbleSort algorithm.
    /// </summary>
    public class BubbleSortService : ISortingService
    {
        /// <summary>
        /// Sorts an array of numbers using the BubbleSort algorithm.
        /// </summary>
        /// <param name="numbers">The array of numbers to sort.</param>
        /// <returns>The sorted array of numbers.</returns>
        public int[] Sort(int[] numbers)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = 0; j < numbers.Length - i - 1; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        // Swap the elements
                        int temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
        
            return numbers;
        }
    }
}
