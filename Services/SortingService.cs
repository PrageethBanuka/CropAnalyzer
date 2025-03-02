public class SortingService
{
    public static List<T> SortByQuantity<T>(List<T> data, Func<T, double> quantitySelector)
    {
        var array = data.ToArray();
        QuickSort(array, 0, array.Length - 1, quantitySelector);
        return array.Reverse().ToList(); // Reverse to get descending order
    }

    private static void QuickSort<T>(T[] array, int low, int high, Func<T, double> quantitySelector)
    {
        if (low < high)
        {
            int pivot = Partition(array, low, high, quantitySelector);
            QuickSort(array, low, pivot - 1, quantitySelector);
            QuickSort(array, pivot + 1, high, quantitySelector);
        }
    }

    private static int Partition<T>(T[] array, int low, int high, Func<T, double> quantitySelector)
    {
        double pivot = quantitySelector(array[high]);
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (quantitySelector(array[j]) > pivot)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        (array[i + 1], array[high]) = (array[high], array[i + 1]);
        return i + 1;
    }
}