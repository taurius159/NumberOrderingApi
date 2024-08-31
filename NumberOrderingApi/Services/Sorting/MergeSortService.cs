namespace NumberOrderingApi.Services.Sorting
{
    /// <summary>
    /// Service for sorting numbers using the MergeSort algorithm.
    /// </summary>
    public class MergeSortService : ISortingService
    {
        /// <summary>
        /// Sorts an array of numbers using the MergeSort algorithm.
        /// </summary>
        /// <param name="numbers">The array of numbers to sort.</param>
        /// <returns>The sorted array of numbers.</returns>
        public int[] Sort(int[] numbers)
        {
            MergeSort(numbers, 0, numbers.Length - 1);
            return numbers;
        }

        /// <summary>
        /// Recursively sorts the array using the MergeSort algorithm.
        /// </summary>
        /// <param name="numbers">The array of numbers to sort.</param>
        /// <param name="left">The starting index of the array segment to sort.</param>
        /// <param name="right">The ending index of the array segment to sort.</param>
        private void MergeSort(int[] numbers, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSort(numbers, left, middle);
                MergeSort(numbers, middle + 1, right);
                Merge(numbers, left, middle, right);
            }
        }

        /// <summary>
        /// Merges two sorted segments of the array.
        /// </summary>
        /// <param name="numbers">The array of numbers to merge.</param>
        /// <param name="left">The starting index of the first segment.</param>
        /// <param name="middle">The ending index of the first segment and the starting index of the second segment.</param>
        /// <param name="right">The ending index of the second segment.</param>
        private void Merge(int[] numbers, int left, int middle, int right)
        {
            int leftSize = middle - left + 1;
            int rightSize = right - middle;
            var leftArray = new int[leftSize];
            var rightArray = new int[rightSize];
            Array.Copy(numbers, left, leftArray, 0, leftSize);
            Array.Copy(numbers, middle + 1, rightArray, 0, rightSize);

            int i = 0, j = 0, k = left;
            while (i < leftSize && j < rightSize)
            {
                if (leftArray[i] <= rightArray[j])
                {
                    numbers[k++] = leftArray[i++];
                }
                else
                {
                    numbers[k++] = rightArray[j++];
                }
            }

            while (i < leftSize)
            {
                numbers[k++] = leftArray[i++];
            }

            while (j < rightSize)
            {
                numbers[k++] = rightArray[j++];
            }
        }
    }
}
