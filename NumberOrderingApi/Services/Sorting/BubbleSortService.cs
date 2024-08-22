namespace NumberOrderingApi.Services.Sorting
{
    public class BubbleSortService : ISortingService
    {
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
