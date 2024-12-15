using System;
using System.Text;
using Lab5.Queues;
using Lab5.Stacks;

namespace Lab5
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;

            // Stack Завдання 1: Розбиття рядка на символи і числа
            Console.WriteLine("Stack Завдання 1:");
            CharNumber charNumberSplitter = new CharNumber();
            charNumberSplitter.SplitString("Hello123World456");

            // Stack Завдання 2: Розбиття рядка на голосні і приголосні
            Console.WriteLine("\nStack Завдання 2:");
            VowelConsonant vowelConsSplitter = new VowelConsonant();
            vowelConsSplitter.SplitString("Hello World!");

            // Stack Завдання 3: Сортування масиву за допомогою стеку
            Console.WriteLine("\nStack Завдання 3:");
            StackSorter sorter = new StackSorter();
            int[] arr = { 34, 7, 23, 32, 5, 62 };
            sorter.SortArray(arr);
            Console.WriteLine("Відсортований масив: " + string.Join(" ", arr));

            // Queue Завдання: Видалення перших k елементів з черги
            Console.WriteLine("\nQueue Завдання:");
            Console.Write("Введіть довжину черги (n): ");
            int n = int.Parse(Console.ReadLine());

            Console.Write("Скільки елементів видалити (k): ");
            int k = int.Parse(Console.ReadLine());

            ArrayQueue queue = new ArrayQueue(n + 1);
            Random rand = new Random();

            // Додавання елементів у чергу
            Console.WriteLine("Додані елементи:");
            for (int i = 0; i < n; i++)
            {
                int val = rand.Next(1, 101); // Генерація випадкового числа від 1 до 100
                queue.Insert(val);
                Console.Write(val + " ");
            }
            Console.WriteLine();

            // Видалення перших k елементів
            Console.WriteLine($"Видалення {k} елементів:");
            for (int i = 0; i < k; i++)
            {
                Console.Write(queue.Remove() + " ");
            }
            Console.WriteLine();

            // Виведення черги після видалення елементів
            Console.WriteLine("Черга після видалення:");
            queue.Display();

            // Тест методу очищення черги
            Console.WriteLine("Очищення черги:");
            queue.Clear();
            queue.Display();

            // Черга з пріоритетом
            Console.WriteLine("\nПриклад черги з пріоритетом:");
            PriorityQueue pq = new PriorityQueue();

            // Додавання елементів у чергу з пріоритетом
            pq.Insert(5);
            pq.Insert(1);
            pq.Insert(3);
            pq.Insert(2);
            pq.Display();

            // Видалення елемента з черги з пріоритетом
            Console.WriteLine("Видалення елемента: " + pq.Remove());
            pq.Display();
        }
    }
}