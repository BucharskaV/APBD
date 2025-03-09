public static class Tutorial1
{
    public static double CalculateAverage(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
            return 0;

        int sum = 0;
        foreach (var value in numbers)
        {
            sum += value;
        }
        return (double)sum / numbers.Length;
    }
    
    public static int FindMaxValue(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
            throw new ArgumentException("Array cannot be null or empty");

        return numbers.Max();
    }
}