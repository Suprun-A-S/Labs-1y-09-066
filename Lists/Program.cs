using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        const int listSize = 1000000;
        const int insertSize = 1000;
        Random random = new Random();

        // ArrayList та LinkedList
        List<int> arrayList = new List<int>();
        LinkedList<int> linkedList = new LinkedList<int>();

        // 1. Заповнення списку даними
        Console.WriteLine("1. Заповнення списку даними");
        MeasureTime("ArrayList заповнення", () =>
        {
            for (int i = 0; i < listSize; i++)
                arrayList.Add(random.Next());
        });

        MeasureTime("LinkedList заповнення", () =>
        {
            for (int i = 0; i < listSize; i++)
                linkedList.AddLast(random.Next());
        });

        // 2. Доступ до елементу Random Access та Sequential Access
        Console.WriteLine("\n2. Доступ до елементу");
        MeasureTime("ArrayList Random Access", () =>
        {
            for (int i = 0; i < listSize; i++)
                _ = arrayList[random.Next(listSize)];
        });

        MeasureTime("LinkedList Sequential Access (ітератор)", () =>
        {
            foreach (var item in linkedList) { }
        });

        // 3. Вставка елементу на початок списку
        Console.WriteLine("\n3. Вставка елементу на початок списку");
        MeasureTime("ArrayList вставка на початок", () =>
        {
            for (int i = 0; i < insertSize; i++)
                arrayList.Insert(0, random.Next());
        });

        MeasureTime("LinkedList вставка на початок", () =>
        {
            for (int i = 0; i < insertSize; i++)
                linkedList.AddFirst(random.Next());
        });

        // 4. Вставка елементу в кінець списку
        Console.WriteLine("\n4. Вставка елементу в кінець списку");
        MeasureTime("ArrayList вставка в кінець", () =>
        {
            for (int i = 0; i < insertSize; i++)
                arrayList.Add(random.Next());
        });

        MeasureTime("LinkedList вставка в кінець", () =>
        {
            for (int i = 0; i < insertSize; i++)
                linkedList.AddLast(random.Next());
        });

        // 5. Вставка елементу в середину списку
        Console.WriteLine("\n5. Вставка елементу в середину списку");
        MeasureTime("ArrayList вставка в середину", () =>
        {
            for (int i = 0; i < insertSize; i++)
                arrayList.Insert(arrayList.Count / 2, random.Next());
        });

        MeasureTime("LinkedList вставка в середину", () =>
        {
            for (int i = 0; i < insertSize; i++)
            {
                var middleNode = GetMiddleNode(linkedList);
                linkedList.AddBefore(middleNode, random.Next());
            }
        });

        Console.WriteLine("\nТест завершено.");
    }

    // Метод для вимірювання часу виконання
    static void MeasureTime(string operation, Action action)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        action.Invoke();
        stopwatch.Stop();
        Console.WriteLine($"{operation}: {stopwatch.ElapsedMilliseconds} ms");
    }

    // Метод для знаходження середнього елемента у LinkedList
    static LinkedListNode<int> GetMiddleNode(LinkedList<int> list)
    {
        var slow = list.First;
        var fast = list.First;

        while (fast != null && fast.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;
        }
        return slow;
    }
}
