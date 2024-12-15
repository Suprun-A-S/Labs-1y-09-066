using System;
using System.Diagnostics;

class Program
{
    // Розміри для тестування
    static readonly int[] Sizes = { 10, 1_000, 10_000, 1_000_000, 100_000_000 };

    // Алгоритми сортування для порівняння
    static readonly (string Name, Action<int[]> SortMethod)[] Algorithms = new (string, Action<int[]>)[]
    {
        ("Insertion Sort", InsertionSort),
        ("Bubble Sort", BubbleSort),
        ("Quick Sort", QuickSort),
        ("Merge Sort", MergeSort),
        ("Counting Sort", CountingSort),
        ("Radix Sort", RadixSort)
        // Можна додати інші: ("Timsort (Array.Sort)", ArraySortWrapper), тощо.
    };

    static void Main()
    {
        // Для демонстрації можливе обмеження найбільших розмірів масивів для повільних алгоритмів.

        // 1) Великий діапазон випадкових чисел
        Console.WriteLine("===== 1) Великий діапазон випадкових чисел =====");
        TestScenario(largeInterval: true, smallInterval: false, reversed: false);

        // 2) Малий діапазон випадкових чисел
        Console.WriteLine("\n===== 2) Малий діапазон випадкових чисел =====");
        TestScenario(largeInterval: false, smallInterval: true, reversed: false);

        // 3) Великий діапазон, у зворотному порядку
        Console.WriteLine("\n===== 3) Великий діапазон, у зворотному порядку =====");
        TestScenario(largeInterval: true, smallInterval: false, reversed: true);

        // 4) Малий діапазон, у зворотному порядку
        Console.WriteLine("\n===== 4) Малий діапазон, у зворотному порядку =====");
        TestScenario(largeInterval: false, smallInterval: true, reversed: true);

        // Висновки та рекомендації:
        Console.WriteLine("\n===== Висновки та рекомендації =====");
        Console.WriteLine("1. Insertion Sort та Bubble Sort - повільні O(n²) алгоритми, придатні лише для дуже малих масивів.");
        Console.WriteLine("2. Quick Sort та Merge Sort мають хорошу середню продуктивність. Merge Sort гарантує O(n log n) у найгіршому випадку.");
        Console.WriteLine("3. Counting Sort та Radix Sort - це некомпаративні алгоритми сортування, які можуть бути дуже швидкими для малих діапазонів цілих чисел, але можуть бути непрактичними для великих діапазонів через використання пам'яті або складність.");
        Console.WriteLine("4. Для дуже великих наборів даних слід використовувати ефективні O(n log n) алгоритми, такі як Merge Sort або вбудований Array.Sort(). Для майже відсортованих даних Timsort (наприклад, вбудоване сортування в .NET) є ідеальним.");
        Console.WriteLine("5. Якщо дані попередньо відсортовані в порядку спадання, а вам потрібен порядок зростання, деякі алгоритми (наприклад, Insertion Sort) значно знижують продуктивність, тоді як Merge Sort та Quick Sort справляються з цим краще.");
    }

    static void TestScenario(bool largeInterval, bool smallInterval, bool reversed)
    {
        // Виведення заголовка таблиці
        Console.Write("Алгоритм".PadRight(20));
        foreach (var size in Sizes)
        {
            Console.Write(size.ToString().PadRight(15));
        }
        Console.WriteLine();

        foreach (var (Name, SortMethod) in Algorithms)
        {
            Console.Write(Name.PadRight(20));

            foreach (int size in Sizes)
            {
                // Пропустити великі тести для повільних алгоритмів (опційно)
                if ((Name == "Bubble Sort" || Name == "Insertion Sort") && size > 10_000)
                {
                    Console.Write("N/A".PadRight(15));
                    continue;
                }

                // Генерація масиву
                int[] arr = GenerateArray(size, largeInterval, smallInterval, reversed);

                // Вимірювання часу
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Stopwatch sw = Stopwatch.StartNew();
                SortMethod(arr);
                sw.Stop();

                Console.Write($"{sw.ElapsedMilliseconds} ms".PadRight(15));
            }
            Console.WriteLine();
        }
    }

    static int[] GenerateArray(int size, bool largeInterval, bool smallInterval, bool reversed)
    {
        int[] arr = new int[size];
        Random rnd = new Random();

        if (largeInterval)
        {
            // Великий діапазон: повний діапазон int
            for (int i = 0; i < size; i++)
                arr[i] = rnd.Next(int.MinValue, int.MaxValue);
        }
        else if (smallInterval)
        {
            // Малий діапазон: від 0 до 100
            for (int i = 0; i < size; i++)
                arr[i] = rnd.Next(0, 101);
        }

        // Якщо потрібно, відсортувати та перевернути
        if (reversed)
        {
            Array.Sort(arr);       // сортування за зростанням
            Array.Reverse(arr);    // зворотній порядок
        }

        return arr;
    }

