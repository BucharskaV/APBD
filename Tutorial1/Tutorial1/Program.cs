public static class Tutorial1
{
    public static double CalculateAverage(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
            return 0;

        return numbers.Average();
    }
}