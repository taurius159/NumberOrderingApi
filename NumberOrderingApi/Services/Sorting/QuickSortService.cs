namespace NumberOrderingApi.Services.Sorting
{
    public class QuickSortService : ISortingService
    {
        /// <summary>
        /// Sorts an array of numbers using the QuickSort algorithm.
        /// </summary>
        /// <param name="numbers">The array of numbers to sort.</param>
        /// <returns>The sorted array of numbers.</returns>
        public int[] Sort(int[] numbers)
        {
            QuickSort(numbers, 0, numbers.Length - 1);
            return numbers;
        }

        /// <summary>
        /// Recursively sorts the array using the QuickSort algorithm.
        /// </summary>
        /// <param name="numbers">The array of numbers to sort.</param>
        /// <param name="left">The starting index of the array segment to sort.</param>
        /// <param name="right">The ending index of the array segment to sort.</param>
        private void QuickSort(int[] numbers, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(numbers, left, right);
                QuickSort(numbers, left, pivotIndex - 1);
                QuickSort(numbers, pivotIndex + 1, right);
            }
        }

        /// <summary>
        /// Partitions the array segment and returns the pivot index.
        /// </summary>
        /// <param name="numbers">The array of numbers to partition.</param>
        /// <param name="left">The starting index of the array segment to partition.</param>
        /// <param name="right">The ending index of the array segment to partition.</param>
        /// <returns>The pivot index.</returns>
        private int Partition(int[] numbers, int left, int right)
        {
            int pivot = numbers[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {
                if (numbers[j] <= pivot)
                {
                    i++;
                    Swap(numbers, i, j);
                }
            }
            Swap(numbers, i + 1, right);
            return i + 1;
        }

        /// <summary>
        /// Swaps two elements in the array.
        /// </summary>
        /// <param name="numbers">The array of numbers.</param>
        /// <param name="i">The index of the first element to swap.</param>
        /// <param name="j">The index of the second element to swap.</param>
        private void Swap(int[] numbers, int i, int j)
        {
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }
    }
}