    // Insertion Sort (сортування вставками)
    static void InsertionSort(int[] arr)
    {
        for (int i = 1; i < arr.Length; i++)
        {
            int key = arr[i];
            int j = i - 1;
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = key;
        }
    }

    // Bubble Sort (бульбашкове сортування)
    static void BubbleSort(int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int tmp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = tmp;
                }
            }
        }
    }

    // Quick Sort (швидке сортування)
    static void QuickSort(int[] arr)
    {
        QuickSortHelper(arr, 0, arr.Length - 1);
    }

    static int ChoosePivot(int[] arr, int low, int high)
    {
        Random rnd = new Random();
        int pivotIndex = rnd.Next(low, high + 1);
        (arr[low], arr[pivotIndex]) = (arr[pivotIndex], arr[low]);
        return arr[low];
    }

    static void QuickSortHelper(int[] arr, int low, int high)
    {
        if (low >= high) return;

        int lt = low, i = low + 1, gt = high;
        int pivot = ChoosePivot(arr, low, high);

        while (i <= gt)
        {
            if (arr[i] < pivot)
            {
                (arr[lt], arr[i]) = (arr[i], arr[lt]);
                lt++;
                i++;
            }
            else if (arr[i] > pivot)
            {
                (arr[i], arr[gt]) = (arr[gt], arr[i]);
                gt--;
            }
            else
            {
                i++;
            }
        }

        if (low < lt - 1) QuickSortHelper(arr, low, lt - 1);
        if (gt + 1 < high) QuickSortHelper(arr, gt + 1, high);
    }

    // Merge Sort (злиття)
    static void MergeSort(int[] arr)
    {
        if (arr.Length <= 1) return;
        MergeSortHelper(arr, 0, arr.Length - 1);
    }

    static void MergeSortHelper(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;
            MergeSortHelper(arr, left, mid);
            MergeSortHelper(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }
    }

    static void Merge(int[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] L = new int[n1];
        int[] R = new int[n2];

        Array.Copy(arr, left, L, 0, n1);
        Array.Copy(arr, mid + 1, R, 0, n2);

        int i = 0, j = 0, k = left;
        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                arr[k++] = L[i++];
            }
            else
            {
                arr[k++] = R[j++];
            }
        }

        while (i < n1) arr[k++] = L[i++];
        while (j < n2) arr[k++] = R[j++];
    }

    // Counting Sort (сортування підрахунком, лише для невід'ємних чисел)
    static void CountingSort(int[] arr)
    {
        if (arr.Length <= 1) return;

        // Знаходимо мінімум і максимум
        int min = int.MaxValue, max = int.MinValue;
        foreach (int v in arr)
        {
            if (v < min) min = v;
            if (v > max) max = v;
        }

        // Якщо діапазон занадто великий, використовуємо Array.Sort
        long range = (long)max - (long)min + 1;
        if (range > 50_000_000)
        {
            Array.Sort(arr);
            return;
        }

        int[] count = new int[range];
        foreach (int v in arr)
        {
            count[v - min]++;
        }

        int idx = 0;
        for (int i = 0; i < count.Length; i++)
        {
            for (int c = 0; c < count[i]; c++)
            {
                arr[idx++] = i + min;
            }
        }
    }

    // Radix Sort (для невід'ємних чисел з урахуванням від'ємних значень)
    static void RadixSort(int[] arr)
    {
        if (arr == null || arr.Length <= 1)
            return;

        // Знаходимо minVal і maxVal
        int minVal = arr[0];
        int maxVal = arr[0];
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] < minVal) minVal = arr[i];
            if (arr[i] > maxVal) maxVal = arr[i];
        }

        // Сдвиг для від'ємних значень
        if (minVal < 0)
        {
            int shift = -minVal;
            for (int i = 0; i < arr.Length; i++)
                arr[i] += shift;
            maxVal += shift;
        }

        // Класичний Radix Sort
        for (int exp = 1; maxVal / exp > 0; exp *= 10)
            CountingSortByDigit(arr, exp);

        // Відновлення значень
        if (minVal < 0)
        {
            int shift = -minVal;
            for (int i = 0; i < arr.Length; i++)
                arr[i] -= shift;
        }
    }

    static void CountingSortByDigit(int[] arr, int exp)
    {
        int[] output = new int[arr.Length];
        int[] count = new int[10];

        // Підрахунок частот для поточного розряду
        for (int i = 0; i < arr.Length; i++)
        {
            int digit = (arr[i] / exp) % 10;
            count[digit]++;
        }

        // Преобразование count[i]
        for (int i = 1; i < 10; i++)
            count[i] += count[i - 1];

        // Формування вихідного масиву
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            int digit = (arr[i] / exp) % 10;
            output[count[digit] - 1] = arr[i];
            count[digit]--;
        }

        // Копіювання назад
        Array.Copy(output, arr, arr.Length);
    }

    // Обгортка для Array.Sort (вбудоване сортування)
    static void ArraySortWrapper(int[] arr)
    {
        Array.Sort(arr);
    }
}